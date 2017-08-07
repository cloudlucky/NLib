# RationalNumber class
Represents a fractional number


**{{RationalNumber(long numerator)}}**
Initialize a new {{RationalNumber}}
{code:c#}
var rn = new RationalNumber(6); // equals to 6 / 1
{code:c#}

**{{RationalNumber(long numerator, long denominator)}}**
Initialize a new {{RationalNumber}}
{code:c#}
var rn = new RationalNumber(6, 4); // equals to 6 / 4
{code:c#}

**{{RationalNumber(string s)}}**
Initialize a new {{RationalNumber}}
{code:c#}
var rn1 = new RationalNumber("1/2"); // equals to 1 / 2
var rn2 = new RationalNumber("1/1/2"); // equals to 3 / 2
{code:c#}

**{{RationalNumber(decimal d)}}**
**{{RationalNumber(double d)}}**
Initialize a new {{RationalNumber}}
{code:c#}
var rn = new RationalNumber(0.25); // equals to 1 / 4
{code:c#}

Operators are defined
Example:
{code:c#}
var r1 = new RationalNumber(1.5);
var r2 = new RationalNumber(2);

var r3 = r1 + r2;
{code:c#}