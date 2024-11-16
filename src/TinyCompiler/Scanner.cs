using System.Collections.Generic;
using System.Windows.Forms;

namespace TinyCompiler
{
    public enum TokenClass
    {
        Undefined,

        // Single character tokens
        LeftBrace, RightBrace, LeftParen, RightParen,
        Minus, Plus, Multiply, Divide,

        // One or two character tokens
        Assign,
        Less, Greater, Equal, NotEqual, And, Or, Comma, Semicolon,

        // Keywords
        Int, Float, String,
        Read, Write, Repeat, Until, If, ElseIf, Else, End, Endl, Then, Return,

        // Literals
        Identifier, Number, StringLiteral,
    }

    public class Token
    {
        public string lex;
        public TokenClass type;
    }

    public class Scanner
    {
        public List<Token> Tokens = new List<Token>();

        private readonly static Dictionary<string, TokenClass> _keywords = new Dictionary<string, TokenClass>{
            {"int", TokenClass.Int},
            {"float", TokenClass.Float},
            {"string", TokenClass.String},
            {"read", TokenClass.Read},
            {"write", TokenClass.Write},
            {"repeat", TokenClass.Repeat},
            {"until", TokenClass.Until},
            {"if", TokenClass.If},
            {"elseif", TokenClass.ElseIf},
            {"else", TokenClass.Else},
            {"end", TokenClass.End},
            {"endl", TokenClass.Endl},
            {"then", TokenClass.Then},
            {"return", TokenClass.Return},
        };

        private string _sourceCode;
        private int _start;
        private int _current;
        private int _linenumber = 1;

        public List<Token> Scan(string sourceCode)
        {
            Tokens.Clear();

            _sourceCode = sourceCode;
            _linenumber = 1;
            _start = 0;
            _current = 0;

            while (!IsEOF())
            {
                _start = _current;
                char c = Read();

                switch (c)
                {
                    case '{': AddToken(TokenClass.LeftBrace); break;
                    case '}': AddToken(TokenClass.RightBrace); break;
                    case '(': AddToken(TokenClass.LeftParen); break;
                    case ')': AddToken(TokenClass.RightParen); break;
                    case ';': AddToken(TokenClass.Semicolon); break;
                    case ',': AddToken(TokenClass.Comma); break;
                    case '-': AddToken(TokenClass.Minus); break;
                    case '+': AddToken(TokenClass.Plus); break;
                    case '*': AddToken(TokenClass.Multiply); break;
                    case '/':
                        if (Match('*'))
                            ReadComment();
                        else
                            AddToken(TokenClass.Divide);
                        break;
                    case ':':
                        if (!Match('='))
                            goto default;
                        AddToken(TokenClass.Assign);
                        break;
                    case '=': AddToken(TokenClass.Equal); break;
                    case '>': AddToken(TokenClass.Greater); break;
                    case '<': AddToken(Match('>') ? TokenClass.NotEqual : TokenClass.Less); break;
                    case '&':
                        if (!Match('&'))
                            goto default;
                        AddToken(TokenClass.And);
                        break;
                    case '|':
                        if (!Match('|'))
                            goto default;
                        AddToken(TokenClass.Or);
                        break;
                    case '"':
                        ReadString();
                        break;

                    // Fall through
                    case '\r':
                    case '\t':
                    case ' ':
                        break;

                    case '\n':
                        _linenumber++;
                        break;

                    default:
                        if (char.IsDigit(c))
                        {
                            ReadNumber();
                        }
                        else if (char.IsLetter(c))
                        {
                            ReadIdentifier();
                        }
                        else
                        {
                            Errors.Add(_linenumber, $"unexpected lexeme '{c}'");
                        }
                        break;
                }
            }

            return Tokens;
        }

        private void ReadIdentifier()
        {
            while (char.IsLetterOrDigit(Peak()))
                Read();

            string lex = _sourceCode.Substring(_start, _current - _start);
            AddToken(_keywords.ContainsKey(lex) ? _keywords[lex] : TokenClass.Identifier);
        }

        private void ReadNumber()
        {
            while (char.IsDigit(Peak()))
                Read();

            if ((Peak() == '.') && char.IsDigit(PeakNext()))
            {
                // Read '.'
                Read();

                while (char.IsDigit(Peak()))
                    Read();
            }

            AddToken(TokenClass.Number);
        }

        private void ReadString()
        {
            char prev = '\0';
            while (!IsEOF())
            {
                if ((Peak() == '"') && (prev != '\\'))
                    break;

                // No multiline strings
                if (Peak() == '\n')
                    break;

                prev = Read();
            }

            if (Match('"'))
                AddToken(TokenClass.StringLiteral);
            else
                Errors.Add(_linenumber, "unterminated string, expected '\"'");
        }

        private void ReadComment()
        {
            while (!IsEOF())
            {
                if ((Peak() == '*') && (PeakNext() == '/'))
                {
                    Read();
                    break;
                }

                if (Read() == '\n')
                    _linenumber++;

            }

            if (Peak() == '/')
                Read();
            else
                Errors.Add(_linenumber, "unterminated comment, expected '*/'");
        }

        private void AddToken(TokenClass type)
        {
            Token token = new Token
            {
                lex = _sourceCode.Substring(_start, _current - _start),
                type = type,
            };

            Tokens.Add(token);
        }

        private char Read() => _sourceCode[_current++];

        private char Peak()
        {
            if (IsEOF())
                return '\0';

            return _sourceCode[_current];
        }

        private char PeakNext()
        {
            if (_current + 1 >= _sourceCode.Length)
                return '\0';

            return _sourceCode[_current + 1];
        }

        private bool Match(char c)
        {
            if (IsEOF())
                return false;

            if (_sourceCode[_current] != c)
                return false;

            _current++;
            return true;
        }

        private bool IsEOF() => (_current >= _sourceCode.Length);
    }
}
