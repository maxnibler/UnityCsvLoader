using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;


namespace CSVInternal
{
    public static class CSV
    {
        public static List<string> LoadCSV(string filePath)
        {
            string path = ENV.PATH()+filePath;
            List<string> csvData = new List<string>();

            using (StreamReader sr = new StreamReader(path))
            {
                string newLine;
                while ((newLine = sr.ReadLine()) != null)
                {
                    csvData.Add(newLine);
                }
            }
            return csvData;
        }
        public static List<Dictionary<string, string>> DataToDicts(List<string> rawData)
        {
            List<Dictionary<string, string>> csvData = new List<Dictionary<string, string>>();
            string keyString = rawData[0];
            rawData.RemoveAt(0);
            string[] keys = split(keyString);
            checkKeys(keys);

            for (int i=0; i<rawData.Count; i++)
            {
                string[] values = split(rawData[i]);
                if (values.Length != keys.Length)
                {
                    throw new Exception(string.Format(
                        "Number of values in row {0} did not match the number of keys: {1}",
                        i, keys.Length
                    ));
                }
                Dictionary<string, string> dataLine = toDict(keys, values);
                csvData.Add(dataLine);
            }
            return csvData;
        }

        private static Dictionary<string, string> toDict(string[] keys, string[] values)
        {
            Dictionary<string, string> dataLine = new Dictionary<string, string>();
            for (int i=0; i<keys.Length; i++)
            {
                dataLine.Add(keys[i], values[i]);
            }
            return dataLine;
        }

        private static string[] split(string line)
        {
            return line.Split(',');
        }

        private static void checkKeys(string[] keys)
        {
            for (int i=0; i<keys.Length; i++)
            {
                for (int j=i+1; j<keys.Length; j++)
                {
                    if (keys[i] == keys[j])
                    {
                        throw new Exception(string.Format(
                            "Duplicate Keys: {0} found in CSV File",
                            keys[i]
                        ));
                    }
                }
            }
        }

        private static Dictionary<string, string> csvLine(string keyString, string valueString)
        {
            Dictionary<string, string> newLine = new Dictionary<string, string>();
            string[] keys = keyString.Split(',');
            string[] values = valueString.Split(',');
            if (keys.Length != values.Length)
            {
                Debug.LogError("Length of Keys and Values did not match");
                return newLine;
            }
            for (int i=0; i<keys.Length; i++)
            {
                newLine.Add(keys[i], values[i]);
            }
            return newLine;
        }
    }
}
