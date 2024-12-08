# Tiny Languages Context Free Grammar

1. **Program** -> **Function_Statements** **Main_Function**

2. **Main_Function** -> **Datatype** _main()_ **Function_Body**

3. **Function_Statement** -> **Function_Declaration** **Function_Body**

4. **Function_Statements** -> **Function_Statement** **Function_Statements** | $\epsilon$

5. **Statements** -> **Statement** **Statements** | $\epsilon$

6. **Statement** -> **Declaration_Statement**
                  | **Assignment_Statement**
                  | **Write_Statement**
                  | **Read_Statement**
                  | **If_Statement**
                  | **Repeat_Statement**

7. **Function_Body** -> _{_ **Statements** **Return_Statement** _}_

8. **Parameter_List** -> **Parameter** **Parameters** | $\epsilon$

9. **Parameters** -> _,_ **Parameter** **Parameters** | $\epsilon$

10. **Parameter** -> **Datatype** **Identifier**

11. **Function_Declaration** -> **Datatype** **FunctionName** _(_ **Parameters_List** _)_

12. **FunctionName** -> **Identifier**

13. **Repeat_Statement** -> _repeat_ **Statements** _until_ **Condition_Statement**

14. **If_Statement** -> _if_ **Condition_Statement** _then_ **Statements** ( **Else_If_Statement** | **Else_Statement** | $\epsilon$ ) _end_

15. **Else_If_Statement** ->  _elseif_ **Condition_Statement** _then_ **Statements** ( **Else_If_Statement** | **Else_Statement** )

16. **Else_Statement** -> _else_ **Statements**

17. **Declaration_Statement** -> **Datatype** **Identifier** **Identifiers** _;_

18. **Write_Statement** -> _write_ ( **Expression** | _endl_ ) _;_

19. **Read_Statement** -> _read_ **Identifier** _;_

20. **Return_Statement** -> _return_ **Expression** _;_

21. **Assignment_Statement** -> **Identifier** _:=_ **Expression** _;_

22. **Condition_List** -> **Boolean_Operator** **Condition** **Condition_List** | $\epsilon$

23. **Condition_Statement** -> **Condition** **Condition_List**

24. **Datatype** -> _int_ | _float_ | _string_

25. **Boolean_Operators** -> _&&_ | _||_

26. **Condition_Operators** -> _<_ | _>_ | _=_ | _<>_

27. **Condition** -> **Identifier** **Condition_Operator** **Term**

28. **Expression** -> **String**
                    | **Term**
                    | **Equation**

29. **Term** -> **Number**
              | **Identifier**
              | **Function_Call**

30. **Arithmetic_Operator** -> _+_ | _-_ | _*_ | _/_

31. **Function_Call** -> **Identifier** _(_ **Identifier_List** _)_

32. **Identifier_List** -> **Identifier** **Identifiers** | $\epsilon$

33. **Identifiers** -> _,_ **Identifier** **Identifiers** | $\epsilon$

34. **Equation_Term** -> **Term** | _(_ **Equation** _)_

35. **Equation** -> **Equation_Term** **Arithmetic_Operator** **Equation_Term**
