﻿using System.Collections.Immutable;
using handin1_pad;

Expr e1 = new Add(new CstI(17), new Var("z"));
Expr e2 = new Mul(new Sub(new Var("a"), new Var("b")), new CstI(3));
Expr e3 = new Add(new Var("x"), new Mul(new CstI(2), new Mul(new Var("x"), new Var("x"))));
Expr e4 = new Sub(new Sub(new Sub(new Sub(new Var("x"), new Var("x")), new Var("x")), new Var("x")), new CstI(20));
Expr e5 = new Add(new CstI(0), new Add(new CstI(17), new Add(new CstI(5), new CstI(-5))));
Expr e6 = new Mul(new CstI(0), new CstI(17));
Expr e7 = new Mul(new CstI(0), new Var("a"));
Expr e8 = new Mul(new Sub(new Var("b"), new CstI(0)), new CstI(1));
ImmutableDictionary<string, int> env = new Dictionary<string, int> {{"z", 4}, {"a", 1}, {"b", 69}, {"x", 10}}.ToImmutableDictionary();

Console.WriteLine("ToString");
Console.WriteLine(e1.toString());
Console.WriteLine(e2.toString());
Console.WriteLine(e3.toString());
Console.WriteLine(e4.toString());
Console.WriteLine(e5.toString());
Console.WriteLine(e6.toString());
Console.WriteLine(e7.toString());
Console.WriteLine(e8.toString());
Console.WriteLine("Eval");
Console.WriteLine(e1.eval(env));
Console.WriteLine(e2.eval(env));
Console.WriteLine(e3.eval(env));
Console.WriteLine(e4.eval(env));
Console.WriteLine(e5.eval(env));
Console.WriteLine(e6.eval(env));
Console.WriteLine(e7.eval(env));
Console.WriteLine(e8.eval(env));
Console.WriteLine("Simplify");
Console.WriteLine(e1.simplify().toString());
Console.WriteLine(e2.simplify().toString());
Console.WriteLine(e3.simplify().toString());
Console.WriteLine(e4.simplify().toString());
Console.WriteLine(e5.simplify().toString());
Console.WriteLine(e6.simplify().toString());
Console.WriteLine(e7.simplify().toString());
Console.WriteLine(e8.simplify().toString());

