using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_through_Binary_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            // sort out more complex examples
            string s = "2+2";
            ExpressionTree e = ExpressionTree.Build(s);
            ExprVarValue[] vars = { new ExprVarValue("x", 2), new ExprVarValue("y", 3) };
            Console.WriteLine(s);
            foreach (ExprVarValue var in vars)
                Console.WriteLine(var);
            Console.Write(e.ToString() + " = ");
            Console.WriteLine(e.getValue(vars));

            Console.ReadKey();
        }
    }
}
