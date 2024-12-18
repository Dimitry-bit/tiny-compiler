# Tiny Languages Context Free Grammar EBNF

1. **Program** -> { **Function** } **MainFunction**

2. **MainFunction** -> **Datatype** _main()_ **FunctionBody**

3. **Function** -> **FunctionDeclaration** **FunctionBody**

4. **FunctionDeclaration** -> **Datatype** **Identifier** _(_ **Parameters**? _)_

5. **FunctionBody** -> _{_ **Statements** **ReturnStatement** _}_

6. **Parameter** -> **Datatype** **Identifier**

7. **Parameters** -> **Parameter** { _,_ **Parameter** }

8. **FunctionCall** -> **Identifier** _(_ **Arguments**? _)_

9. **Argument** -> **Expression**

10. **Arguments** -> **Argument** { _,_ **Argument** }

11. **Statements** -> { **Statement** }

12. **Statement** -> **DeclarationStatement** _;_
                  | **AssignmentStatement** _;_
                  | **WriteStatement** _;_
                  | **ReadStatement** _;_
                  | **IfStatement**
                  | **RepeatStatement**

13. **RepeatStatement** -> _repeat_ **Statements** _until_ **LogicalOr**

14. **IfStatement** -> _if_ **LogicalOr** _then_ **Statements** [ **ElseIfStatement** | **ElseStatement** ] _end_

15. **ElseIfStatement** ->  _elseif_ **LogicalOr** _then_ **Statements** [ **ElseIfStatement** | **ElseStatement** ]

16. **ElseStatement** -> _else_ **Statements**

17. **WriteStatement** -> _write_ ( **Expression** | _endl_ )

18. **ReadStatement** -> _read_ **Identifier**

19. **AssignmentStatement** -> **Identifier** _:=_ **Expression**

20. **Condition** -> **Identifier** ( _>_ | _<_ | _=_ | _<>_ ) **Expression**

21. **LogicalOr** -> **LogicalAnd** ( ( _||_ ) **LogicalOr** )?

22. **LogicalAnd** -> **Cond** ( (_&&_) **Cond** )?

23. **DeclarationStatement** -> **Datatype** **Declarations**

24. **Declaration** -> **AssignmentStatement** | **Identifier**

25. **Declarations** -> **Declaration** { **Declaration** _,_ }

26. **ReturnStatement** -> _return_ **Expression** _;_

27. **Datatype** -> _int_ | _float_ | _string_

28. **Expression** -> **Term** ( ( _-_ | _+_ ) **Term** )?

29. **Term** -> **Factor** ( ( _*_ | _/_ ) **Term** )?

30. **Factor** -> **FunctionCall**
                | **Number**
                | **String**
                | **Identifier**
                | _(_ **Expressions** _)_
