# Tiny Language Tokens Regex

### Reserved Keywords:

Regex:

```regex
int|float|string|read|write|repeat|until|if|elseif|else|then|return|end|endl|main
```

DFA:

![reserved-keywords-dfa.svg](./DFA/reserved-keywords-dfa.svg)

### Number:

Regex:

```regex
[0-9]+(\.[0-9]+)?
```

DFA:

![number-dfa.svg](./DFA/number-dfa.svg)

### Arithmetic Operators + Assignment Operator:

Regex:

```regex
+ | - | * | / | :=
```

DFA:

![arithmetic-operators-dfa.svg](./DFA/arith_assign-dfa.svg)
