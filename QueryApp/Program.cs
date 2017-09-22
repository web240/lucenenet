using System;
using System.Collections.Generic;

namespace QueryApp
{
    using Lucene.Net.Analysis;
    using Lucene.Net.Search;

    /// <summary>
    /// lucene.net 2.9.2
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("创建索引至文件开始");
            bool isPangu = true; //是否按照盘古分词器进行分词
            LuceneIndex.PrepareIndex(isPangu);//创建索引，保存在应用程序的index文件夹下
            Console.WriteLine(string.Format("{0}索引创建成功", isPangu ? "盘古分词" : string.Empty));
            Console.WriteLine();

            string keyword = "测试 博 客 园";//搜索输入关键词
            string field = "contents";//搜索的对应字段
            string[] fieldArr = new string[] { field, "title" };//两个字段
            string rangeField = "createdate";//范围搜索对应字段
            string start = "20101010";
            string end = "20110101";
            IList<Analyzer> listAnalyzer =LuceneAnalyzer. BuildAnalyzers();
            BooleanClause.Occur[] occurs = new BooleanClause.Occur[] { BooleanClause.Occur.MUST, BooleanClause.Occur.SHOULD };
            foreach (Analyzer analyzer in listAnalyzer)
            {

                LuceneSearch.PanguQueryTest(analyzer, field, keyword);//通过盘古分词搜索

                //NormalQueryTest(analyzer);
                //LuceneSearch.NormalQueryParserTest(analyzer, field, keyword);//直接通过QueryParser配合构造好的查询表达式搜索

                //LuceneSearch.TermQueryTest(analyzer, field, "高手");//contents:高手

                //LuceneSearch.BooleanQueryTest(analyzer, field, "jeffreyzhao 老赵", occurs);//+contents:jeffreyzhao +contents:"老 赵"

                //LuceneSearch.RangeQueryTest(analyzer, rangeField, start, end); // createdate:[20101010 TO 20110101]  createdate:[20101010 TO 20110101}

                //LuceneSearch.PrefixQueryTest(analyzer, field, "hell"); // contents:hell*  (可以找到hello world那一项)

                //LuceneSearch.WildcardQueryTest(analyzer, field, "高手"); //contents:高手

                //LuceneSearch.FuzzyQueryTest(analyzer, field, "牛"); //contents:牛~0.5

                //LuceneSearch.PhraseQueryTest(analyzer, field, "hello world", 1); //contents:"hello world"~1

                //LuceneSearch.MulFieldsSearchTest(analyzer, fieldArr, "博  -园", occurs); //+(contents:博 contents:园) +(title:博 title:园)
            }


            Console.WriteLine();
            Console.WriteLine("不同分词器（Analyzer）的分词效果测试开始：");
            string input = "Hello World. 我认识的一个高手，他拥有广博的知识，有极客的态度，还经常到园子里来看看";
            LuceneAnalyzer.TestAnalyzer(listAnalyzer, input);

            Console.ReadKey();
        }

        /// <summary>
        /// 构造几个简单的搜索表达式进行搜索测试(与或非 以及时间范围)
        /// </summary>
        /// <param name="analyzer"></param>
        static void NormalQueryTest(Analyzer analyzer) //StandardAnalyzer
        {
            string keyword = "jeffreyzhao 老赵";//搜索输入关键词
            string field = "contents";//搜索的对应字段
            LuceneSearch.NormalQueryParserTest(analyzer, field, keyword); //contents:jeffreyzhao contents:"老 赵"

            keyword = "+jeffreyzhao 老赵";
            LuceneSearch.NormalQueryParserTest(analyzer, field, keyword);//+contents:jeffreyzhao contents:"老 赵"

            keyword = "+jeffreyzhao +老赵";
            LuceneSearch.NormalQueryParserTest(analyzer, field, keyword);//+contents:jeffreyzhao +contents:"老 赵"


            keyword = "+jeffreyzhao -老赵";
            LuceneSearch.NormalQueryParserTest(analyzer, field, keyword);//+contents:jeffreyzhao -contents:"老 赵"

            keyword = "+jeffreyzhao !老赵";
            LuceneSearch.NormalQueryParserTest(analyzer, field, keyword);//+contents:jeffreyzhao -contents:"老 赵"

            field = "createdate";
            keyword = "[20101010  20110101]";
            LuceneSearch.NormalQueryParserTest(analyzer, field, keyword);//createdate:[20101010 TO 20121212]

            keyword = "{20101010  20110101}";
            LuceneSearch.NormalQueryParserTest(analyzer, field, keyword);//createdate:{20101010 TO 20121212}

        }


    }
}
