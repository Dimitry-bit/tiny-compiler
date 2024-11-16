---
marp: true
theme: default
paginate: true
---

# Tiny Language Tokens Regex

---

### Reserved Keywords:

Regex:

```regex
int|float|string|read|write|repeat|until|if|elseif|else|then|return|end|endl
```
---
<!-- _class: reserved -->

DFA:

![reserved-keywords-dfa.svg](./DFA/reserved-keywords-dfa.svg)



---
### Number:

Regex:

```regex
[0-9]+(\.[0-9]+)?
```

DFA:

![number-dfa.svg](./DFA/number-dfa.svg)

---

### String

Regex:

```regex
"([a-zA-Z0-9-!#$%&'()*+,./:;<=>?@[\\\]_`{|}~]|(\"))*"
```

DFA:

![string-dfa.svg](./DFA/string-dfa.svg)

---

## Other Operators:

Regex:

```regex
{|}|(|)|;|,
```

DFA:

![other-operators-dfa.svg](./DFA/other-operators-dfa.svg)

---

### Identifier

Regex:

```regex
[a-zA-Z][a-zA-Z0-9]*
```

DFA:

![identifier-dfa.svg](./DFA/identifier-dfa.svg)

---

### Arithmetic Operators + Assignment Operator:

Regex:

```regex
+ | - | * | / | :=
```

DFA:

![arithmetic-operators-dfa.svg](./DFA/arith_assign-dfa.svg)

---

### Comment

Regex:

```regex
/\*[a-zA-Z0-9-*!#$%&'()+,./:;<=>?@[]_{|}~]*\*/
```

DFA:

![comment-dfa.svg](./DFA/comment-dfa.svg)

---

### Boolean Operators

Regex:

```regex
&& | ||
```

DFA:

![boolean-op-dfa.svg](./DFA/boolean-op-dfa.svg)

---

### Condition Operators

Regex:

```regex
< | > | = | <>
```

DFA:

![condition-op-dfa.svg](./DFA/condition-op-dfa.svg)

<style>
    img[src*="reserved-keywords-dfa.svg"] {
    width: 65%;
    height: 100%;
}
</style>

