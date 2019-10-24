using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_through_Binary_Tree
{
    public class Expression
    {
        public virtual double getValue(params ExprVarValue[] vars)
        {
            return 0.0;
        }
    }

    public class ExprValue : Expression
    {
        double m_Value;
        public override double getValue(params ExprVarValue[] vars)
        {
            return m_Value;
        }
        public ExprValue(double x)
        {
            m_Value = x;
        }
        public override string ToString()
        {
            return m_Value.ToString();
        }
    }

    public class ExprVariable : Expression
    {
        string Name;
        public ExprVariable(string Name)
        {
            this.Name = Name;
        }
        public override double getValue(params ExprVarValue[] vars)
        {
            return vars.First(x => x.Name == Name).Value.getValue();
        }
        public override string ToString()
        {
            return Name.ToString();
        }
    }
    public struct ExprVarValue
    {
        public string Name;
        public ExpressionTree Value;
        public ExprVarValue(string Name, double Value)
        {
            this.Name = Name;
            this.Value = new ExpressionTree(null, new ExprValue(Value));
        }
        public ExprVarValue(string Name, ExpressionTree Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        public static ExprVarValue Parse(string s)
        {
            ExprVarValue res;

            int i = 0;
            Buttons ButtonType;
            string button;

            button = ButtonFunctions.ParseNextButton(s, ref i, out ButtonType);
            if (ButtonType != Buttons.Letter)
                throw new Exception();
            res.Name = button;

            button = ButtonFunctions.ParseNextButton(s, ref i, out ButtonType);
            if (button != "=")
                throw new Exception();

            res.Value = ExpressionTree.Build(s, ref i);

            return res;
        }
        public override string ToString()
        {
            return Name.ToString() + "= " + Value.ToString();
        }
    }

    public class ExprOperations
    {
        public int Priority { get; private set; }
        public int Type { get; private set; }
        public Func<double, double, double> Function { get; private set; }
        public string Description;

        public static ExprOperations NoneOperations;

        static ExprOperations()
        {
            NoneOperations = new ExprOperations(' ');
        }

        public ExprOperations(char Operation)
        {
            Description = Operation.ToString();
            Type = 2;
            switch (Operation)
            {
                case
                    '+': Priority = 1; Function = ((x, y) => x + y);
                    break;
                case
                    '-': Priority = 1; Function = ((x, y) => x - y);
                    break;
                case
                    '*': Priority = 2; Function = ((x, y) => x * y);
                    break;
                case
                    '/': Priority = 2; Function = ((x, y) => x / y);
                    break;
                case
                    '^': Priority = 3; Function = ((x, y) => Math.Pow(x,y));
                    break;
                case
                    ' ': Priority = 0; Function = ((x, y) => x);
                    break;
                default: throw new Exception("AAA EPIC FAIL in ExprOperations.ExprOperations(char Operation)");
            }        
        }

        public ExprOperations(string function)
        {
            Type = 1;
            switch (function)
            {
                case
                    "abs": Function = ((x, y) => Math.Abs(x));
                    break;
                case
                    "sin": Function = ((x, y) => Math.Sin(x));
                    break;
                case
                    "cos": Function = ((x, y) => Math.Cos(x));
                    break;
                case
                    "sqrt": Function = ((x, y) => Math.Sqrt(x));
                    break;
                case
                    "pi": Function = ((x, y) => Math.PI); Type = 0;
                    break;
                default: throw new Exception("AAAA EPIC FAIL in ExprOperations.ExprOperations(char Operation)");
            }

            Description = function;
            Priority = 4;
        }

        public override string ToString()
        {
            return Description.ToString(); 
        }
    }
}
