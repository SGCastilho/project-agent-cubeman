using System;
using UnityEngine;

namespace Cubeman.Utilities
{
    public sealed class CSVReader
    {
        public CSVReader(TextAsset csvFile)
        {
            _csvToRead = csvFile;
        }

        private TextAsset _csvToRead;

        private char[] _charToReplace = { ':', '&', '|' };
        private char[] _replaceChar = { ',', ':', '&' };

        public string[] Read()
        {
            if (_csvToRead == null)
                return null;

            var data = _csvToRead.text.Split(new string[] { ";" , "," , "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var fixString = new string[data.Length];

            fixString = data;

            for(int i = 0; i < fixString.Length; i++)
            {
                if (fixString[i].Contains(_charToReplace[0]))
                {
                    string newString = fixString[i];

                    fixString[i] = newString.Replace(_charToReplace[0], _replaceChar[0]);
                }

                if (fixString[i].Contains(_charToReplace[1]))
                {
                    string newString = fixString[i];

                    fixString[i] = newString.Replace(_charToReplace[1], _replaceChar[1]);
                }

                if (fixString[i].Contains(_charToReplace[2]))
                {
                    string newString = fixString[i];

                    fixString[i] = newString.Replace(_charToReplace[2], _replaceChar[2]);
                }
            }

            return fixString;
        }
    }
}