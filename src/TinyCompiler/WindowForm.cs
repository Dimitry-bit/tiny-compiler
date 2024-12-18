using System.Drawing;
using System;
using System.Windows.Forms;

namespace TinyCompiler
{
    public partial class WindowForm : Form
    {
        public WindowForm()
        {
            InitializeComponent();

            // Subscribe to the necessary events
            tfSourceCode.TextChanged += (object sender, EventArgs e) => Invalidate();
            tfSourceCode.VScroll += (object sender, EventArgs e) => Invalidate();
            tfSourceCode.KeyDown += RichTextBox1_KeyDown;
        }

        private void RichTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if Ctrl+V is pressed (paste command)
            if (e.Control && e.KeyCode == Keys.V)
            {
                // Check if clipboard contains plain text
                if (Clipboard.ContainsText())
                {
                    // Get plain text from clipboard
                    string plainText = Clipboard.GetText(TextDataFormat.Text);

                    // Insert the plain text into the RichTextBox (at current cursor position)
                    tfSourceCode.SelectedText = plainText;
                }

                // Prevent default paste behavior (i.e., disabling rich text paste)
                e.SuppressKeyPress = true;
            }
        }

        private void btnCompile_Click(object sender, System.EventArgs e)
        {
            string sourceCode = tfSourceCode.Text;
            Compiler.Compile(sourceCode);

            PopulateTokensTable();
            PopulateParseTree();
            PrintErrors();
        }

        private void PopulateTokensTable()
        {
            tblTokens.Rows.Clear();
            foreach (var token in Compiler.TokenStream)
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

        private void PopulateParseTree()
        {
            treePrase.Nodes.Clear();
            treePrase.Nodes.Add(PrintParaseTree(Compiler.treeRoot));
        }

        private void btnClear_Click(object sender, System.EventArgs e)
        {
            // Resest UI Elements
            tfSourceCode.Clear();
            tfErrors.Clear();
            tblTokens.Rows.Clear();
            treePrase.Nodes.Clear();

            // Reset Global Data
            Compiler.TokenStream.Clear();
            Errors.Error_List.Clear();
        }

        private static TreeNode PrintParaseTree(Node root)
        {
            TreeNode tree = new TreeNode("Parse Tree");
            TreeNode treeRoot = PrintTree(root);

            if (treeRoot != null)
            {
                tree.Nodes.Add(treeRoot);
            }

            return tree;
        }

        private static TreeNode PrintTree(Node root)
        {
            if ((root == null) || (root.Name == null))
            {
                return null;
            }

            TreeNode tree = new TreeNode(root.Name);
            foreach (Node child in root.Children)
            {
                if (child == null)
                {
                    continue;
                }

                tree.Nodes.Add(PrintTree(child));
            }

            return tree;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawLineNumbers(e.Graphics);
        }

        private void DrawLineNumbers(Graphics graphics)
        {
            // Set the font and brush for the line numbers
            Font lineNumberFont = new Font("Consolas", 10);
            Brush lineNumberBrush = Brushes.Gray;

            // Calculate the height of a single line based on the font
            // Use GetLineFromCharIndex and GetPositionFromCharIndex to get the correct line height
            int lineHeight =  tfSourceCode.GetPositionFromCharIndex(tfSourceCode.GetFirstCharIndexFromLine(1)).Y
                          - tfSourceCode.GetPositionFromCharIndex(tfSourceCode.GetFirstCharIndexFromLine(0)).Y;

            // Get the first and last visible lines
            int firstVisibleLine = tfSourceCode.GetLineFromCharIndex(tfSourceCode.GetCharIndexFromPosition(new Point(0, 4)));
            int lastVisibleLine = tfSourceCode.GetLineFromCharIndex(tfSourceCode.GetCharIndexFromPosition(new Point(0, tfSourceCode.ClientRectangle.Bottom)));

            // Get the position of the RichTextBox control (Top is the distance from the top of the form)
            int richTextBoxTop = tfSourceCode.Top;
            int richTextBoxLeft = tfSourceCode.Left;

            // Set the initial position for drawing the line numbers
            // Align with the left of the RichTextBox
            int xPosition = richTextBoxLeft;
            int yPosition = richTextBoxTop + tfSourceCode.GetPositionFromCharIndex(tfSourceCode.GetFirstCharIndexFromLine(firstVisibleLine)).Y;

            // Draw the line numbers for all visible lines
            for (int i = firstVisibleLine; i <= lastVisibleLine; i++)
            {
                SizeF size = graphics.MeasureString((i + 1).ToString(), lineNumberFont);
                graphics.DrawString((i + 1).ToString(), lineNumberFont, lineNumberBrush, xPosition - size.Width, yPosition);
                yPosition += lineHeight;  // Move to the next line position
            }
        }
    }
}
