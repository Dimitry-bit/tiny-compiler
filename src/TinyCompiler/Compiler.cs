using System.Collections.Generic;

namespace TinyCompiler
{
    public static class Compiler
    {
        public static Scanner Scanner = new Scanner();
        public static List<string> Lexemes = new List<string>();
        public static List<Token> TokenStream = new List<Token>();

        public static void Compile(string sourceCode)
        {
            //Scanner
            Scanner.Scan(sourceCode);

            //Parser
            //Sematic Analysis
        }

        static void SplitLexemes(string sourceCode)
        {
            string[] lexemes = sourceCode.Split(' ');

            for (int i = 0; i < lexemes.Length; i++)
            {
                if (lexemes[i].Contains("\r\n"))
                {
                    lexemes[i] = lexemes[i].Replace("\r\n", string.Empty);
                }

                Lexemes.Add(lexemes[i]);
            }
        }
    }
}
