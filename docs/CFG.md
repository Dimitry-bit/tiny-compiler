# Tiny Languages Context Free Grammar

1. **Program** -> **FunctionStatements** **MainFunction**

2. **MainFunction** -> **Datatype** _main()_ **FunctionBody**

3. **FunctionStatement** -> **FunctionDeclaration** **FunctionBody**

4. **FunctionStatements** -> **FunctionStatement** **FunctionStatements** | $\epsilon$

5. **FunctionDeclaration** -> **Datatype** **Identifier** _(_ **ParameterList** _)_

6. **FunctionBody** -> _{_ **Statements** **ReturnStatement** _}_

7. **FunctionCall** -> **Identifier** _(_ **ArgumentList** _)_

8. **Arguments** -> _,_ **Identifier** **Identifiers** | $\epsilon$

9. **ArgumentList** -> **Identifier** **Identifiers** | $\epsilon$

10. **Parameter** -> **Datatype** **Identifier**

11. **Parameters** -> _,_ **Parameter** **Parameters** | $\epsilon$

12. **ParameterList** -> **Parameter** **Parameters** | $\epsilon$

13. **Statements** -> **Statement** **Statements** | $\epsilon$

14. **Statement** -> **DeclarationStatement**
                  | **AssignmentStatement**
                  | **WriteStatement**
                  | **ReadStatement**
                  | **IfStatement**
                  | **RepeatStatement**

15. **RepeatStatement** -> _repeat_ **Statements** _until_ **ConditionStatement**

16. **IfStatement** -> _if_ **ConditionStatement** _then_ **Statements** ( **ElseIfStatement** | **ElseStatement** | $\epsilon$ ) _end_

17. **ElseIfStatement** ->  _elseif_ **ConditionStatement** _then_ **Statements** ( **ElseIfStatement** | **ElseStatement** | $\epsilon$ )

18. **ElseStatement** -> _else_ **Statements**

19. **WriteStatement** -> _write_ ( **Expression** | _endl_ ) _;_

20. **ReadStatement** -> _read_ **Identifier** _;_

21. **ReturnStatement** -> _return_ **Expression** _;_

22. **AssignmentStatement** -> **Identifier** _:=_ **Expression** _;_

23. **ConditionStatement** -> **Condition** **Conditions**

24. **Condition** -> **Identifier** **ConditionOperator** **Term**

25. **Conditions** -> **BooleanOperator** **Condition** **Conditions** | $\epsilon$

26. **BooleanOperators** -> _&&_ | _||_

27. **ConditionOperators** -> _<_ | _>_ | _=_ | _<>_

28. **DeclarationStatement** -> **Datatype** **DeclarationList** _;_

29. **Declaration** -> **AssignmentStatement** | **Identifier**

30. **Declarations** -> _,_ **Declaration** **Declarations** | $\epsilon$

31. **DeclarationList** -> **Declaration** **Declarations**

32. **Datatype** -> _int_ | _float_ | _string_

33. **Term** -> **Number**
              | **Identifier**
              | **FunctionCall**

34. **Expression** -> **String**
                    | **Equation**

35. **Equation** -> **EquationTerm** **EquationDash**

36. **EquationDash** -> **AddOperator** **EquationTerm** **EquationDash** | $\epsilon$

37. **EquationTerm** -> **Factor** **EquationTermDash**

38. **EquationTermDash** -> **MulOperator** **Factor** **EquationTermDash** | $\epsilon$

39. **Factor** -> _(_ **Equation** _)_ | **Term**

40. **AddOperator** -> _+_ | _-_

41. **MulOperator** -> _*_ | _/_
