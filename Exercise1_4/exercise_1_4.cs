namespace handin1_pad;

using System.Collections.Immutable;
using System.Text.RegularExpressions;

abstract class Expr {

    public abstract int eval(ImmutableDictionary<string, int> env);
    public abstract Expr simplify();
    public abstract string toString();
}

class CstI : Expr {
    private int value;

    public CstI(int value) {
        this.value = value;
    }

    public override int eval(ImmutableDictionary<string, int> env) {
        return value;
    }

    public override Expr simplify() {
        return this;
    }

    public override string toString() {
        return value.ToString();
    }

    public int getValue() {
        return value;
    }
}

class Var : Expr {
    private string name;

    public Var(string name) {
        this.name = name;
    }

    public override int eval(ImmutableDictionary<string, int> env) {
        return env[name];
    }

    public override Expr simplify() {
        return this;
    }

    public override string toString() {
        return name;
    }

    public string getName() {
        return name;
    }
}

abstract class Binop : Expr {
    protected Expr exp1;
    protected Expr exp2;

    protected Binop(Expr exp1, Expr exp2) {
        this.exp1 = exp1;
        this.exp2 = exp2;
    }
}
class Add : Binop {

    public Add(Expr exp1, Expr exp2) : base(exp1, exp2) {}

    public override int eval(ImmutableDictionary<string, int> env) {
        return exp1.eval(env) + exp2.eval(env);
    }

    // INCOMPLETE
    public override Expr simplify() {
        var e1 = exp1.simplify();
        var e2 = exp2.simplify();
        switch ((e1, e2))
        {
            case (CstI, CstI): {
                var v1 = ((CstI)e1).getValue();
                var v2 = ((CstI)e2).getValue();
                return new CstI(v1 + v2);
                }
            case (CstI, _): {
                var v = ((CstI)e1).getValue();
                if (v == 0) {
                    return e2;
                }
                break; }
            case (_, CstI): {
                var v = ((CstI)e2).getValue();
                if (v == 0) {
                    return e1;
                }
                break; }
        }
        return new Add(e1, e2);
    }

    public override string toString() {
        return "(" + exp1.toString() + " + " + exp2.toString() + ")";
    }
} 

class Mul : Binop {

    public Mul(Expr exp1, Expr exp2) : base(exp1, exp2) {}

    public override int eval(ImmutableDictionary<string, int> env) {
        return exp1.eval(env) * exp2.eval(env);
    }

    // INCOMPLETE
    public override Expr simplify() {
        var e1 = exp1.simplify();
        var e2 = exp2.simplify();
        switch ((e1, e2))
        {
            case (CstI, CstI): {
                var v1 = ((CstI)e1).getValue();
                var v2 = ((CstI)e2).getValue();
                return new CstI(v1*v2);
                }
            case (CstI, _): {
                var v = ((CstI)e1).getValue();
                if (v == 1) {
                    return e2;
                } else if (v == 0) {
                    return new CstI(0);
                }
                break; }
            case (_, CstI): {
                var v = ((CstI)e2).getValue();
                if (v == 1) {
                    return e1;
                } else if (v == 0) {
                    return new CstI(0);
                }
                break; }
        }
        return new Mul(e1, e2);
    }

    public override string toString() {
        return "(" + exp1.toString() + " * " + exp2.toString() + ")";
    }
} 

class Sub : Binop {

    public Sub(Expr exp1, Expr exp2) : base(exp1, exp2) {}

    public override int eval(ImmutableDictionary<string, int> env) {
        return exp1.eval(env) - exp2.eval(env);
    }

    // INCOMPLETE
    public override Expr simplify() { 
                        var e1 = exp1.simplify();
        var e2 = exp2.simplify();
        switch ((e1, e2))
        {
            case (CstI, CstI): {
                var v1 = ((CstI)e1).getValue();
                var v2 = ((CstI)e2).getValue();
                return new CstI(v1-v2);
                }
            case (_, CstI): {
                var v = ((CstI)e2).getValue();
                if (v == 0) {
                    return e1;
                }
                break; }
            case (Var, Var): {
                var v1 = ((Var)e1).getName();
                var v2 = ((Var)e2).getName();
                if (v1 == v2) {
                    return new CstI(0);
                }
                break; }
        }
        return new Sub(e1, e2);
    }

    public override string toString() {
        return "(" + exp1.toString() + " - " + exp2.toString() + ")";
    }
}
