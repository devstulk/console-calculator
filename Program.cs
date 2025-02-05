using System.Data;
using System.Text.RegularExpressions;

namespace ConsoleCalculator
{
    public class Program()
    {
        private static Queue<string> _history { get; set; } = new Queue<string>();
        private static string _operation { get; set; } = "";
        private static string _result { get; set; } = "";
        private static int _step { get; set; } = 0;

        /**
         * The main.
        */
        static void Main()
        {
            while (true)
                CallStep();
        }

        /**
         * Define which step is called.
        */
        static void CallStep()
        {
            switch (_step)
            {
                case 0:
                    CalculatorTemplate();
                    _step = 1;
                    break;
                case 1:
                    GetOperation();
                    _step = 0;
                    break;
                default:
                    Console.WriteLine("Error: Invalid step.");
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
            }
        }

        /**
         * Build calculator header.
        */
        static void BuildHeader()
        {
            Console.WriteLine(" ========================================== ");
            Console.WriteLine("|                                          |");
            Console.WriteLine("|            CONSOLE CALCULATOR            |");
            Console.WriteLine("|                                          |");
            Console.WriteLine(" ========================================== ");

        }

        /**
         * Build calculator history.
        */
        static void BuildHistory()
        {
            Console.WriteLine("| History                                  |");
            Console.WriteLine("|------------------------------------------|");
            foreach (var item in _history)
                Console.WriteLine($"| {item.PadRight(40)} |");
            Console.WriteLine("|------------------------------------------|");
        }

        /**
         * Build calculator body.
        */
        static void BuildBody()
        {
            Console.WriteLine($"| Operation: {_operation.PadRight(29)} |");
            Console.WriteLine($"| Result: {_result.PadRight(32)} |");
            Console.WriteLine("|------------------------------------------|");
            Console.WriteLine("|          To exit press CTRL + C          |");
            Console.WriteLine(" ========================================== ");
        }

        /**
         * Show calculator template.
        */
        static void CalculatorTemplate()
        {
            Console.Clear();
            BuildHeader();
            BuildHistory();
            BuildBody();
        }

        /**
         * Get operation to calculate. 
        */
        static void GetOperation()
        {
            Console.WriteLine("");
            Console.Write("Enter the operation: ");
            string operation = Console.ReadLine() ?? "";

            if (!IsValidOperation(operation))
            {
                Console.WriteLine("Invalid operation, try again!");
                Thread.Sleep(3000);
                return;
            }

            Calculate(operation);
        }

        /**
         * Calculate operation.
        */
        static void Calculate(string operation)
        {
            string oldResult = _result;

            try
            {
                _result = new DataTable().Compute(operation, string.Empty).ToString() ?? "";
            }
            catch (System.Exception)
            {
                Console.WriteLine("Unable to calculate operation, try again!");
                Thread.Sleep(3000);
                return;
            }

            if (_history.Count == 5)
                _history.Dequeue();

            if (_result.Length > 0 && _operation.Length > 0)
                _history.Enqueue($"{_operation} = {oldResult}");

            _operation = operation;
        }

        /**
         * Check if operation is valid.
        */
        static bool IsValidOperation(string operation)
        {
            string pattern = @"^[0-9]+(\s*[-+*/]\s*[0-9]+|\s*[-+*/]\s*\([0-9]+(\s*[-+*/]\s*[0-9]+)*\))*(\s*[-+*/]\s*[0-9]+|\s*\([0-9]+(\s*[-+*/]\s*[0-9]+)*\))*$"; ;
            return Regex.IsMatch(operation, pattern);
        }
    }
}