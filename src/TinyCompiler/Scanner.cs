using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TinyCompiler
{
    public enum TokenClass
    {
        Undefined,
        Int, Float, String,
        Read, Write, Repeat, Until, If, ElseIf, Else, End, Endl, Then, Return,
        OP_Minus, OP_Plus, OP_Multiply, OP_Divide, OP_Assign, OP_LE, OP_GE,
        OP_EQ, OP_NOT_EQ, OP_AND, OP_OR, Comma, Semicolon,
        LeftCurlyBraces, RightCurlyBraces, LeftParentheses, RightParentheses,
        Identifier, Number, StringLiteral,
        Main,
    }

    public class Token
    {
        public string lex;
        public TokenClass type;
    }

    public class Scanner
    {
        public List<Token> Tokens = new List<Token>();
        readonly static Dictionary<string, TokenClass> ReservedWords = new Dictionary<string, TokenClass>();
        readonly static Dictionary<string, TokenClass> Operators = new Dictionary<string, TokenClass>();

        public Scanner()
        {
            ReservedWords.Add("int", TokenClass.Int);
            ReservedWords.Add("float", TokenClass.Float);
            ReservedWords.Add("string", TokenClass.String);
            ReservedWords.Add("read", TokenClass.Read);
            ReservedWords.Add("write", TokenClass.Write);
            ReservedWords.Add("repeat", TokenClass.Repeat);
            ReservedWords.Add("until", TokenClass.Until);
            ReservedWords.Add("if", TokenClass.If);
            ReservedWords.Add("elseif", TokenClass.ElseIf);
            ReservedWords.Add("else", TokenClass.Else);
            ReservedWords.Add("end", TokenClass.End);
            ReservedWords.Add("endl", TokenClass.Endl);
            ReservedWords.Add("then", TokenClass.Then);
            ReservedWords.Add("return", TokenClass.Return);
            ReservedWords.Add("main", TokenClass.Main);

            Operators.Add("+", TokenClass.OP_Plus);
            Operators.Add("-", TokenClass.OP_Minus);
            Operators.Add("*", TokenClass.OP_Multiply);
            Operators.Add("/", TokenClass.OP_Divide);
            Operators.Add(":=", TokenClass.OP_Assign);
            Operators.Add("<", TokenClass.OP_LE);
            Operators.Add(">", TokenClass.OP_GE);
            Operators.Add("=", TokenClass.OP_EQ);
            Operators.Add("<>", TokenClass.OP_NOT_EQ);
            Operators.Add("&&", TokenClass.OP_AND);
            Operators.Add("||", TokenClass.OP_OR);
            Operators.Add(",", TokenClass.Comma);
            Operators.Add(";", TokenClass.Semicolon);
            Operators.Add("{", TokenClass.LeftCurlyBraces);
            Operators.Add("}", TokenClass.RightCurlyBraces);
            Operators.Add("(", TokenClass.LeftParentheses);
            Operators.Add(")", TokenClass.RightParentheses);
        }

        public void Scan(string sourceCode)
        {
            Errors.Error_List.Clear();
            Tokens.Clear();

            int lineCount = 1;
            for (int i = 0; i < sourceCode.Length; i++)
            {
                int j = i;
                char c = sourceCode[i];
                string lex = c.ToString();

                if (string.IsNullOrWhiteSpace(lex))
                {
                    if (c == '\n')
                    {
                        lineCount++;
                    }

                    continue;
                }

                // skip comments
                if (c == '/')
                {
                    if ((j + 1 < sourceCode.Length) && (sourceCode[++j] == '*'))
                    {
                        for (j += 1; j < sourceCode.Length; j++)
                        {
                            c = sourceCode[j];

                            if (c == '/' && sourceCode[j - 1] == '*')
                            {
                                break;
                            }
                        }

                        i = j;
                        continue;
                    }
                }

                if (char.IsLetter(c))
                {
                    for (i += 1; i < sourceCode.Length; i++)
                    {
                        if (!char.IsLetterOrDigit(sourceCode[i]))
                        {
                            --i;
                            break;
                        }

                        c = sourceCode[i];
                        lex += c;
                    }
                }
                else if (char.IsDigit(c))
                {
                    for (i += 1; i < sourceCode.Length; i++)
                    {
                        if (!char.IsDigit(sourceCode[i]) && (sourceCode[i] != '.'))
                        {
                            --i;
                            break;
                        }

                        c = sourceCode[i];
                        lex += c;
                    }
                }
                else if (c == '"')
                {
                    for (i += 1; i < sourceCode.Length; i++)
                    {
                        c = sourceCode[i];
                        lex += c;

                        if ((c == '"') && (sourceCode[i - 1] != '\\'))
                        {
                            break;
                        }
                    }
                }
                else if (Operators.Any((op) => (op.Key.Length > 1) && (op.Key.First() == c)))
                {
                    if ((i + 1 < sourceCode.Length) && Operators.ContainsKey(lex + sourceCode[i + 1]))
                    {
                        c = sourceCode[++i];
                        lex += c;
                    }
                }

                if (!FindTokenClass(lex))
                {
                    Errors.Error_List.Add($"line {lineCount}: Undefined lexeme '{lex}'. {(lex.Length == 1 ? $"(ASCII: {(int)lex.First()})" : "")}");
                }
            }

            Compiler.TokenStream = Tokens;
        }

        private bool FindTokenClass(string lex)
        {
            Token token = new Token
            {
                lex = lex,
                type = TokenClass.Undefined
            };

            if (ReservedWords.ContainsKey(lex))
            {
                token.type = ReservedWords[lex];
            }
            else if (Operators.ContainsKey(lex))
            {
                token.type = Operators[lex];
            }
            else if (IsIdentifier(lex))
            {
                token.type = TokenClass.Identifier;
            }
            else if (IsNumber(lex))
            {
                token.type = TokenClass.Number;
            }
            else if (IsStringLiteral(lex))
            {
                token.type = TokenClass.StringLiteral;
            }

            //Is it an undefined?
            if (token.type == TokenClass.Undefined)
            {
                return false;
            }

            Tokens.Add(token);
            return true;
        }

        private bool IsIdentifier(string lex)
        {
            Regex regex = new Regex(@"^[A-Za-z]+[A-Za-z0-9]*$");
            return regex.IsMatch(lex);
        }

        private bool IsNumber(string lex)
        {
            Regex regex = new Regex(@"^[0-9]+(\.[0-9]+)?$");
            return regex.IsMatch(lex);
        }

        private bool IsStringLiteral(string lex)
        {
            Regex regex = new Regex(@"^.*""$");
            return regex.IsMatch(lex);
        }
    }
}
