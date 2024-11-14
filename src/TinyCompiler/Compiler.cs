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
            Errors.Error_List.Clear();

            //Scanner
            Scanner.Scan(sourceCode);

            //Parser
            //Sematic Analysis
        }
    }
}
