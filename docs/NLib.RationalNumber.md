# RationalNumber class
Represents a fractional number


**{{RationalNumber(long numerator)}}**
Initialize a new {{RationalNumber}}
```csharp
var rn = new RationalNumber(6); // equals to 6 / 1
```

**{{RationalNumber(long numerator, long denominator)}}**
Initialize a new {{RationalNumber}}
```csharp
var rn = new RationalNumber(6, 4); // equals to 6 / 4
```

**{{RationalNumber(string s)}}**
Initialize a new {{RationalNumber}}
```csharp
var rn1 = new RationalNumber("1/2"); // equals to 1 / 2
var rn2 = new RationalNumber("1/1/2"); // equals to 3 / 2
```

**{{RationalNumber(decimal d)}}**
**{{RationalNumber(double d)}}**
Initialize a new {{RationalNumber}}
```csharp
var rn = new RationalNumber(0.25); // equals to 1 / 4
```

Operators are defined
Example:
```csharp
var r1 = new RationalNumber(1.5);
var r2 = new RationalNumber(2);

var r3 = r1 + r2;
```
