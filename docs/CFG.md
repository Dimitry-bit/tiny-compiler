# Tiny Languages Context Free Grammar

1. *Program* -> *Functions* *MainFunction*

2. *MainFunction* -> *Datatype* __main()__ *FunctionBody*

3. *Function* -> *FunctionDeclaration* *FunctionBody*

4. *Functions* -> *Function* *Functions* | $\epsilon$

5. *FunctionDeclaration* -> *Datatype* *Identifier* __(__ *ParameterList* __)__

6. *FunctionBody* -> __{__ *Statements* *ReturnStatement* __}__

7. *FunctionCall* -> *Identifier* __(__ *ArgumentList* __)__

8. *Argument* -> *Identifier*

9. *Arguments* -> __,__ *Argument* *Arguments* | $\epsilon$

10. *ArgumentList* -> *Argument* *Arguments* | $\epsilon$

11. *Parameter* -> *Datatype* *Identifier*

12. *Parameters* -> __,__ *Parameter* *Parameters* | $\epsilon$

13. *ParameterList* -> *Parameter* *Parameters* | $\epsilon$

14. *Statements* -> *Statement* *Statements* | $\epsilon$

15. *Statement* -> *DeclarationStatement* __;__
                  | *AssignmentStatement* __;__
                  | *WriteStatement* __;__
                  | *ReadStatement* __;__
                  | *IfStatement*
                  | *RepeatStatement*

16. *RepeatStatement* -> __repeat__ *Statements* __until__ *LogicalOr*

17. *IfStatement* -> __if__ *LogicalOr* __then__ *Statements* ( *ElseIfStatement* | *ElseStatement* | $\epsilon$ ) __end__

18. *ElseIfStatement* ->  __elseif__ *LogicalOr* __then__ *Statements* ( *ElseIfStatement* | *ElseStatement* | $\epsilon$ )

19. *ElseStatement* -> __else__ *Statements*

20. *WriteStatement* -> __write__ ( *Expression* | __endl__ )

21. *ReadStatement* -> __read__ *Identifier*

22. *AssignmentStatement* -> *Identifier* __:=__ *Expression*

23. *Condition* -> *Identifier* ( __>__ | __<__ | __=__ | __<>__ ) *Expression*

24. *LogicalOr* -> *LogicalAnd* ( __||__ *LogicalOr* | $\epsilon$ )

25. *LogicalAnd* -> *Cond* ( __&&__ *Cond* | $\epsilon$ )

26. *DeclarationStatement* -> *Datatype* *DeclarationList*

27. *Declaration* -> *AssignmentStatement* | *Identifier*

28. *Declarations* -> __,__ *Declaration* *Declarations* | $\epsilon$

29. *DeclarationList* -> *Declaration* *Declarations*

30. *ReturnStatement* -> __return__ *Expression* __;__

31. *Datatype* -> __int__ | __float__ | __string__

32. *Expression* -> *Term* ( ( __-__ | __+__ ) *Term*  | $\epsilon$ )

33. *Term* -> *Factor* ( ( __*__ | __/__ ) *Term* | $\epsilon$ )

34. *Factor* -> *FunctionCall*
                | *Number*
                | *String*
                | *Identifier*
                | __(__ *Expressions* __)__
