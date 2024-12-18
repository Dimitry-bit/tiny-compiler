using System.Collections.Generic;
using System.Linq;

namespace TinyCompiler
{
    public static class Errors
    {
        public static List<string> Error_List = new List<string>();

        public static void Add(int lineNumber, string msg)
        {
            Error_List.Add($"[Line {lineNumber}]: {msg}.");
        }

        public static bool HasError() => Error_List.Any();
    }
}
