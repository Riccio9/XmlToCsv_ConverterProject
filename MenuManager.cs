using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversionXmlToCsv
{


    public class MenuManager
    {
        private int _currentChoice;


        public void Start()
        {
            while (true)
            {
                if (!SetCommand(GetCommand()))
                {
                    Console.WriteLine("Arrivederci!");
                    break;
                }
            }
        }


        public bool SetCommand(int choice)
        {
            _currentChoice = choice;
            switch (_currentChoice)
            {
                case 1:
                    ConvertXmlToCsv();
                    return true;
                case 2:
                    return false; 
                default:
                    Console.WriteLine("Opzione non valida. Riprova.");
                    return true;
            }
        }


        public void Space()
        {

            Console.WriteLine();
            Console.WriteLine();
          

        }

     
        public int GetCommand()
        {
            Console.Clear();
            Console.WriteLine("=== Applicazione di Conversione XML in CSV ===");
            Console.WriteLine("1. Converti XML in CSV");
            Console.WriteLine("2. Esci");
            Console.Write("Scegli un'opzione: ");
            Space();

            string input = Console.ReadLine();

         
            if (int.TryParse(input, out int choice))
            {
                return choice;
            }
            else
            {
                Console.WriteLine("Inserisci un numero valido. Premi un tasto per continuare.");
                Console.ReadKey();
                return 0; 
            }
        }

        private void ConvertXmlToCsv()
        {
            Console.WriteLine("Inserisci il contenuto XML (termina con una riga vuota):");
            string xmlContent = ReadMultilineInput();

            Console.Write("Inserisci il percorso in cui salvare il file CSV: ");
            string csvFilePath = Console.ReadLine();

            try
            {
                XmlToCsvConverter.Convert(xmlContent, csvFilePath);
                Console.WriteLine($"Conversione completata! File CSV salvato in: {csvFilePath}");

                if (File.Exists("conversion_errors.log"))
                {
                    Console.WriteLine("Sono stati riscontrati errori durante la conversione. Controlla 'conversion_errors.log' per i dettagli.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante la conversione: {ex.Message}");
            }

            Console.WriteLine("Premi un tasto per tornare al menù principale.");
            Console.ReadKey();
        }


        private string ReadMultilineInput()
        {
            StringWriter stringWriter = new StringWriter();
            string line;
            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                stringWriter.WriteLine(line);
            }
            return stringWriter.ToString();
        }

    }

}

