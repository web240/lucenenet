using System;
using System.Collections.Generic;
using System.Text;

namespace QueryApp
{
    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.PanGu;
    using Lucene.Net.Documents;
    using Lucene.Net.Index;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Search;
    using Lucene.Net.Util;
    using PanGu;

    public class LuceneSearch
    {

        #region 搜索表达式

        public static void NormalQueryParserTest(Analyzer analyzer, string field, string keyword)
        {
            QueryParser parser = new QueryParser(Version.LUCENE_29, field, analyzer);
            Query query = parser.Parse(keyword);
            ShowQueryExpression(analyzer, query, keyword);
            SearchToShow(query);
            Console.WriteLine();
        }

        public static void TermQueryTest(Analyzer analyzer, string field, string keyword)
        {
            Console.WriteLine("====TermQuery====");
            TermQuery query = new TermQuery(new Term(field, keyword));
            ShowQueryExpression(analyzer, query, keyword);
            SearchToShow(query);
            Console.WriteLine();
        }

        public static void BooleanQueryTest(Analyzer analyzer, string field, string keyword, BooleanClause.Occur[] flags)
        {
            Console.WriteLine("====BooleanQuery====");
            string[] arrKeywords = keyword.Trim().Split(new char[] { ' ', ',', '，', '、' }, StringSplitOptions.RemoveEmptyEntries);
            QueryParser parser = new QueryParser(Version.LUCENE_29, field, analyzer);
            BooleanQuery bq = new BooleanQuery();
            int counter = 0;
            foreach (string item in arrKeywords)
            {
                Query query = parser.Parse(item);
                bq.Add(query, flags[counter]);
                counter++;
            }
            ShowQueryExpression(analyzer, bq, keyword);
            SearchToShow(bq);
            Console.WriteLine();
        }

        public static void RangeQueryTest(Analyzer analyzer, string field, string lowTerm, string upperTerm)
        {
            Console.WriteLine("====RangeQuery====");
            Query query = new TermRangeQuery(field, lowTerm, upperTerm,true, true);
            Console.WriteLine("{0},inclusive start:{1} end:{2}", analyzer.GetType().Name, lowTerm, upperTerm);
            Console.WriteLine(query.ToString());
            Console.WriteLine();
            query = new TermRangeQuery(field, lowTerm, upperTerm, true, false);
            Console.WriteLine("{0},not inclusive start:{1} end:{2}", analyzer.GetType().Name, lowTerm, upperTerm);
            Console.WriteLine(query.ToString());
            ShowQueryExpression(analyzer, query, string.Format("{0} {1}",lowTerm,upperTerm));
            SearchToShow(query);
            Console.WriteLine();
        }

        public static void PrefixQueryTest(Analyzer analyzer, string field, string keyword)
        {
            Console.WriteLine("====PrefixQuery====");
            PrefixQuery query = new PrefixQuery(new Term(field, keyword));
            ShowQueryExpression(analyzer, query, keyword);
            SearchToShow(query);
            Console.WriteLine();
        }

        public static void WildcardQueryTest(Analyzer analyzer, string field, string keyword)
        {
            Console.WriteLine("====WildcardQuery====");
            WildcardQuery query = new WildcardQuery(new Term(field, keyword));
            ShowQueryExpression(analyzer, query, keyword);
            SearchToShow(query);
            Console.WriteLine();
        }

        public static void FuzzyQueryTest(Analyzer analyzer, string field, string keyword)
        {
            Console.WriteLine("====FuzzyQuery====");
            FuzzyQuery query = new FuzzyQuery(new Term(field, keyword));
            ShowQueryExpression(analyzer, query, keyword);
            SearchToShow(query);
            Console.WriteLine();
        }

        public static void PhraseQueryTest(Analyzer analyzer, string field, string keyword,int slop)
        {
            Console.WriteLine("====PhraseQuery====");
            string[] arr = keyword.Trim().Split(new char[] { ' ', ',', '，', '、' }, StringSplitOptions.RemoveEmptyEntries);
            PhraseQuery query = new PhraseQuery();
            foreach (string item in arr)
            {
                query.Add(new Term(field, item));
            }
            query.SetSlop(slop);
            ShowQueryExpression(analyzer, query, keyword);
            SearchToShow(query);
            Console.WriteLine();
        }

        public static void MulFieldsSearchTest(Analyzer analyzer, string[] fields, string keyword, BooleanClause.Occur[] flags)
        {
            Console.WriteLine("====MultiFieldQueryParser====");
            MultiFieldQueryParser parser = new MultiFieldQueryParser(Version.LUCENE_29, fields, analyzer);
            //Query query = parser.Parse(keyword);
            Query query = MultiFieldQueryParser.Parse(Version.LUCENE_29, keyword, fields, flags, analyzer);
            ShowQueryExpression(analyzer, query, keyword);
            SearchToShow(query);
            Console.WriteLine();
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 显示搜索表达式
        /// </summary>
        /// <param name="analyzer"></param>
        /// <param name="query"></param>
        /// <param name="keyword"></param>
        private static void ShowQueryExpression(Analyzer analyzer,Query query,string keyword)
        {
            Console.WriteLine("{0},Current Keywords:{1}", analyzer.GetType().Name, keyword);
            Console.WriteLine(query.ToString()); //显示搜索表达式
        }

        /// <summary>
        /// 搜索并显示结果
        /// </summary>
        /// <param name="query"></param>
        private static void SearchToShow(Query query)
        {
            int n = 10;//最多返回多少个结果  
            TopDocs docs = Config.GenerateSearcher().Search(query, (Filter)null, n);
            ShowSearchResult(docs);
        }

        /// <summary>
        /// 显示搜索结果
        /// </summary>
        /// <param name="queryResult"></param>
        private static void ShowSearchResult(TopDocs queryResult)
        {
            if (queryResult == null || queryResult.totalHits == 0)
            {
                Console.WriteLine("Sorry，没有搜索到你要的结果。");
                return;
            }

            int counter = 1;
            foreach (ScoreDoc sd in queryResult.scoreDocs)
            {
                try
                {
                    Document doc =Config.GenerateSearcher().Doc(sd.doc);
                    string title = doc.Get("title");
                    string contents = doc.Get("contents");
                    string createdate = doc.Get("createdate");
                    string result = string.Format("这是第{0}个搜索结果,title为{1},createdate:{2}，content:{3}{4}", counter, title, createdate, Environment.NewLine, contents);
                    Console.WriteLine();
                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                counter++;
            }
        }

       #endregion

        #region 盘古分词搜索

        public static void PanguQueryTest(Analyzer analyzer, string field, string keyword)
        {
            QueryParser parser = new QueryParser(Version.LUCENE_29, field, analyzer);
            string panguQueryword = GetKeyWordsSplitBySpace(keyword, new PanGuTokenizer());//对关键字进行分词处理
            Query query = parser.Parse(panguQueryword);
            ShowQueryExpression(analyzer, query, keyword);
            SearchToShow(query);
            Console.WriteLine();
        }

        public static string GetKeyWordsSplitBySpace(string keywords, PanGuTokenizer ktTokenizer)
        {
            StringBuilder result = new StringBuilder();
            ICollection<WordInfo> words = ktTokenizer.SegmentToWordInfos(keywords);
            foreach (WordInfo word in words)
            {
                if (word == null)
                {
                    continue;
                }
                result.AppendFormat("{0}^{1}.0 ", word.Word, (int)Math.Pow(3, word.Rank));
            }
            return result.ToString().Trim();
        }

        #endregion
    }
}
