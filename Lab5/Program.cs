using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            DocumentStatistics statistics = new DocumentStatistics();
            string filepath = @"..\..\Documents";
            ProcessFiles(filepath, statistics);
            //SerializeStats(filepath, statistics);
            Console.ReadLine();
        }
        private static void ProcessFiles(string filepath, DocumentStatistics stats)
        {
            string[] files = Directory.GetFiles(filepath);
            foreach (string file in files)
            {
                //add the file name to the stats.documents property.
                //add 1 to the stats.DocumentCount property
                //stream reader

                stats.Documents.Add(file);
                stats.DocumentCount = stats.DocumentCount + 1;

                using (StreamReader sr = File.OpenText(file))
                {
                    string input = null;
                    input = sr.ReadLine();
                    string[] words = Regex.Split(input, @"\s");//s+
                    
                    for (int i = 0; i < words.Length; i++)
                    {
                            //update stats.wordcount dictionary
                            if (stats.WordCounts.ContainsKey(words[i]))
                        {
                            int counter = stats.WordCounts[words[i]];
                            stats.WordCounts[words[i]] = counter + 1;
                        }
                        else
                        {
                            stats.WordCounts.Add(words[i], 1);
                        }
                    }
                }
            }
        }
        
        private static void SerializeStats(string filepath, DocumentStatistics stats)
        {
            using (var stm = new FileStream(filepath + "\\stats.json", FileMode.CreateNew))
            {
                //var settings = new DataContractJsonSerializerSettings();
                var serializer = new DataContractJsonSerializer(typeof(DocumentStatistics));
                serializer.WriteObject(stm, stats);

            }
                //stats = serializer.ReadObject(stm) as DocumentStatistics;

                /*
                using (var stm = new FileStream("stats", FileMode.CreateNew))
                {
                    var settings = new DataContractJsonSerializerSettings();
                    var serializer = new DataContractJsonSerializer(typeof(DocumentStatistics), settings);
                        stats = serializer.ReadObject(stm) as DocumentStatistics;
                }
                 if (stats != null)
                {
                    foreach ()
                */
            }
        
    }
   
}
