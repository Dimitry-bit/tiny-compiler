using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TinyCompiler
{
    public class Node
    {
        public List<Node> Children = new List<Node>();

        public string Name;

        public Node(string name) => Name = name;
    }

    public class Parser
    {
        public Node root;
        private int _inputPointer;
        private List<Token> _tokenStream;

        public Node Parse(List<Token> tokenStreaam)
        {
            _inputPointer = 0;
            _tokenStream = tokenStreaam;
            root = Program();

            return root;
        }

        private Node Program()
        {
            Node n = new Node("Program");

            n.Children.Add(FunctionStatements());
            // n.Children.Add(MainFunction());

            return n;
        }

        #region Function

        private Node MainFunction()
        {
            Node n = new Node("Main Function");

            n.Children.Add(DataType());
            n.Children.Add(Match(TokenClass.Main));
            n.Children.Add(Match(TokenClass.LeftParen));
            n.Children.Add(Match(TokenClass.RightParen));
            n.Children.Add(FunctionBody());

            return n;
        }

        private Node FunctionStatements()
        {
            Node functionStatement = FunctionStatement();
            if (functionStatement == null)
            {
                return null;
            }

            Node n = new Node("Function Statements");
            n.Children.Add(functionStatement);
            n.Children.Add(FunctionStatements());

            return n;
        }

        private Node FunctionStatement()
        {
            Node decl = FunctionDeclaration();
            if (decl == null)
            {
                return null;
            }

            Node n = new Node("Function Statement");
            n.Children.Add(decl);
            n.Children.Add(FunctionBody());

            return n;
        }

        private Node FunctionDeclaration()
        {
            Node dataType = DataType();
            if (dataType == null)
            {
                return null;
            }

            Node n = new Node("Function Declaration");
            n.Children.Add(dataType);
            n.Children.Add(Identifier());
            n.Children.Add(Match(TokenClass.LeftParen));
            n.Children.Add(ParameterList());
            n.Children.Add(Match(TokenClass.RightParen));

            return n;
        }

        private Node FunctionBody()
        {
            Node n = new Node("Function Body");

            n.Children.Add(Match(TokenClass.LeftBrace));
            n.Children.Add(Statements());
            n.Children.Add(ReturnStatement());
            n.Children.Add(Match(TokenClass.RightBrace));

            return n;
        }

        private Node FunctionCall()
        {
            if (!IsMatch(TokenClass.Identifier) || (PeakNext().type != TokenClass.LeftParen))
            {
                return null;
            }

            Node n = new Node("Function Call");
            n.Children.Add(Identifier());
            n.Children.Add(Match(TokenClass.LeftParen));
            n.Children.Add(ArgumentList());
            n.Children.Add(Match(TokenClass.RightParen));

            return n;
        }

        private Node Arguments()
        {
            if (!IsMatch(TokenClass.Comma))
            {
                return null;
            }

            Node n = new Node("Arguments");
            n.Children.Add(Match(TokenClass.Comma));
            n.Children.Add(Expression());
            n.Children.Add(Arguments());

            return n;
        }

        private Node ArgumentList()
        {
            if (PeakNext().type == TokenClass.RightParen)
            {
                return null;
            }

            Node n = new Node("Argument List");
            n.Children.Add(Expression());
            n.Children.Add(Arguments());

            return n;
        }

        #endregion

        #region Parameter

        private Node Parameter()
        {
            Node dataType = DataType();
            if (dataType == null)
            {
                return null;
            }

            Node n = new Node("Parameter");
            n.Children.Add(dataType);
            n.Children.Add(Identifier());

            return n;
        }

        private Node Parameters()
        {
            if (!IsMatch(TokenClass.Comma))
            {
                return null;
            }

            Node n = new Node("Parameters");
            n.Children.Add(Match(TokenClass.Comma));
            n.Children.Add(Parameter());
            n.Children.Add(Parameters());

            return n;
        }

        private Node ParameterList()
        {
            Node parameter = Parameter();
            if (parameter == null)
            {
                return null;
            }

            Node n = new Node("Parameter List");
            n.Children.Add(parameter);
            n.Children.Add(Parameters());

            return n;
        }

        #endregion

        #region Statements

        private Node Statements()
        {
            Node statement = Statement();
            if (statement == null)
            {
                return null;
            }

            Node n = new Node("Statements");
            n.Children.Add(statement);
            n.Children.Add(Statements());

            return n;
        }

        private Node Statement()
        {
            Node statement = null;
            statement = statement ?? DeclareStatement();
            statement = statement ?? AssignmentStatement();
            statement = statement ?? WriteStatement();
            statement = statement ?? ReadStatement();
            statement = statement ?? IfStatement();
            statement = statement ?? RepeatStatement();

            Node n = new Node("Statement");
            n.Children.Add(statement);

            return (statement != null) ? n : null;
        }

        private Node ReturnStatement()
        {
            if (!IsMatch(TokenClass.Return))
            {
                return null;
            }

            Node n = new Node("Return Statement");
            n.Children.Add(Match(TokenClass.Return));
            n.Children.Add(Expression());
            n.Children.Add(Match(TokenClass.Semicolon));

            return n;
        }

        private Node AssignmentStatement(bool matchSemiColon = true)
        {
            if ((!IsMatch(TokenClass.Identifier) || (PeakNext().type != TokenClass.Assign)))
            {
                return null;
            }

            Node n = new Node("Assignment Statement");
            n.Children.Add(Identifier());
            n.Children.Add(Match(TokenClass.Assign));
            n.Children.Add(Expression());

            if (matchSemiColon)
            {
                n.Children.Add(Match(TokenClass.Semicolon));
            }

            return n;
        }

        private Node WriteStatement()
        {
            if (!IsMatch(TokenClass.Write))
            {
                return null;
            }

            Node n = new Node("Write Statement");

            n.Children.Add(Match(TokenClass.Write));
            if (IsMatch(TokenClass.Endl))
            {
                n.Children.Add(Match(TokenClass.Endl));
            }
            else
            {
                n.Children.Add(Expression());
            }
            n.Children.Add(Match(TokenClass.Semicolon));

            return n;
        }

        private Node ReadStatement()
        {
            if (!IsMatch(TokenClass.Read))
            {
                return null;
            }

            Node n = new Node("Read Statement");
            n.Children.Add(Match(TokenClass.Read));
            n.Children.Add(Identifier());
            n.Children.Add(Match(TokenClass.Semicolon));

            return n;
        }

        private Node RepeatStatement()
        {
            if (!IsMatch(TokenClass.Repeat))
            {
                return null;
            }

            Node n = new Node("Repeat Statement");
            n.Children.Add(Match(TokenClass.Repeat));
            n.Children.Add(Statements());
            n.Children.Add(Match(TokenClass.Until));
            n.Children.Add(ConditionStatement());

            return n;
        }

        private Node IfStatement()
        {
            if (!IsMatch(TokenClass.If))
            {
                return null;
            }

            Node n = new Node("If Statement");

            n.Children.Add(Match(TokenClass.If));
            n.Children.Add(ConditionStatement());
            n.Children.Add(Match(TokenClass.Then));
            n.Children.Add(Statements());

            // Note: should return null if not matched
            n.Children.Add(ElseIfStatement());
            n.Children.Add(ElseStatement());

            n.Children.Add(Match(TokenClass.End));

            return n;
        }

        private Node ElseIfStatement()
        {
            if (!IsMatch(TokenClass.ElseIf))
            {
                return null;
            }

            Node n = new Node("ElseIf Statement");
            n.Children.Add(Match(TokenClass.ElseIf));
            n.Children.Add(ConditionStatement());
            n.Children.Add(Match(TokenClass.Then));
            n.Children.Add(Statements());

            // Note: should return null if not matched
            n.Children.Add(ElseIfStatement());
            n.Children.Add(ElseStatement());

            return n;
        }

        private Node ElseStatement()
        {
            if (!IsMatch(TokenClass.Else))
            {
                return null;
            }

            Node n = new Node("Else Statement");
            n.Children.Add(Match(TokenClass.Else));
            n.Children.Add(Statements());

            return n;
        }

        #endregion

        #region ConditionStatement

        private Node ConditionStatement()
        {
            Node n = new Node("Condition Statement");

            n.Children.Add(Condition());
            n.Children.Add(Conditions());

            return n;
        }

        private Node Condition()
        {
            Node n = new Node("Condition");

            n.Children.Add(Identifier());
            n.Children.Add(ConditionOperator());
            n.Children.Add(Term());

            return n;
        }

        private Node Conditions()
        {
            Node boolOpers = BooleanOperator();
            if (boolOpers == null)
            {
                return null;
            }

            Node n = new Node("Conditions");
            n.Children.Add(boolOpers);
            n.Children.Add(Condition());
            n.Children.Add(Conditions());

            return n;
        }

        private Node ConditionOperator()
        {
            var ops = new[] { TokenClass.Less, TokenClass.Greater, TokenClass.Equal, TokenClass.NotEqual };
            if (!ops.Contains(Peak().type))
            {
                return null;
            }

            Node n = new Node("Condition Operator");
            n.Children.Add(Match(Peak().type));

            return n;
        }

        private Node BooleanOperator()
        {
            Node n = new Node("Boolean Operator");

            if (IsMatch(TokenClass.And))
            {
                n.Children.Add(Match(TokenClass.And));
            }
            else if (IsMatch(TokenClass.Or))
            {
                n.Children.Add(Match(TokenClass.Or));
            }
            else
            {
                return null;
            }

            return n;
        }

        #endregion

        #region DeclareStatement

        private Node DeclareStatement()
        {
            Node dataType = DataType();
            if (dataType == null)
            {
                return null;
            }

            Node n = new Node("Delcate Statement");
            n.Children.Add(dataType);
            n.Children.Add(DeclarationsList());
            n.Children.Add(Match(TokenClass.Semicolon));

            return n;
        }

        private Node Declaration()
        {
            Node n = new Node("Declaration");

            Node assignment = AssignmentStatement(false);
            n.Children.Add((assignment != null) ? assignment : Identifier());

            return n;
        }

        private Node Declarations()
        {
            if (!IsMatch(TokenClass.Comma))
            {
                return null;
            }

            Node n = new Node("Declarations");
            n.Children.Add(Match(TokenClass.Comma));
            n.Children.Add(Declaration());
            n.Children.Add(Declarations());

            return n;
        }

        private Node DeclarationsList()
        {
            Node n = new Node("Declaration List");

            n.Children.Add(Declaration());
            n.Children.Add(Declarations());

            return n;
        }

        #endregion

        private Node Expression()
        {
            Node n = new Node("Expression");
            n.Children.Add((IsMatch(TokenClass.StringLiteral) ? String() : Equation()));

            return n;
        }

        private Node Identifier()
        {
            Token t = Peak();

            Node n = Match(TokenClass.Identifier);
            n?.Children.Add(new Node(t.lex));

            return n;
        }

        private Node DataType()
        {
            var types = new[] { TokenClass.Int, TokenClass.Float, TokenClass.String };
            if (!types.Contains(Peak().type))
            {
                return null;
            }

            Node n = new Node("DataType");
            n.Children.Add(Match(Peak().type));

            return n;
        }

        private Node Term()
        {
            Node n = new Node("Term");

            if (IsMatch(TokenClass.Number))
            {
                n.Children.Add(Number());
            }
            else if (IsMatch(TokenClass.Identifier))
            {
                Node fCall = FunctionCall();
                n.Children.Add((fCall != null) ? fCall : Identifier());
            }
            else
            {
                Error("Term", Peak());
                Advance();
            }

            return n;
        }

        private Node String()
        {
            Token t = Peak();

            Node n = Match(TokenClass.StringLiteral);
            n?.Children.Add(new Node(t.lex));

            return n;
        }

        private Node Number()
        {
            Token t = Peak();

            Node n = Match(TokenClass.Number);
            n?.Children.Add(new Node(t.lex));

            return n;
        }

        #region Equation

        private Node Equation()
        {
            Node n = new Node("Equation");

            n.Children.Add(EquationTerm());
            n.Children.Add(EquationDash());

            return n;
        }

        private Node EquationDash()
        {
            Node addOp = AddOperator();
            if (addOp == null)
            {
                return null;
            }

            Node n = new Node("Equation Dash");
            n.Children.Add(addOp);
            n.Children.Add(EquationTerm());
            n.Children.Add(EquationDash());

            return n;
        }

        private Node EquationTerm()
        {
            Node n = new Node("Equataion Term");

            n.Children.Add(Factor());
            n.Children.Add(EquationTermDash());

            return n;
        }

        private Node EquationTermDash()
        {
            Node mulOp = MulOperator();
            if (mulOp == null)
            {
                return null;
            }

            Node n = new Node("Equation Term Dash");
            n.Children.Add(mulOp);
            n.Children.Add(Factor());
            n.Children.Add(EquationTermDash());

            return n;
        }

        private Node Factor()
        {
            Node n = new Node("Factor");

            if (IsMatch(TokenClass.LeftParen))
            {
                n.Children.Add(Match(TokenClass.LeftParen));
                n.Children.Add(Equation());
                n.Children.Add(Match(TokenClass.RightParen));
            }
            else
            {
                n.Children.Add(Term());
            }

            return n;
        }

        private Node AddOperator()
        {
            var ops = new[] { TokenClass.Plus, TokenClass.Minus };
            if (!ops.Contains(Peak().type))
            {
                return null;
            }

            Node n = new Node("Add Operator");
            n.Children.Add(Match(Peak().type));

            return n;
        }

        private Node MulOperator()
        {
            var ops = new[] { TokenClass.Multiply, TokenClass.Divide };
            if (!ops.Contains(Peak().type))
            {
                return null;
            }

            Node n = new Node("Mul Operator");
            n.Children.Add(Match(Peak().type));

            return n;
        }

        #endregion

        #region Utils

        private void Error(TokenClass expected)
        {
            Error(Peak().line, Token.TokenToString(expected), null);
        }

        private void Error(TokenClass expected, Token Acutal)
        {
            Error(Token.TokenToString(expected), Acutal);
        }

        private void Error(string expected, Token actual)
        {
            Error(actual.line, expected, actual.ToString());
        }

        private void Error(int line, string expected, string actual)
        {
            if (string.IsNullOrWhiteSpace(actual))
                Errors.Error_List.Add($"parser:{line}: error: expected '{expected}'.");
            else
                Errors.Error_List.Add($"parser:{line}: error: expected '{expected}', but found '{actual}'.");
        }

        private Token Peak()
        {
            if ((_inputPointer) < _tokenStream.Count)
            {
                return _tokenStream[_inputPointer];
            }

            return new Token();
        }

        private Token PeakNext()
        {
            if ((_inputPointer + 1) < _tokenStream.Count)
            {
                return _tokenStream[_inputPointer + 1];
            }

            return new Token();
        }

        private bool IsMatch(TokenClass tokenClass)
        {
            return (Peak().type == tokenClass);
        }

        private Node Match(TokenClass expectedToken)
        {
            if (_inputPointer < _tokenStream.Count)
            {
                if (expectedToken == _tokenStream[_inputPointer].type)
                {
                    Node newNode = new Node(expectedToken.ToString());
                    _inputPointer++;
                    return newNode;
                }
                else
                {
                    Error(expectedToken, _tokenStream[_inputPointer]);
                    _inputPointer++;
                }
            }
            else
            {
                Error(expectedToken);
                _inputPointer++;
            }

            return null;
        }

        private Token Advance() => (_inputPointer < _tokenStream.Count) ? _tokenStream[_inputPointer++] : null;

    }

    #endregion
}