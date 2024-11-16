# Tiny Language Tokens

## Reserved Keywords

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

## Operators

| Token         | Operator  |
|:------------- |:--------- |
| T_PLUS        | `+`       |
| T_MINUS       | `-`       |
| T_MULTIPLY    | `*`       |
| T_DIVIDE      | `/`       |
| T_Assign      | `:=`      |
| T_LESS        | `<`       |
| T_GREATER     | `>`       |
| T_EQUAL       | `=`       |
| T_NOT_EQUAL   | `<>`      |
| T_AND         | `&&`      |
| T_OR          | `\|\|`    |
| T_COMMA       | `,`       |
| T_SEMICOLON   | `;`       |
| T_LEFT_BRACE  | `{`       |
| T_RIGHT_BRACE | `}`       |
| T_LEFT_PAREN  | `(`       |
| T_RIGHT_PAREN | `)`       |

## Literals

| Token             | Description | Example |
| :---------------- | :---------- | :------ |
| T_NUMBER          |  any sequence of digits and maybe floats | `123`, `0.23` |
| T_IDENTIFIER      |  starts with letter then any combination of letters and digits. | `x`, `val`, `counter1`, `str1`, `s2`  |
| T_STRING_LITERAL  | starts with double quotes followed by any combination of characters and digits then ends with double quotes  | `"Hello"`, `"2nd + 3rd"` |
