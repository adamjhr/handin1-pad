namespace handin1_pad;

using System.Collections.Immutable;

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
        return new Add(exp1.simplify(), exp2.simplify());
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
        return new Mul(exp1.simplify(), exp2.simplify());
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
        return new Sub(exp1.simplify(), exp2.simplify());
    }

    public override string toString() {
        return "(" + exp1.toString() + " - " + exp2.toString() + ")";
    }
}

