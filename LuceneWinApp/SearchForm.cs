using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace LuceneWinApp
{
    using DotNet.Common.Util;
    using DotNet.Common.WinForm;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Search;

    public partial class SearchForm : Form
    {
        #region 字段

        private static int currentPage = 1;//当前第几页
        private static int recordsPerPage = 1;//每页记录数
        private MainForm mainForm = null;
        private static readonly string INDEX_STORE_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "index");//索引所在目录1
        private static readonly string INDEX_STORE_PATH1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "index1");//索引所在目录2
        private IndexSearcher searcher = null;//索引搜索器

        #endregion

        #region 辅助方法

        /// <summary>
        /// 文本提示清空，状态还原
        /// </summary>
        private void SetCtrReady()
        {
            this.rtbMsg.Text = string.Empty;
            this.statusBarLucene.Text = "准备";
            this.panelPager.Visible = false;
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

        #endregion

        public SearchForm()
        {
            InitializeComponent();
        }
        public SearchForm(MainForm main)
        {
            InitializeComponent();
            mainForm = main;
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCtrReady();
        }
        private void SearchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.sf = null;
            mainForm.Visible = true;
        }

        #region 常见QUERY方法汇总

        private TopDocs TermQuery(string keyword, string field)
        {
            TopDocs docs = null;
            int n = 10;//最多返回多少个结果
            SetOutput(string.Format("正在检索关键字：{0}", keyword));
            try
            {
                Term t = new Term(field, keyword);
                Query query = new TermQuery(t);
                Stopwatch watch = new Stopwatch();
                watch.Start();
                docs = searcher.Search(query, (Filter)null, n);
                watch.Stop();
                StringBuffer sb = "TermQuery搜索完成,共用时：" + watch.Elapsed.Hours + "时 " + watch.Elapsed.Minutes + "分 " + watch.Elapsed.Seconds + "秒 " + watch.Elapsed.Milliseconds + "毫秒";
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
        /// RangeQuery
        /// </summary>
        /// <param name="field"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="isInclude">是否包含左边界</param>
        /// <returns></returns>
        private TopDocs RangeQuery( string field,string start,string end,bool isInclude)
        {
            TopDocs docs = null;
            int n = 10;//最多返回多少个结果
            SetOutput(string.Format("正在检索,id范围为{0}~{1}", start,end));
            try
            {
                Term beginT = new Term(field, start);
                Term endT = new Term(field, end);
                Query query = new RangeQuery(beginT, endT, isInclude);
                Stopwatch watch = new Stopwatch();
                watch.Start();
                docs = searcher.Search(query, (Filter)null, n);
                watch.Stop();
                StringBuffer sb = "RangeQuery搜索完成,共用时：" + watch.Elapsed.Hours + "时 " + watch.Elapsed.Minutes + "分 " + watch.Elapsed.Seconds + "秒 " + watch.Elapsed.Milliseconds + "毫秒";
                SetOutput(sb);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
                docs = null;
            }
            return docs;
        }

        private TopDocs BooleanQuery(string keyword, string field)
        {
            string[] words = keyword.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            TopDocs docs = null;
            int n = 10;//最多返回多少个结果
            SetOutput(string.Format("正在检索关键字：{0}", keyword));
            try
            {
                BooleanQuery boolQuery = new BooleanQuery();
                Term beginT = new Term("id", "3");
                Term endT = new Term("id", "5");
                RangeQuery rq = new RangeQuery(beginT, endT, true); //rangequery id从3到5
                for (int i = 0; i < words.Length; i++)
                {
                    TermQuery tq = new TermQuery(new Term(field, words[i]));//termquery
                    boolQuery.Add(tq, BooleanClause.Occur.MUST);
                }
                boolQuery.Add(rq, BooleanClause.Occur.MUST);

                Stopwatch watch = new Stopwatch();
                watch.Start();
                docs = searcher.Search(boolQuery, (Filter)null, n);
                watch.Stop();
                StringBuffer sb = "BooleanQuery搜索完成,共用时：" + watch.Elapsed.Hours + "时 " + watch.Elapsed.Minutes + "分 " + watch.Elapsed.Seconds + "秒 " + watch.Elapsed.Milliseconds + "毫秒";
                SetOutput(sb);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
                docs = null;
            }
            return docs;
        }

        private TopDocs PrefixQuery(string keyword, string field)
        {
            TopDocs docs = null;
            int n = 10;//最多返回多少个结果
            SetOutput(string.Format("正在检索关键字：{0}", keyword));
            try
            {
                Term t = new Term(field, keyword);
                PrefixQuery query = new PrefixQuery(t);
                Stopwatch watch = new Stopwatch();
                watch.Start();
                docs = searcher.Search(query, (Filter)null, n);
                watch.Stop();
                StringBuffer sb = "PrefixQuery搜索完成,共用时：" + watch.Elapsed.Hours + "时 " + watch.Elapsed.Minutes + "分 " + watch.Elapsed.Seconds + "秒 " + watch.Elapsed.Milliseconds + "毫秒";
                SetOutput(sb);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
                docs = null;
            }
            return docs;
        }

        private TopDocs FuzzyQuery(string keyword, string field)
        {
            TopDocs docs = null;
            int n = 10;//最多返回多少个结果
            SetOutput(string.Format("正在检索关键字：{0}", keyword));
            try
            {
                Term t = new Term(field, keyword);
                FuzzyQuery query = new FuzzyQuery(t);
                Stopwatch watch = new Stopwatch();
                watch.Start();
                docs = searcher.Search(query, (Filter)null, n);
                watch.Stop();
                StringBuffer sb = "FuzzyQuery搜索完成,共用时：" + watch.Elapsed.Hours + "时 " + watch.Elapsed.Minutes + "分 " + watch.Elapsed.Seconds + "秒 " + watch.Elapsed.Milliseconds + "毫秒";
                SetOutput(sb);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
                docs = null;
            }
            return docs;
        }

        private TopDocs WildcardQuery(string keyword, string field)
        {
            TopDocs docs = null;
            int n = 10;//最多返回多少个结果
            SetOutput(string.Format("正在检索关键字：{0}", keyword));
            try
            {
                Term t = new Term(field, keyword);
                WildcardQuery query = new WildcardQuery(t);
                Stopwatch watch = new Stopwatch();
                watch.Start();
                docs = searcher.Search(query, (Filter)null, n);
                watch.Stop();
                StringBuffer sb = "WildcardQuery搜索完成,共用时：" + watch.Elapsed.Hours + "时 " + watch.Elapsed.Minutes + "分 " + watch.Elapsed.Seconds + "秒 " + watch.Elapsed.Milliseconds + "毫秒";
                SetOutput(sb);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
                docs = null;
            }
            return docs;
        }

        private TopDocs PhraseQuery(string keyword, string field,int slop)
        {
            string[] words = keyword.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            TopDocs docs = null;
            int n = 10;//最多返回多少个结果
            SetOutput(string.Format("正在检索关键字：{0}", keyword));
            try
            {
                PhraseQuery query = new PhraseQuery();
                query.SetSlop(slop);
                foreach (string word in words)
                {
                    Term t = new Term(field, word);
                    query.Add(t);
                }
                Stopwatch watch = new Stopwatch();
                watch.Start();
                docs = searcher.Search(query, (Filter)null, n);
                watch.Stop();
                StringBuffer sb = "PhraseQuery搜索完成,共用时：" + watch.Elapsed.Hours + "时 " + watch.Elapsed.Minutes + "分 " + watch.Elapsed.Seconds + "秒 " + watch.Elapsed.Milliseconds + "毫秒";
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
        /// 是否有输入搜索关键字
        /// </summary>
        /// <returns></returns>
        private bool IsHasKeyword()
        {
            bool flag = string.IsNullOrEmpty(this.txtKeyword.Text.Trim()) ? false : true;
            return flag;
        }

        /// <summary>
        /// 获取搜索关键字
        /// </summary>
        /// <returns></returns>
        private string GetKeyword()
        {
            return this.txtKeyword.Text.Trim();
        }

        private void btnTermQuery_Click(object sender, EventArgs e)
        {
            if (IsHasKeyword() == false)
            {
                return;
            }
            try
            {
                SetOutput(string.Format("==========================TermQuery搜索开始时间：{0}===========================", DateTime.Now.ToString()));
                searcher = new IndexSearcher(INDEX_STORE_PATH); //构建一个索引搜索器
                TopDocs queryResult = TermQuery(GetKeyword(), "contents");//按照内容搜索
                ShowFileSearchResult(queryResult);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        private void btnRangeQuery_Click(object sender, EventArgs e)
        {
            try
            {
                SetOutput(string.Format("==========================RangeQuery搜索开始时间：{0}===========================", DateTime.Now.ToString()));
                searcher = new IndexSearcher(INDEX_STORE_PATH);
                TopDocs queryResult = RangeQuery("id", "3", "5", true);
                ShowFileSearchResult(queryResult);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        private void btnBooleanQuery_Click(object sender, EventArgs e)
        {
            if (IsHasKeyword() == false)
            {
                return;
            }
            try
            {
                SetOutput(string.Format("==========================BooleanQuery搜索开始时间：{0}===========================", DateTime.Now.ToString()));
                searcher = new IndexSearcher(INDEX_STORE_PATH); 
                TopDocs queryResult = BooleanQuery(GetKeyword(), "contents");
                ShowFileSearchResult(queryResult);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        private void btnPrefixQuery_Click(object sender, EventArgs e)
        {
            if (IsHasKeyword() == false)
            {
                return;
            }
            try
            {
                SetOutput(string.Format("==========================PrefixQuery搜索开始时间：{0}===========================", DateTime.Now.ToString()));
                searcher = new IndexSearcher(INDEX_STORE_PATH);
                TopDocs queryResult = PrefixQuery(GetKeyword(), "contents");
                ShowFileSearchResult(queryResult);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        private void btnFuzzyQuery_Click(object sender, EventArgs e)
        {
            if (IsHasKeyword() == false)
            {
                return;
            }
            try
            {
                SetOutput(string.Format("==========================FuzzyQuery搜索开始时间：{0}===========================", DateTime.Now.ToString()));
                searcher = new IndexSearcher(INDEX_STORE_PATH);
                TopDocs queryResult = FuzzyQuery(GetKeyword(), "contents");
                ShowFileSearchResult(queryResult);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        private void btnWildcardQuery_Click(object sender, EventArgs e)
        {
            if (IsHasKeyword() == false)
            {
                return;
            }
            try
            {
                SetOutput(string.Format("==========================WildcardQuery搜索开始时间：{0}===========================", DateTime.Now.ToString()));
                searcher = new IndexSearcher(INDEX_STORE_PATH);
                TopDocs queryResult = WildcardQuery(GetKeyword(), "contents");
                ShowFileSearchResult(queryResult);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        private void btnPhraseQuery_Click(object sender, EventArgs e)
        {
            if (IsHasKeyword() == false)
            {
                return;
            }
            try
            {
                SetOutput(string.Format("==========================PhraseQuery搜索开始时间：{0}===========================", DateTime.Now.ToString()));
                searcher = new IndexSearcher(INDEX_STORE_PATH);
                TopDocs queryResult = PhraseQuery(GetKeyword(), "contents", 1);
                ShowFileSearchResult(queryResult);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        #endregion

        #region 多字段搜索

        /// <summary>
        /// 多字段搜索(以空格，逗号等分隔符隔开)
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private TopDocs MulFieldsSearch(string keyword)
        {
            TopDocs docs = null;
            int n = 100;
            SetOutput("正在检索关键字：" + keyword);
            try
            {
                BooleanClause.Occur[] flags=new BooleanClause.Occur[]{BooleanClause.Occur.MUST,BooleanClause.Occur.MUST};
                string[] fields = new string[] { "id", "contents" };
                string[] values = keyword.Trim().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (fields.Length != values.Length)
                {
                    throw new Exception("字段和对应值不一致");
                }
                //MultiFieldQueryParser parser = new MultiFieldQueryParser(fields, new StandardAnalyzer());
                //parser.SetDefaultOperator(QueryParser.Operator.OR);//或者的关系
                //Query query = parser.Parse(keyword); 
                Query query = MultiFieldQueryParser.Parse(values, fields, flags, new StandardAnalyzer());
                Stopwatch watch = new Stopwatch();
                watch.Start();

                docs = searcher.Search(query, (Filter)null, n); //排序获取搜索结果
                watch.Stop();
                StringBuffer sb = "搜索完成,共用时：" + watch.Elapsed.Hours + "时 " + watch.Elapsed.Minutes + "分 " + watch.Elapsed.Seconds + "秒 " + watch.Elapsed.Milliseconds + "毫秒";
                SetOutput(sb);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
            return docs;
        }

        private void btnMultFields_Click(object sender, EventArgs e)
        {
            if (this.txtMultFields.Text.Trim().Length == 0)
            {
                return;
            }
            try
            {
                SetOutput(string.Format("==========================多字段（id，contents）搜索开始时间：{0}===========================", DateTime.Now.ToString()));
                searcher = new IndexSearcher(INDEX_STORE_PATH);
                TopDocs queryResult = MulFieldsSearch(this.txtMultFields.Text.Trim());
                ShowFileSearchResult(queryResult);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        #endregion

        #region 多索引文件搜索

        /// <summary>
        /// 根据多个索引文件夹搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private TopDocs MultiSearch(string keyword, string field)
        {
            TopDocs docs = null;
            int n = 20;//最多返回多少个结果
            SetOutput(string.Format("正在检索关键字：{0}", keyword));
            Searchable[] abs = new Searchable[2];
            abs[0] = new IndexSearcher(INDEX_STORE_PATH);
            abs[1] = new IndexSearcher(INDEX_STORE_PATH1);
            MultiSearcher searcher = new MultiSearcher(abs);//构造MultiSearcher
            try
            {
                QueryParser parser = new QueryParser(field, new StandardAnalyzer());
                Query query = parser.Parse(keyword);
                Stopwatch watch = new Stopwatch();
                watch.Start();
                docs = searcher.Search(query, (Filter)null, n); //排序获取搜索结果
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
        private void ShowMultFileSearchResult(TopDocs queryResult)
        {
            if (queryResult == null || queryResult.totalHits == 0)
            {
                SetOutput("Sorry，没有搜索到你要的结果。");
                return;
            }
            Searchable[] abs = new Searchable[2];
            abs[0] = new IndexSearcher(INDEX_STORE_PATH);
            abs[1] = new IndexSearcher(INDEX_STORE_PATH1);
            MultiSearcher searcher = new MultiSearcher(abs);//构造MultiSearcher
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

        private void btnMultSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtMultiSearchKeyword.Text.Trim()))
            {
                return;
            }
            try
            {
                SetOutput(string.Format("==========================搜索开始时间：{0}===========================", DateTime.Now.ToString()));
                TopDocs queryResult = MultiSearch(this.txtMultiSearchKeyword.Text.Trim(), "contents");//按照内容搜索
                ShowMultFileSearchResult(queryResult);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        #endregion

        #region 排序搜索

        /// <summary>
        /// 根据索引排序搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private TopDocs SortSearch(string keyword, string field)
        {
            TopDocs docs = null;
            int n = 10;//最多返回多少个结果
            SetOutput(string.Format("正在检索关键字：{0}", keyword));
            try
            {
                QueryParser parser = new QueryParser(field, new StandardAnalyzer());//针对内容查询
                Query query = parser.Parse(keyword);//搜索内容 contents  (用QueryParser.Parse方法实例化一个查询)
                Stopwatch watch = new Stopwatch();
                bool sortDirection = true;

                if (chkIsSortById.Checked == true)//按照id升序
                {
                    sortDirection = false;
                }
                watch.Start();
                Sort sort = new Sort();
                SortField sf = new SortField("id", SortField.INT, sortDirection);//按照id字段排序，false表示升序,ture表示逆序
                //SortField sf = new SortField("filename", SortField.DOC, false);//按照filename字段排序，false表示升序

                sort.SetSort(sf);

                //多个条件排序
                //Sort sort = new Sort();
                //SortField f1 = new SortField("id", SortField.INT, false);
                //SortField f2 = new SortField("filename", SortField.DOC, false);
                //sort.SetSort(new SortField[] { f1, f2 });
                docs = searcher.Search(query, (Filter)null, n, sort); //排序获取搜索结果
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

        private void btnSortSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtSortKeyword.Text.Trim()))
            {
                return;
            }
            try
            {
                SetOutput(string.Format("==========================搜索开始时间：{0}===========================", DateTime.Now.ToString()));
                searcher = new IndexSearcher(INDEX_STORE_PATH); //构建一个索引搜索器
                TopDocs queryResult = SortSearch(this.txtSortKeyword.Text.Trim(), "contents");//按照内容搜索
                ShowFileSearchResult(queryResult);
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        #endregion

        #region 分页搜索

        /// <summary>
        /// 根据索引分页搜索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private TopDocs PagerSearch(string keyword, string field)
        {
            TopDocs docs = null;
            int n = 10000;//最多返回多少个结果
            SetOutput(string.Format("正在检索关键字：{0}", keyword));
            try
            {
                QueryParser parser = new QueryParser(field, new StandardAnalyzer());//针对内容查询
                Query query = parser.Parse(keyword);//搜索内容 contents  (用QueryParser.Parse方法实例化一个查询)
                Stopwatch watch = new Stopwatch();
                watch.Start();
                docs = searcher.Search(query, (Filter)null, n); //排序获取搜索结果
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
        /// 显示分页搜索结果
        /// </summary>
        /// <param name="queryResult"></param>
        private void ShowPagerSearchResult(TopDocs queryResult, int currentPage)
        {
            if (queryResult == null || queryResult.totalHits == 0)
            {
                SetOutput("Sorry，没有搜索到你要的结果。");
                return;
            }

            int counter = 1;
            int start = (currentPage - 1) * recordsPerPage;//开始记录
            int end = currentPage * recordsPerPage;//结束记录
            if (end > queryResult.totalHits)
            {
                end = queryResult.totalHits;
            }
            for (int i = start; i < end; i++)
            {
                ScoreDoc sd = queryResult.scoreDocs[i];
                try
                {
                    Document doc = searcher.Doc(sd.doc);
                    string id = doc.Get("id");//获取id
                    string fileName = doc.Get("filename");//获取文件名
                    string contents = doc.Get("contents");//获取文件内容
                    string result = string.Format("这是第{0}页第{1}个搜索结果,Id为{2},文件名为:{3}，文件内容为:{4}{5}", currentPage, counter, id, fileName, Environment.NewLine, contents);
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
        /// 将搜索结果分页显示
        /// </summary>
        /// <param name="currentPage"></param>
        private void BindPagerResults(int currentPage)
        {
            searcher = new IndexSearcher(INDEX_STORE_PATH); //构建一个索引搜索器
            TopDocs queryResult = PagerSearch(this.txtPagerKeyword.Text.Trim(), "contents");//按照内容搜索 
            int totalCount = queryResult.totalHits;//总记录数
            ShowPagerSearchResult(queryResult, currentPage);
            if (totalCount > 0)
            {
                this.panelPager.Visible = true;
                //绑定页码相关信息
                PagerControl pager = new PagerControl(currentPage, recordsPerPage, totalCount, "跳转");
                pager.currentPageChanged += new EventHandler(pager_currentPageChanged);//页码变化 触发的事件
                this.panelPager.Controls.Add(pager);//在Panel容器中加入分页控件
            }
        }

        private void pager_currentPageChanged(object sender, EventArgs e)
        {
            PagerControl pager = sender as PagerControl;
            if (pager == null || string.IsNullOrEmpty(this.txtPagerKeyword.Text.Trim()))
            {
                return;
            }
            SetOutput(string.Format("==========================分页搜索开始时间：{0}===========================", DateTime.Now.ToString()));
            currentPage = pager.CurrentPage;
            BindPagerResults(currentPage);//查询数据并分页绑定
        }

        private void btnPagerSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtPagerKeyword.Text.Trim()))
            {
                return;
            }
            try
            {
                SetOutput(string.Format("==========================搜索开始时间：{0}===========================", DateTime.Now.ToString()));
                currentPage = 1;
                BindPagerResults(currentPage);//显示分页搜索结果
            }
            catch (Exception ex)
            {
                SetOutput(ex.Message);
            }
        }

        #endregion
    }
}
