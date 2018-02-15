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
        #region main
        static void Main(string[] args)
        {
            //Create a document statistics object and then process input files and then write stats as json.
            DocumentStatistics statistics = new DocumentStatistics();
            string filepath = @"..\..\Documents";
            ProcessFiles(filepath, statistics);
            SerializeStats(filepath, statistics);
            Console.WriteLine("Press any key to Exit...");
            Console.ReadLine();
        }
        #endregion

        #region methods
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
                try
                {
                    using (StreamReader sr = File.OpenText(file))
                    {
                        string input = null;
                        input = sr.ReadLine();
                        string[] words = Regex.Split(input, @"\s");//s+

                        for (int i = 0; i < words.Length; i++)
                        {
                            string temp = null;

                            //remove caps
                            temp = Regex.Replace(words[i], @"\w+", m => " " + m.ToString().ToLower());
                            words[i] = temp;
                            temp = Regex.Replace(words[i], @"\s+", "");
                            words[i] = temp;

                            //remove other rando's if they exist
                            if (words[i].Contains("("))
                            {
                                temp = Regex.Replace(words[i], @"\(", "");
                                words[i] = temp;
                            }
                            if (words[i].Contains(")"))
                            {
                                temp = Regex.Replace(words[i], @"\)", "");
                                words[i] = temp;
                            }
                            if (words[i].Contains(":"))
                            {
                                temp = Regex.Replace(words[i], @"\:", "");
                                words[i] = temp;
                            }
                            if (words[i].Contains("."))
                            {
                                temp = Regex.Replace(words[i], @"\.", "");
                                words[i] = temp;
                            }
                            if (words[i].Contains(","))
                            {
                                temp = Regex.Replace(words[i], @"\,", "");
                                words[i] = temp;
                            }

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
                catch 
                {
                    Console.WriteLine("You were not able to read the input files");
                }               
            }
        }

        private static void SerializeStats(string filepath, DocumentStatistics stats)
        {
            try
            {
                using (var stm = new FileStream(filepath + "\\stats.json", FileMode.CreateNew))
                {
                    //var settings = new DataContractJsonSerializerSettings();
                    var serializer = new DataContractJsonSerializer(typeof(DocumentStatistics));
                    serializer.WriteObject(stm, stats);
                }
               
            }

            catch 
            {
                Console.WriteLine("You were not able to create a new file in the target directory");
                
            }
        }
        #endregion
    }
   
}
