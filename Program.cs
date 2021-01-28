using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeTestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> Test = new BinaryTree<int>(BinaryTree<int>.CompareFunction_Int);

            // Строим дерево
            Console.WriteLine();
           Test.Add(5);
            Test.Add(2);
            Test.Add(1);
            Test.Add(3);
            Test.Add(3); // Для теста еще разок
            Test.Add(4);
            BinaryTree<int>.Insert(Test, 999);
            Test.Add(6);
            Test.Add(10);
            Test.Add(7);
            Test.Add(8);
            Test.Add(9);

            // Поиск значений
            for (int Lp = 1; Lp < 12; Lp++)
                Console.WriteLine("({0}) = {1}", Lp, Test.Find(Lp));

            // Не находит значение по Insert -   я не понял почему
            Console.WriteLine("(999) = {0}", Test.Find(999));
            Console.WriteLine();

            foreach (int value in Test)
            {
                Console.WriteLine("Value: {0}", value);
            }
            Console.ReadLine();
        }

       
        
    }
}
        
    
