using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_through_Binary_Tree
{
    enum Operations
    {
        Empty,
        Plus,
        Minus,
        Mult,
        Div
    }
    public class ExpressionTree : Expression
    {
        Expression Left;
        Expression Right;
        ExpressionTree Parent;
        ExprOperations Operation;

        public ExpressionTree(ExprOperations Operation = null, Expression Left = null, Expression Right = null, ExpressionTree Parent = null)
        {
            if (Operation != null)
                this.Operation = Operation;
            else
                this.Operation = ExprOperations.NoneOperations;
            this.Left = Left;
            this.Right = Right;
            this.Parent = Parent;
        }

        public override double getValue(params ExprVarValue[] vars)
        {
            if (Right != null)
                return Operation.Function(Left.getValue(vars), Right.getValue(vars));
            else if (Left != null)
                return Operation.Function(Left.getValue(vars), 0.0);
            else
                return Operation.Function(0.0, 0.0);
        }

        public ExpressionTree CopyPartial()
        {
            return new ExpressionTree(Operation, Left, Right, Parent);
        }

        private void AddValue(Expression x)
        {
            if (Left == null)
            {
                if (Operation == ExprOperations.NoneOperations)
                    Left = x;
                else
                {
                    Left = new ExprValue(0.0);
                    Right = x;
                }
            }
            else if (Right == null)
                Right = x;
            else
                throw new Exception("AAAA EPIC FAIL in ExpressionTree.AddValue(double x)");
        }
        private void AddValue(double x)
        {
            AddValue(new ExprValue(x));
        }

        private void AddValue(string x)
        {
            AddValue(new ExprVariable(x));
        }

        public static ExpressionTree Build(string s, ref int startPos)
        {
            ExpressionTree tree = new ExpressionTree();
            ExpressionTree tRoot = tree;

            int i = startPos;
            while (i < s.Length)
            {
                Buttons ButtonType;
                string button = ButtonFunctions.ParseNextButton(s, ref i, out ButtonType);
                Console.WriteLine(button);

                if (ButtonType == Buttons.Number)
                    tree.AddValue(double.Parse(button));

                else if (ButtonType == Buttons.Letter && button.Length == 1)
                {
                    tree.AddValue(button);
                }
                else
                {
                    ExprOperations newOperation = ExprOperations.NoneOperations;

                    if (ButtonType == Buttons.Letter && button.Length > 1)
                        newOperation = new ExprOperations(button);
                    else if (ButtonType == Buttons.Operator)
                        newOperation = new ExprOperations(button[0]);

                    if (ButtonType == Buttons.Operator || ButtonType == Buttons.Letter)
                    {
                        if (tree.Operation == ExprOperations.NoneOperations)
                            tree.Operation = newOperation;
                        else if (newOperation.Priority > tree.Operation.Priority)
                            tree.Right = new ExpressionTree(newOperation, tree.Right, null, tree);
                            tree = (ExpressionTree)tree.Right;
                    }
                    else
                    {
                        tree.Left = tree.CopyPartial();
                        tree.Operation = newOperation;
                        tree.Right = null;
                    }
                
                    if (ButtonType == Buttons.Bracket)
                    {
                        if (button[0] == '(')
                        {
                            if (tree.Left == null)
                            {
                                tree.Left = new ExpressionTree(Parent: tree);
                                tree = (ExpressionTree)tree.Left;
                            }
                            else
                            {
                                tree.Right = new ExpressionTree(Parent: tree);
                                tree = (ExpressionTree)tree.Right;
                            }
                        }
                        else if (button[0] == ')')
                            tree = tree.Parent;
                    
                    }
                }
            }
            return tRoot;
        }
        public static ExpressionTree Build(string s)
        {
            int i = 0;
            return Build(s, ref i);
        }

        public override string ToString()
        {
            if (Operation.Type == 0)
            return Operation.Description;

            if (Operation.Type == 1)
                return Operation.Description + "(" + Left.ToString() + ")";

            if (Operation.Description == " ")
                return Left.ToString();

            StringBuilder s = new StringBuilder();

            s.Append("(");

            if (Left != null)
            {
                s.Append(Left.ToString());
            }

            s.Append(Operation.Description);

            if (Right != null)
                s.Append(Right.ToString());

            s.Append(")");

            return s.ToString();
        }
    }
}
