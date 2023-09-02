using handin1_pad;

Expr e1 = new Add(new CstI(17), new Var("z"));
Expr e2 = new Mul(new Sub(new Var("a"), new Var("b")), new CstI(3));
Expr e3 = new Add(new Var("x"), new Mul(new CstI(2), new Mul(new Var("x"), new Var("x"))));
Expr e4 = new Sub(new Sub(new Sub(new Sub(new Var("x"), new Var("x")), new Var("x")), new Var("x")), new CstI(20));

Console.WriteLine(e1.toString());
Console.WriteLine(e2.toString());
Console.WriteLine(e3.toString());
Console.WriteLine(e4.toString());

