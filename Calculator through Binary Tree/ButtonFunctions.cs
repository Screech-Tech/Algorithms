using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_through_Binary_Tree
{
    public enum Buttons
    {
        Number, 
        Letter,
        Operator,
        Bracket,
        Nothing
    }
    public class ButtonFunctions
    {
        public static Buttons ButtonType(char c)
        {
            if (char.IsNumber(c) || c == ',')
                return Buttons.Number;
            if (c == '+' || c == '-' || c == '*' || c == '/' || c == '=' || c == '^')
                return Buttons.Operator;
            if (c == '(' || c == ')')
                return Buttons.Bracket;
            if (char.IsLetter(c))
                return Buttons.Letter;

            return Buttons.Nothing;
        }

        public static void SkipSpaces(string s, ref int i)
        {
            for (; i < s.Length; i++)
                if (s[i] != ' ')
                    break;
        }

        public static string ParseNextButton(string s, ref int i, out Buttons Button)
        {
            SkipSpaces(s, ref i);

            int oldI = i;
            Button = ButtonType(s[i]);

            for (; i < s.Length; i++)
                if (ButtonType(s[i]) != Button)
                    break;
            return (s.Substring(oldI, i - oldI));
        }
    }
}
