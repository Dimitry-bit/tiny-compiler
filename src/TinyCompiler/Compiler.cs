using System.Collections.Generic;

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
            if (Errors.HasError()) {
                Errors.Error_List.Add($"Compilation failed, found {Errors.Error_List.Count} error{(Errors.Error_List.Count == 1 ? "" : "s")}.");
                return;
            }

            //Parser
            Parser.Parse(TokenStream);
            treeRoot = Parser.root;
            if (Errors.HasError()) {
                Errors.Error_List.Add($"Compilation failed, found {Errors.Error_List.Count} error{(Errors.Error_List.Count == 1 ? "" : "s")}.");
                return;
            }

            //Sematic Analysis
        }
    }
}
