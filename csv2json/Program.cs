using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace csv2json
{
    class Program
    {
        static void Main(string[] args)
        {
            var csv = new List<string[]>();
            var lines = File.ReadAllLines(@"cid_non_target.csv"); // csv file location

            foreach (string line in lines)
                csv.Add(line.Split(','));

            var properties = lines[0].Split(',');
            var listObjResult = new List<Dictionary<string, string>>();

            for (int i = 1; i < lines.Length; i++)
            {
                var objResult = new Dictionary<string, string>();
                for (int j = 0; j < properties.Length; j++)
                    objResult.Add(properties[j], csv[i][j]);

                listObjResult.Add(objResult);
            }

            var json = JsonConvert.SerializeObject(listObjResult);
            writeToFile(json);
            Console.WriteLine(json);
        }

        private static void writeToFile(string json)
        {
            Console.WriteLine("START");
            FileStream fs = new FileStream("cid_non_target.json", FileMode.CreateNew);
            StreamWriter w = new StreamWriter(fs, Encoding.Default);
            w.WriteLine(json);
            w.Close();
            Console.WriteLine($"END");
        }
    }
}
