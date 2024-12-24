# Tiny Languages Context Free Grammar EBNF

1. *Program* -> { *Function* } *MainFunction*

2. *MainFunction* -> *Datatype* __main()__ *FunctionBody*

3. *Function* -> *FunctionDeclaration* *FunctionBody*

4. *FunctionDeclaration* -> *Datatype* *Identifier* __(__ *Parameters*? __)__

5. *FunctionBody* -> __{__ *Statements* *ReturnStatement* __}__

6. *Parameter* -> *Datatype* *Identifier*

7. *Parameters* -> *Parameter* { __,__ *Parameter* }

8. *FunctionCall* -> *Identifier* __(__ *Arguments*? __)__

9. *Argument* -> *Expression*

10. *Arguments* -> *Argument* { __,__ *Argument* }

11. *Statements* -> { *Statement* }

12. *Statement* -> *DeclarationStatement* __;__
                  | *AssignmentStatement* __;__
                  | *WriteStatement* __;__
                  | *ReadStatement* __;__
                  | *IfStatement*
                  | *RepeatStatement*

13. *RepeatStatement* -> __repeat__ *Statements* __until__ *LogicalOr*

14. *IfStatement* -> __if__ *LogicalOr* __then__ *Statements* [ *ElseIfStatement* | *ElseStatement* ] __end__

15. *ElseIfStatement* ->  __elseif__ *LogicalOr* __then__ *Statements* [ *ElseIfStatement* | *ElseStatement* ]

16. *ElseStatement* -> __else__ *Statements*

17. *WriteStatement* -> __write__ ( *Expression* | __endl__ )

18. *ReadStatement* -> __read__ *Identifier*

19. *AssignmentStatement* -> *Identifier* __:=__ *Expression*

20. *Condition* -> *Identifier* ( __>__ | __<__ | __=__ | __<>__ ) *Expression*

21. *LogicalOr* -> *LogicalAnd* ( __||__ *LogicalOr* )?

22. *LogicalAnd* -> *Cond* ( __&&__ *Cond* )?

23. *DeclarationStatement* -> *Datatype* *Declarations*

24. *Declaration* -> *AssignmentStatement* | *Identifier*

25. *Declarations* -> *Declaration* { *Declaration* __,__ }

26. *ReturnStatement* -> __return__ *Expression* __;__

27. *Datatype* -> __int__ | __float__ | __string__

28. *Expression* -> *Term* ( ( __-__ | __+__ ) *Expression* )?

29. *Term* -> *Factor* ( ( __*__ | __/__ ) *Term* )?

30. *Factor* -> *FunctionCall*
                | *Number*
                | *String*
                | *Identifier*
                | __(__ *Expressions* __)__
