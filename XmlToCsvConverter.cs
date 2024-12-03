using System;
using System.Data;
using System.IO;

public static class XmlToCsvConverter
{
    public static void Convert(string xmlContent, string csvFilePath)
    {
        DataSet dataSet = new DataSet();
        using (StringReader stringReader = new StringReader(xmlContent))
        {
            dataSet.ReadXml(stringReader);
        }

        using (StreamWriter sw = new StreamWriter(csvFilePath))
        {
            foreach (DataTable table in dataSet.Tables)
            {
                // Scrive l'intestazione
                sw.WriteLine(string.Join(",", table.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));

                // Scrive i dati
                foreach (DataRow row in table.Rows)
                {
                    try
                    {
                        string line = string.Join(",", row.ItemArray.Select(field => EscapeCsvField(field.ToString())));
                        sw.WriteLine(line);
                    }
                    catch (Exception ex)
                    {
                        // Log dell'errore
                        LogError($"Errore nella riga: {string.Join(",", row.ItemArray)}. Dettagli: {ex.Message}");
                    }
                }
            }
        }
    }

    private static string EscapeCsvField(string field)
    {
        // Gestisce i campi con virgole o virgolette
        if (field.Contains(",") || field.Contains("\""))
        {
            field = $"\"{field.Replace("\"", "\"\"")}\"";
        }
        return field;
    }

    private static void LogError(string message)
    {
        string logFilePath = "conversion_errors.log";
        File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
    }
}
