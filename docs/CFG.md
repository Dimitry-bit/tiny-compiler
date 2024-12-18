# Tiny Languages Context Free Grammar

1. **Program** -> **Functions** **MainFunction**

2. **MainFunction** -> **Datatype** _main()_ **FunctionBody**

3. **Function** -> **FunctionDeclaration** **FunctionBody**

4. **Functions** -> **Function** **Functions** | $\epsilon$

5. **FunctionDeclaration** -> **Datatype** **Identifier** _(_ **ParameterList** _)_

6. **FunctionBody** -> _{_ **Statements** **ReturnStatement** _}_

7. **FunctionCall** -> **Identifier** _(_ **ArgumentList** _)_

8. **Argument** -> **Identifier**

9. **Arguments** -> _,_ **Argument** **Arguments** | $\epsilon$

10. **ArgumentList** -> **Argument** **Arguments** | $\epsilon$

11. **Parameter** -> **Datatype** **Identifier**

12. **Parameters** -> _,_ **Parameter** **Parameters** | $\epsilon$

13. **ParameterList** -> **Parameter** **Parameters** | $\epsilon$

14. **Statements** -> **Statement** **Statements** | $\epsilon$

15. **Statement** -> **DeclarationStatement** _;_
                  | **AssignmentStatement** _;_
                  | **WriteStatement** _;_
                  | **ReadStatement** _;_
                  | **IfStatement**
                  | **RepeatStatement**

16. **RepeatStatement** -> _repeat_ **Statements** _until_ **LogicalOr**

17. **IfStatement** -> _if_ **LogicalOr** _then_ **Statements** ( **ElseIfStatement** | **ElseStatement** | $\epsilon$ ) _end_

18. **ElseIfStatement** ->  _elseif_ **LogicalOr** _then_ **Statements** ( **ElseIfStatement** | **ElseStatement** | $\epsilon$ )

19. **ElseStatement** -> _else_ **Statements**

20. **WriteStatement** -> _write_ ( **Expression** | _endl_ )

21. **ReadStatement** -> _read_ **Identifier**

22. **AssignmentStatement** -> **Identifier** _:=_ **Expression**

23. **Condition** -> **Identifier** ( _>_ | _<_ | _=_ | _<>_ ) **Expression**

24. **LogicalOr** -> **LogicalAnd** ( ( _||_ ) **LogicalOr** | $\epsilon$ )

25. **LogicalAnd** -> **Cond** ( (_&&_) **Cond** | $\epsilon$ )

26. **DeclarationStatement** -> **Datatype** **DeclarationList**

27. **Declaration** -> **AssignmentStatement** | **Identifier**

28. **Declarations** -> _,_ **Declaration** **Declarations** | $\epsilon$

29. **DeclarationList** -> **Declaration** **Declarations**

30. **ReturnStatement** -> _return_ **Expression** _;_

31. **Datatype** -> _int_ | _float_ | _string_

32. **Expression** -> **Term** ( ( _-_ | _+_ ) **Term**  | $\epsilon$ )

33. **Term** -> **Factor** ( ( _*_ | _/_ ) **Term** | $\epsilon$ )

34. **Factor** -> **FunctionCall**
                | **Number**
                | **String**
                | **Identifier**
                | _(_ **Expressions** _)_
