using System;
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


        static void Main()
        {
            while (true)
                callStep();
        }

        static void callStep()
        {
            switch (_step)
            {
                case 0:
                    calculatorTemplate();
                    _step = 1;
                    break;
                case 1:
                    getValue();
                    _step = 0;
                    break;
                default:
                    Console.WriteLine("Error: Invalid step.");
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
            }
        }

        static void buildHeader()
        {
            Console.WriteLine(" ========================================== ");
            Console.WriteLine("|                                          |");
            Console.WriteLine("|            CONSOLE CALCULATOR            |");
            Console.WriteLine("|                                          |");
            Console.WriteLine(" ========================================== ");
            Console.WriteLine("|          To exit press CTRL + C          |");
            Console.WriteLine(" ========================================== ");
        }

        static void calculatorTemplate()
        {
            buildHeader();
            buildHistory();
            buildBody();
        }

        static void buildHistory()
        {
            Console.WriteLine("| History                                  |");
            Console.WriteLine("|------------------------------------------|");
            foreach (var item in _history)
                Console.WriteLine($"| {item.PadRight(40)} |");
            Console.WriteLine("|------------------------------------------|");
        }

        static void buildBody()
        {
            Console.WriteLine($"| Operation: {_operation.PadRight(29)} |");
            Console.WriteLine($"| Result: {_result.PadRight(32)} |");
            Console.WriteLine(" ========================================== ");
        }

        static void getValue()
        {
            Console.WriteLine("");
            Console.Write("Enter the operation: ");
            string operation = Console.ReadLine() ?? "";

            if (!IsValidOperation(operation))
            {
                Console.WriteLine("Invalid operation, try again!");
                getValue();
            }

            calculate(operation);
        }

        static void calculate(string operation)
        {
            if (_history.Count == 5)
                _history.Dequeue();

            if (_result.Length > 0 && _operation.Length > 0)
                _history.Enqueue($"{_operation} + {_result}");

            _operation = operation;

            try
            {
                _result = new DataTable().Compute(operation, string.Empty).ToString() ?? "";
            }
            catch (System.Exception)
            {
                Console.WriteLine("Unable to calculate operation, try again!");
                getValue();
            }
        }

        static bool IsValidOperation(string operation)
        {
            string pattern = @"^[0-9]+(\s*[-+*/]\s*[0-9]+|\s*[-+*/]\s*\([0-9]+(\s*[-+*/]\s*[0-9]+)*\))*(\s*[-+*/]\s*[0-9]+|\s*\([0-9]+(\s*[-+*/]\s*[0-9]+)*\))*$"; ;
            return Regex.IsMatch(operation, pattern);
        }
    }
}