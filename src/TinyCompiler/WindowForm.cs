using System.Windows.Forms;

namespace TinyCompiler
{
    public partial class WindowForm : Form
    {
        public WindowForm()
        {
            InitializeComponent();
        }

        private void btnCompile_Click(object sender, System.EventArgs e)
        {
            string sourceCode = tfSourceCode.Text;
            Compiler.Compile(sourceCode);

            PopulateTokensTable();
            PrintErrors();
        }

        private void PopulateTokensTable()
        {
            tblTokens.Rows.Clear();
            foreach (var token in Compiler.Scanner.Tokens)
            {
                tblTokens.Rows.Add(token.lex, token.type);
            }
        }

        private void PrintErrors()
        {
            tfErrors.Clear();
            foreach (var error in Errors.Error_List)
            {
                tfErrors.Text += error + "\r\n";
            }
        }
    }
}
