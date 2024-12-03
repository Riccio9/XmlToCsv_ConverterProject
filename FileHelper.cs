using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversionXmlToCsv
{
    public static class FileHelper
    {
        public static bool IsValidFile(string filePath, string expectedExtension)
        {
            return !string.IsNullOrEmpty(filePath) &&
                   File.Exists(filePath) &&
                   Path.GetExtension(filePath).Equals(expectedExtension, StringComparison.OrdinalIgnoreCase);
        }

        public static string GetCsvFilePath(string xmlFilePath)
        {
            return Path.ChangeExtension(xmlFilePath, ".csv");
        }
    }

}
