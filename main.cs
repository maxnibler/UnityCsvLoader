using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using CSVInternal;

namespace Mnibler
{
    public static class CSVLoader
    {
        public static List<Dictionary<string, string>> LoadCSV(string filePath)
        {
            List<string> rawData = CSV.LoadCSV(filePath);
            foreach(var data in rawData)
            {
                Debug.Log(data);
            }
            List<Dictionary<string, string>> dictData = CSV.DataToDicts(rawData);
            return dictData;
        }
    }
}