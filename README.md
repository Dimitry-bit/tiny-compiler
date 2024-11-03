# Tiny Compiler

A two-phase **Tiny** language compiler built with C# on the .NET framework.

Tiny PL language specification can be found [here](./docs/tiny-pl-language-description.pdf).

## Tokens

### Reserved Keywords

| Token     | Keyword   |
|:--------- |:--------- |
| T_INT     | `int`     |
| T_FLOAT   | `float`   |
| T_STRING  | `strign`  |
| T_READ    | `read`    |
| T_WRITE   | `write`   |
| T_REPEAT  | `repeat`  |
| T_UNTIL   | `until`   |
| T_IF      | `if`      |
| T_ELSE_IF | `elseif`  |
| T_ELSE    | `else`    |
| T_END     | `end`     |
| T_THEN    | `then`    |
| T_RETURN  | `return`  |
| T_END     | `end`     |
| T_ENDL    | `endl`    |
| T_MAIN    | `main` (Main Function)   |

### Operators

| Token         | Operator  |
|:------------- |:--------- |
| T_OP_PLUS     | `+`       |
| T_OP_MINUS    | `-`       |
| T_OP_MULTIPLY | `*`       |
| T_OP_DIVIDE   | `/`       |
| T_OP_Assign   | `:=`      |
| T_OP_LE       | `<`       |
| T_OP_GE       | `>`       |
| T_OP_EQ       | `=`       |
| T_OP_NOT_EQ   | `<>`      |
| T_OP_AND      | `&&`      |
| T_OP_OR       | `\|\|`    |
| T_COMMA       | `,`       |
| T_SEMICOLON   | `;`       |
| T_LEFT_CURLY_BRACES  | `{`       |
| T_RIGHT_CURLY_BRACES | `}`       |
| T_LEFT_PARENTHESES   | `(`       |
| T_RIGHT_PARENTHESES  | `)`       |

### Other

| Token             | Description | Example |
| :---------------- | :---------- | :------ |
| T_NUMBER          |  any sequence of digits and maybe floats | `123`, `0.23` |
| T_IDENTIFIER      |  starts with letter then any combination of letters and digits. | `x`, `val`, `counter1`, `str1`, `s2`  |
| T_STRING_LITERAL  | starts with double quotes followed by any combination of characters and digits then ends with double quotes  | `"Hello"`, `"2nd + 3rd"` |
