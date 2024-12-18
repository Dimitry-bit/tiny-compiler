using System.Collections.Generic;

namespace TinyCompiler
{
    public static class Compiler
    {
        public static List<Token> TokenStream = new List<Token>();
        public static Node treeRoot;

        public static void Compile(string sourceCode)
        {
            Errors.Error_List.Clear();
            TokenStream.Clear();

            //Scanner
            Scanner scanner = new Scanner(sourceCode);
            TokenStream = scanner.Scan();
            if (Errors.HasError()) {
                Errors.Error_List.Add($"Compilation failed, found {Errors.Error_List.Count} error{(Errors.Error_List.Count == 1 ? "" : "s")}.");
                return;
            }

            //Parser
            Parser parser = new Parser(TokenStream);
            treeRoot = parser.Parse();
            if (Errors.HasError()) {
                Errors.Error_List.Add($"Compilation failed, found {Errors.Error_List.Count} error{(Errors.Error_List.Count == 1 ? "" : "s")}.");
                return;
            }

            //Sematic Analysis
        }
    }
}
