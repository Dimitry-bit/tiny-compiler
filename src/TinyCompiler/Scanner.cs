using System.Collections.Generic;

namespace TinyCompiler
{
    public enum TokenClass
    {
        Begin, Call, Declare, End, Do, Else, EndIf, EndUntil, EndWhile, If, Integer,
        Parameters, Procedure, Program, Read, Real, Set, Then, Until, While, Write,
        Dot, Semicolon, Comma, LParanthesis, RParanthesis, EqualOp, LessThanOp,
        GreaterThanOp, NotEqualOp, PlusOp, MinusOp, MultiplyOp, DivideOp,
        Idenifier, Constant
    }

    public class Token
    {
        public string lex;
        public TokenClass type;
    }

    public class Scanner
    {
        public List<Token> Tokens = new List<Token>();
        readonly Dictionary<string, TokenClass> ReservedWords = new Dictionary<string, TokenClass>();
        readonly Dictionary<string, TokenClass> Operators = new Dictionary<string, TokenClass>();

        public Scanner()
        {
            ReservedWords.Add("IF", TokenClass.If);
            ReservedWords.Add("BEGIN", TokenClass.Begin);
            ReservedWords.Add("CALL", TokenClass.Call);
            ReservedWords.Add("DECLARE", TokenClass.Declare);
            ReservedWords.Add("END", TokenClass.End);
            ReservedWords.Add("DO", TokenClass.Do);
            ReservedWords.Add("ELSE", TokenClass.Else);
            ReservedWords.Add("ENDIF", TokenClass.EndIf);
            ReservedWords.Add("ENDUNTIL", TokenClass.EndUntil);
            ReservedWords.Add("ENDWHILE", TokenClass.EndWhile);
            ReservedWords.Add("INTEGER", TokenClass.Integer);
            ReservedWords.Add("PARAMETERS", TokenClass.Parameters);
            ReservedWords.Add("PROCEDURE", TokenClass.Procedure);
            ReservedWords.Add("PROGRAM", TokenClass.Program);
            ReservedWords.Add("READ", TokenClass.Read);
            ReservedWords.Add("REAL", TokenClass.Real);
            ReservedWords.Add("SET", TokenClass.Set);
            ReservedWords.Add("THEN", TokenClass.Then);
            ReservedWords.Add("UNTIL", TokenClass.Until);
            ReservedWords.Add("WHILE", TokenClass.While);
            ReservedWords.Add("WRITE", TokenClass.Write);

            Operators.Add(".", TokenClass.Dot);
            Operators.Add(";", TokenClass.Semicolon);
            Operators.Add(",", TokenClass.Comma);
            Operators.Add("(", TokenClass.LParanthesis);
            Operators.Add(")", TokenClass.RParanthesis);
            Operators.Add("=", TokenClass.EqualOp);
            Operators.Add("<", TokenClass.LessThanOp);
            Operators.Add(">", TokenClass.GreaterThanOp);
            Operators.Add("!", TokenClass.NotEqualOp);
            Operators.Add("+", TokenClass.PlusOp);
            Operators.Add("-", TokenClass.MinusOp);
            Operators.Add("*", TokenClass.MultiplyOp);
            Operators.Add("/", TokenClass.DivideOp);
        }

        public void Scan(string SourceCode)
        {
            for (int i = 0; i < SourceCode.Length; i++)
            {
                int j = i;
                char c = SourceCode[i];
                string lex = c.ToString();

                if (c == ' ' || c == '\r' || c == '\n')
                    continue;

                if (c >= 'A' && c <= 'z') //if you read a character
                {

                }

                else if (c >= '0' && c <= '9')
                {

                }
                else if (c == '{')
                {

                }
                else
                {

                }
            }

            Compiler.TokenStream = Tokens;
        }

        private void FindTokenClass(string Lex)
        {
            TokenClass tokenClass;
            Token Tok = new Token();
            Tok.lex = Lex;

            //Is it a reserved word?

            //Is it an identifier?

            //Is it a Constant?

            //Is it an operator?

            //Is it an undefined?
        }

        private bool IsIdentifier(string lex)
        {
            bool isValid = true;
            // Check if the lex is an identifier or not.

            return isValid;
        }

        private bool IsConstant(string lex)
        {
            bool isValid = true;
            // Check if the lex is a constant (Number) or not.

            return isValid;
        }
    }
}
