using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace LuceneWinApp
{
    using DotNet.Common.Util;
    using DotNet.SQLServer.DataAccess;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Search;
    using LuceneIO = Lucene.Net.Store;

    public partial class MainForm : Form
    {

        #region 公共字段
        public static SearchForm sf = null;//搜索窗体
        private static readonly string INDEX_STORE_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "index");//索引所在目录
        private IndexSearcher searcher = null;//索引搜索器
        private DateTime dtStart = DateTime.Now;
        private static LuceneIO.RAMDirectory ramDir = null;
        private static readonly string strSqlConn = ConfigurationManager.ConnectionStrings["sqlserver"].ConnectionString;//数据库连接字符串

        #endregion

        #region 辅助方法

        /// <summary>
        /// 文本提示清空，状态还原
        /// </summary>
        private void SetCtrReady()
        {
            this.rtbMsg.Text = string.Empty;
            this.statusBarLucene.Text = "准备";
        }

        delegate void ShowOutput(string msg);

        /// <summary>
        /// 将操作信息显示在richtextbox控件上
        /// </summary>
        /// <param name="msg"></param>
        private void SetOutput(string msg)
        {
            if (this.rtbMsg.InvokeRequired == false)
            {
                rtbMsg.AppendText(msg);
                rtbMsg.AppendText(Environment.NewLine);
                this.statusBarLucene.Text = msg;
            }
            else
            {
                ShowOutput action = new ShowOutput(SetOutput);
                this.BeginInvoke(action, new object[] { msg });
            }
        }

        /// <summary>
        /// 获取某一目录下的所有txt文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="listFiles"></param>
        private void GetTxtFiles(string path, IList<string> listFiles)
        {
            if (Directory.Exists(path))
            {
                String[] files = Directory.GetFileSystemEntries(path);
                for (int i = 0; i < files.Length; i++)
                {
                    GetTxtFiles(files[i], listFiles);  //递归 
                }

            }
            else if (path.ToLower().LastIndexOf(".txt") > -1)
            {
                listFiles.Add(path);
            }
        }

        /// <summary>
        /// 递归删除文件
        /// </summary>
        /// <param name="path"></param>
        private void DeleteFiles(string path)
        {
            if (string.IsNullOrEmpty(path) || Directory.Exists(path) == false)
            {
                return;
            }
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] fileInfos = dirInfo.GetFiles();
            if (fileInfos != null && fileInfos.Length > 0)
            {
                foreach (FileInfo fileInfo in fileInfos)
                {
                    File.Delete(fileInfo.FullName); //删除文件
                }
            }

            DirectoryInfo[] dirInfos = dirInfo.GetDirectories();
            if (dirInfos != null && dirInfos.Length > 0)
            {
                foreach (DirectoryInfo childDirInfo in dirInfos)
                {
                    DeleteFiles(childDirInfo.Name); //递归
                }
            }
            Directory.Delete(dirInfo.FullName, true); //删除目录
        }

        /// <summary>
        /// 是否已经创建索引文件
        /// </summary>
        /// <returns></returns>
        private bool IsEnableCreated()
        {
            bool enableCreate = true;
            StringBuffer sb = INDEX_STORE_PATH + "\\segments.gen";
            if (System.IO.File.Exists(sb.ToString()))
            {
                enableCreate = false;
            }
            return enableCreate;
        }

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 1;
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedTab.Text)
            {
                default:
                    break;
                case "打开搜索窗体":
                    if (sf == null)
                    {
                        sf = new SearchForm(this);
                        sf.Show(this);
                        this.Visible = false;
                    }
                    return;
            }
            SetCtrReady();
        }

        #region 索引至文件

        delegate void AsyncIndexDirectoryCaller(IndexModifier modifier, FileInfo file);

        private void IndexDirectory(IndexModifier modifier, FileInfo file)
        {
            if (Directory.Exists(file.FullName))
            {
                String[] files = Directory.GetFileSystemEntries(file.FullName);
                // an IO error could occur 
                if (files != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        IndexDirectory(modifier, new FileInfo(files[i]));  //递归 
                    }
                }
            }
            else if (string.Compare(file.Extension.ToLower(), ".txt") == 0)
            {
                IndexFile(file, modifier);
            }
        }

        /// <summary>
        /// 索引文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="modifier"></param>
        private void IndexFile(FileInfo file, IndexModifier modifier)
        {
            try
            {
                Document doc = new Document();//创建文档，给文档添加字段，并把文档添加到索引书写器里
                SetOutput("正在建立索引，文件名：" + file.FullName);

                doc.Add(new Field("id", id.ToString(), Field.Store.YES, Field.Index.TOKENIZED));//存储且索引
                id++;

                /* filename begin */
                doc.Add(new Field("filename", file.FullName, Field.Store.YES, Field.Index.TOKENIZED));//存储且索引
                //doc.Add(new Field("filename", file.FullName, Field.Store.YES, Field.Index.UN_TOKENIZED));
                //doc.Add(new Field("filename", file.FullName, Field.Store.NO, Field.Index.TOKENIZED));
                //doc.Add(new Field("filename", file.FullName, Field.Store.NO, Field.Index.UN_TOKENIZED));
                /* filename end */

                /* contents begin */
                //doc.Add(new Field("contents", new StreamReader(file.FullName, System.Text.Encoding.Default)));

                string contents = string.Empty;
                using (TextReader rdr = new StreamReader(file.FullName, System.Text.Encoding.Default))
                {
                    contents = rdr.ReadToEnd();//将文件内容提取出来
                    doc.Add(new Field("contents", contents, Field.Store.YES, Field.Index.TOKENIZED));//存储且索引
                    //doc.Add(new Field("contents", contents, Field.Store.NO, Field.Index.TOKENIZED));//不存储索引
                }
                /* contents end */
                modifier.AddDocument(doc);
            }

            catch (FileNotFoundException fnfe)
            {
                SetOutput(fnfe.Message);
            }
        }

        /// <summary>
        /// 索引优化(可以按照一定的策略进行优化 ，比如每天深夜自动进行索引优化)
        /// </summary>
        /// <param name="result"></param>
        private void IndexCallback(IAsyncResult result)
        {
            IndexModifier modifier = result.AsyncState as IndexModifier;
            modifier.Optimize();//优化索引
            modifier.Close();//关闭索引读写器
            TimeSpan span = DateTime.Now - dtStart;
            StringBuffer sb = "索引完成,共用时：" + span.Hours + "时 " + span.Minutes + "分 " + span.Seconds + "秒 " + span.Milliseconds + "毫秒";
            SetOutput(sb);
        }

        private static int id = 0;
        private void btnCreateIndex_Click(object sender, EventArgs e)
        {
            DialogResult result = this.fbdSelectFile.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            id = 1;
            IndexModifier modifier = null;
            try
            {
                SetOutput("======================.txt文件索引创建开始===============================");
                modifier = new IndexModifier(INDEX_STORE_PATH, new StandardAnalyzer(), true);

                #region 同步创建索引

                Stopwatch watch = new Stopwatch();
                watch.Start();
                IndexDirectory(modifier, new FileInfo(this.fbdSelectFile.SelectedPath));
                modifier.Optimize();//优化索引
                modifier.Close();//关闭索引读写器
                watch.Stop();
                StringBuffer sb = "索引完成,共用时：" + watch.Elapsed.Hours + "时 " + watch.Elapsed.Minutes + "分 " + watch.Elapsed.Seconds + "秒 " + watch.Elapsed.Milliseconds + "毫秒";
                SetOutput(sb);

                #endregion

                //#region 异步创建索引

                //dtStart = DateTime.Now;
                //AsyncIndexDirectoryCaller caller = new AsyncIndexDirectoryCaller(IndexDirectory);
                //IAsyncResult ar = caller.BeginInvoke(modifier, new FileInfo(this.fbdSelectFile.SelectedPath), new AsyncCallback(IndexCallback), modifier);

                //#endregion
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        private void btnAppendIndex_Click(object sender, EventArgs e)
        {
            DialogResult result = this.fbdSelectFile.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            IndexModifier modifier = null;
            try
            {
                SetOutput("======================.txt文件索引追加开始===============================");
                modifier = new IndexModifier(INDEX_STORE_PATH, new StandardAnalyzer(), IsEnableCreated());

                #region 同步索引

                Stopwatch watch = new Stopwatch();
                watch.Start();
                IndexDirectory(modifier, new FileInfo(this.fbdSelectFile.SelectedPath));
                modifier.Optimize();//优化索引
                modifier.Close();//关闭索引读写器
                watch.Stop();
                StringBuffer sb = "索引追加完成,共用时：" + watch.Elapsed.Hours + "时 " + watch.Elapsed.Minutes + "分 " + watch.Elapsed.Seconds + "秒 " + watch.Elapsed.Milliseconds + "毫秒";
                SetOutput(sb);

                #endregion

            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        ///// <summary>
        ///// 删除索引所保存的文件夹
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnDeleteAllIndex_Click(object sender, EventArgs e)
        //{
        //    DeleteFiles(INDEX_STORE_PATH);
        //    SetOutput(string.Format("{0}文件夹保存的索引已经全部删除", INDEX_STORE_PATH));
        //}

        private void btnDeleteIndex_Click(object sender, EventArgs e)
        {
            string id = txtFileId.Text.Trim();
            if (string.IsNullOrEmpty(id) ==true)
            {
                SetOutput("请输入文件id(整数)");
                return;
            }
            LuceneIO.Directory directory = LuceneIO.FSDirectory.GetDirectory(INDEX_STORE_PATH, false);
            IndexModifier modifier = new IndexModifier(directory, new StandardAnalyzer(), false);

            Term term = new Term("id", id);
            modifier.DeleteDocuments(term);//删除  
            modifier.Close();
            directory.Close();
            SetOutput(string.Format("删除文件索引成功,ID为{0}！", id));
        }

        private void btnUpdateIndex_Click(object sender, EventArgs e)
        {
            string id = txtFileId.Text.Trim();
            if (string.IsNullOrEmpty(id) == true)
            {
                SetOutput("请输入文件id(整数)");
                return;
            }
            string filename = "think in lucene......";
            if (string.IsNullOrEmpty(id))
            {
                return;
            }
            bool enableCreate = IsEnableCreated();//是否已经创建索引文件
            Term term = new Term("id", id);
            Document doc = new Document();
            doc = new Document();//创建文档，给文档添加字段，并把文档添加到索引书写器里
            doc.Add(new Field("id", id, Field.Store.YES, Field.Index.TOKENIZED));//存储且索引
            doc.Add(new Field("filename", filename, Field.Store.YES, Field.Index.TOKENIZED));
            doc.Add(new Field("contents", filename, Field.Store.YES, Field.Index.TOKENIZED));
            LuceneIO.Directory directory = LuceneIO.FSDirectory.GetDirectory(INDEX_STORE_PATH, enableCreate);
            IndexWriter writer = new IndexWriter(directory, new StandardAnalyzer(),IndexWriter.MaxFieldLength.LIMITED);
            writer.UpdateDocument(term, doc);
            writer.Optimize();
            //writer.Commit();
            writer.Close();
            directory.Close();

            SetOutput(string.Format("更新索引.Id:{0}，已经优化成功", id));
        }

        #endregion

        #region 文件索引搜索

        /// <summary>
        /// 根据索引搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private TopDocs Search(string keyword,string field)
        {
            TopDocs docs = null;
            int n = 10;//最多返回多少个结果
            SetOutput(string.Format("正在检索关键字：{0}", keyword));
            try
            {
                QueryParser parser = new QueryParser(field, new StandardAnalyzer());//针对内容查询
                Query query = parser.Parse(keyword);//搜索内容 contents  (用QueryParser.Parse方法实例化一个查询)
                Stopwatch watch = new Stopwatch();
                watch.Start();
                docs = searcher.Search(query, (Filter)null, n); //获取搜索结果
                watch.Stop();
                StringBuffer sb = "搜索完成,共用时：" + watch.Elapsed.Hours + "时 " + watch.Elapsed.Minutes + "分 " + watch.Elapsed.Seconds + "秒 " + watch.Elapsed.Milliseconds + "毫秒";
                SetOutput(sb);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
                docs = null;
            }
            return docs;
        }

        /// <summary>
        /// 显示搜索结果
        /// </summary>
        /// <param name="queryResult"></param>
        private void ShowFileSearchResult(TopDocs queryResult)
        {
            if (queryResult == null || queryResult.totalHits == 0)
            {
                SetOutput("Sorry，没有搜索到你要的结果。");
                return;
            }

            int counter = 1;
            foreach (ScoreDoc sd in queryResult.scoreDocs)
            {
                try
                {
                    Document doc = searcher.Doc(sd.doc);
                    string id = doc.Get("id");//获取id
                    string fileName = doc.Get("filename");//获取文件名
                    string contents = doc.Get("contents");//获取文件内容
                    string result = string.Format("这是第{0}个搜索结果,Id为{1},文件名为:{2}，文件内容为:{3}{4}", counter, id, fileName, Environment.NewLine, contents);
                    SetOutput(result);
                }
                catch (Exception ex)
                {
                    SetOutput(ex.Message);
                }
                counter++;
            }
        }

        /// <summary>
        /// 文件索引搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFileIndexSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtFileKeyword.Text.Trim()))
            {
                return;
            }
            try
            {
                SetOutput(string.Format("==========================搜索开始时间：{0}===========================", DateTime.Now.ToString()));
                searcher = new IndexSearcher(INDEX_STORE_PATH); //构建一个索引搜索器
                TopDocs queryResult = Search(this.txtFileKeyword.Text.Trim(), "contents");//按照内容搜索
                ShowFileSearchResult(queryResult);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        #endregion

        #region 索引至内存

        /// <summary>
        /// 创建索引至内存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMemoryCreateIndex_Click(object sender, EventArgs e)
        {
            DialogResult result = this.fbdSelectFile.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            id = 1;
            IndexModifier modifier = null;
            try
            {
                SetOutput("======================.txt文件内存索引创建开始===============================");
                ramDir = new LuceneIO.RAMDirectory();
                modifier = new IndexModifier(ramDir, new StandardAnalyzer(), true);

                #region 同步创建索引

                Stopwatch watch = new Stopwatch();
                watch.Start();
                IndexDirectory(modifier, new FileInfo(this.fbdSelectFile.SelectedPath));
                modifier.Optimize();//优化索引
                modifier.Close();//关闭索引读写器
                watch.Stop();
                StringBuffer sb = "索引完成,共用时：" + watch.Elapsed.Hours + "时 " + watch.Elapsed.Minutes + "分 " + watch.Elapsed.Seconds + "秒 " + watch.Elapsed.Milliseconds + "毫秒";
                SetOutput(sb);

                #endregion

                //#region 异步创建索引

                //dtStart = DateTime.Now;
                //AsyncIndexDirectoryCaller caller = new AsyncIndexDirectoryCaller(IndexDirectory);
                //IAsyncResult ar = caller.BeginInvoke(modifier, new FileInfo(this.fbdSelectFile.SelectedPath), new AsyncCallback(IndexCallback), modifier);

                //#endregion
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        /// <summary>
        /// 在内存中搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMemorySearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtMemoryKeyword.Text.Trim()))
            {
                return;
            }
            try
            {
                SetOutput(string.Format("==========================搜索开始时间：{0}===========================", DateTime.Now.ToString()));
                searcher = new IndexSearcher(ramDir); //构建一个内存索引搜索器
                TopDocs queryResult = Search(this.txtMemoryKeyword.Text.Trim(), "contents");
                ShowFileSearchResult(queryResult);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        #endregion

        #region 数据库索引创建和搜索

        delegate void AsyncIndexDBCaller(IndexModifier modifier, IList<Person> listModels);

        private void IndexDB(IndexModifier modifier,IList<Person> listModels)
        {
            SetOutput(string.Format("正在建立数据库索引，共{0}人",listModels.Count));
            foreach (Person item in listModels)
            {
                Document doc = new Document();//创建文档，给文档添加字段，并把文档添加到索引书写器里
                doc.Add(new Field("id", item.Id.ToString(), Field.Store.YES, Field.Index.TOKENIZED));//存储且索引
                doc.Add(new Field("fullname", string.Format("{0} {1}",item.FirstName,item.LastName), Field.Store.YES, Field.Index.TOKENIZED));//存储且索引
                modifier.AddDocument(doc);
            }
        }

        private void btnDBCreateIndex_Click(object sender, EventArgs e)
        {

            StringBuffer sql = "SELECT TOP 1000 Id,FirstName,LastName FROM Person(NOLOCK)";
            try
            {
                IList<Person> listPersons = EntityConvertor.QueryForList<Person>(sql.ToString(), strSqlConn, null);
                SetOutput("======================DB索引创建开始===============================");
                IndexModifier modifier = new IndexModifier(INDEX_STORE_PATH, new StandardAnalyzer(), true);

                #region 同步创建索引

                Stopwatch watch = new Stopwatch();
                watch.Start();
                IndexDB(modifier, listPersons);
                modifier.Optimize();//优化索引
                modifier.Close();//关闭索引读写器
                watch.Stop();
                StringBuffer sb = "索引完成,共用时：" + watch.Elapsed.Hours + "时 " + watch.Elapsed.Minutes + "分 " + watch.Elapsed.Seconds + "秒 " + watch.Elapsed.Milliseconds + "毫秒";
                SetOutput(sb);

                #endregion

                //#region 异步创建索引

                //dtStart = DateTime.Now;
                //AsyncIndexDBCaller caller = new AsyncIndexDBCaller(IndexDB);
                //IAsyncResult ar = caller.BeginInvoke(modifier, listPersons, new AsyncCallback(IndexCallback), modifier);

                //#endregion
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        /// <summary>
        /// 根据索引搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private TopDocs SearchDB(string keyword, string field)
        {
            TopDocs docs = null;
            int n = 50;//最多返回多少个结果
            SetOutput(string.Format("正在检索关键字：{0}", keyword));
            try
            {
                QueryParser parser = new QueryParser(field, new StandardAnalyzer());//针对内容查询
                Query query = parser.Parse(keyword);//搜索内容 contents  (用QueryParser.Parse方法实例化一个查询)
                Stopwatch watch = new Stopwatch();
                watch.Start();
                docs = searcher.Search(query, (Filter)null, n); //获取搜索结果
                watch.Stop();
                StringBuffer sb = "索引完成,共用时：" + watch.Elapsed.Hours + "时 " + watch.Elapsed.Minutes + "分 " + watch.Elapsed.Seconds + "秒 " + watch.Elapsed.Milliseconds + "毫秒";
                SetOutput(sb);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
                docs = null;
            }
            return docs;
        }

        /// <summary>
        /// 显示搜索结果
        /// </summary>
        /// <param name="queryResult"></param>
        private void ShowDBSearchResult(TopDocs queryResult)
        {
            if (queryResult == null || queryResult.totalHits == 0)
            {
                SetOutput("Sorry，没有搜索到你要的结果。");
                return;
            }

            int counter = 1;
            foreach (ScoreDoc sd in queryResult.scoreDocs)
            {
                try
                {
                    Document doc = searcher.Doc(sd.doc);
                    string id = doc.Get("id");
                    string fullname = doc.Get("fullname");
                    string result = string.Format("这是第{0}个搜索结果,Id为{1},全名为:{2}", counter, id, fullname);
                    SetOutput(result);
                }
                catch (Exception ex)
                {
                    SetOutput(ex.Message);
                }
                counter++;
            }
        }

        private void btnDBSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtDBKeyword.Text.Trim()))
            {
                return;
            }
            try
            {
                SetOutput(string.Format("==========================DB搜索开始时间：{0}===========================", DateTime.Now.ToString()));
                searcher = new IndexSearcher(INDEX_STORE_PATH); //构建一个索引搜索器
                TopDocs queryResult = SearchDB(this.txtDBKeyword.Text.Trim(), "fullname");//按照全名搜索
                ShowDBSearchResult(queryResult);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        #endregion

    }
}
