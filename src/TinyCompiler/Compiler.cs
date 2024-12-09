using System.Collections.Generic;
using System.Linq;

namespace TinyCompiler
{
    public static class Compiler
    {
        public static Scanner Scanner = new Scanner();
        public static Parser Parser = new Parser();
        public static List<string> Lexemes = new List<string>();
        public static List<Token> TokenStream = new List<Token>();
        public static Node treeRoot;

        public static void Compile(string sourceCode)
        {
            Errors.Error_List.Clear();
            TokenStream.Clear();

            //Scanner
            TokenStream = Scanner.Scan(sourceCode);

            //Parser
            Parser.Parse(TokenStream);
            treeRoot = Parser.root;

            //Sematic Analysis
        }
    }
}
