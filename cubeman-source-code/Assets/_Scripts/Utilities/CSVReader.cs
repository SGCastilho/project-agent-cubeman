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

        public string[] Read()
        {
            if (_csvToRead == null)
                return null;

            var data = _csvToRead.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);

            return data;
        }
    }
}