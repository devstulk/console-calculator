using System;

namespace ConsoleCalculator
{
    public class Program()
    {
        private static Queue<string> _history { get; set; } = new Queue<string>();
        private static string _operation { get; set; } = "";
        private static string _result { get; set; } = "0";


        static void Main()
        {
            buildHeader();
            buildHistory();
            buildBody();

            // HARD CODE ONLY TEST.
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("Digite o um valor: ");
            Console.ReadLine();
        }

        static void buildHeader()
        {
            Console.WriteLine(" ========================================== ");
            Console.WriteLine("|                                          |");
            Console.WriteLine("|            CONSOLE CALCULATOR            |");
            Console.WriteLine("|                                          |");
            Console.WriteLine(" ========================================== ");
        }

        static void buildHistory()
        {
            // HARD CODE ONLY TEST.
            _history.Enqueue("5+6*7 = 1");
            _history.Enqueue("5+6*7 = 2");
            _history.Enqueue("5+6*7 = 3");
            _history.Enqueue("5+6*7 = 4");
            _history.Enqueue("5+6*7 = 5");
            _history.Dequeue();
            _history.Enqueue("5+6*7 = 6");

            Console.WriteLine("| History                                  |");
            Console.WriteLine("|------------------------------------------|");
            foreach (var item in _history)
                Console.WriteLine($"| {item.PadRight(40)} |");
            Console.WriteLine("|------------------------------------------|");
        }

        static void buildBody()
        {
            Console.WriteLine($"| Operation: {_operation.PadRight(29)} |");
            Console.WriteLine($"| Result: {_operation.PadRight(32)} |");
            Console.WriteLine(" ========================================== ");
        }


        static float getValue()
        {
            return 1.0f;
        }

        static int getOperator()
        {
            return 1;
        }

    }
}