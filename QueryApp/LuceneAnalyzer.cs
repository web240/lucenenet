using System;
using System.Collections.Generic;
using System.IO;

namespace QueryApp
{
    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.PanGu;

    public class LuceneAnalyzer
    {

        /// <summary>
        /// 构造常见的几种Analyzer列表
        /// </summary>
        /// <returns></returns>
        public static IList<Analyzer> BuildAnalyzers()
        {
            IList<Analyzer> listAnalyzer = new List<Analyzer>()
            {
                new PanGuAnalyzer(),//盘古分词器  provide by eaglet http://pangusegment.codeplex.com/
                //new StandardAnalyzer(Version.LUCENE_29),
                //new WhitespaceAnalyzer(),
                //new KeywordAnalyzer(),
                //new SimpleAnalyzer(),
                //new StopAnalyzer(Version.LUCENE_29),
            };
            return listAnalyzer;
        }

        /// <summary>
        /// 测试不同的Analyzer分词效果
        /// </summary>
        /// <param name="listAnalyzer"></param>
        /// <param name="input"></param>
        public static void TestAnalyzer(IList<Analyzer> listAnalyzer, string input)
        {
            foreach (Analyzer analyzer in listAnalyzer)
            {
                Console.WriteLine(string.Format("{0}:", analyzer.ToString()));

                using (TextReader reader = new StringReader(input))
                {
                    TokenStream stream = analyzer.ReusableTokenStream(string.Empty, reader);
                    Lucene.Net.Analysis.Token token = null;
                    while ((token = stream.Next()) != null)
                    {
                        Console.WriteLine(token.TermText());
                    }
                }

                Console.WriteLine();
            }
        }

    }
}