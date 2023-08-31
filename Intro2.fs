(* Programming language concepts for software developers, 2010-08-28 *)

(* Evaluating simple expressions with variables *)

//module Intro2

(* Association lists map object language variables to their values *)


let emptyenv = [] (* the empty environment *)

let env = [("a", 3); ("c", 78); ("baf", 666); ("b", 111)]


let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x

let cvalue = lookup env "c";;


(* Object language expressions with variables *)

type expr = 
  | CstI of int
  | Var of string
  | Prim of string * expr * expr
  | If of expr * expr * expr;;

let e1 = CstI 17;;

let e2 = Prim("+", CstI 3, Var "a");;

let e3 = Prim("+", Prim("*", Var "b", CstI 9), Var "a")

//Exercise 1.1 c)
let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i              -> i
    | Var x               -> lookup env x 
    | Prim(op, e1, e2)   ->
      let r1 = eval e1 env
      let r2 = eval e2 env
      match op with
      | "+" -> r1 + r2
      | "*" -> r1 * r2
      | "-" -> r1 - r2
      | "max" -> if r1 > r2 then r1 else r2
      | "min" -> if r1 < r2 then r1 else r2
      | "==" -> if r1 = r2 then r1 else r2
      | _ -> failwith "unkno[]wn primitive"
    | If(e1, e2, e3) ->
      match eval e1 env with
      | 0 -> eval e3 env                //else
      | _ -> eval e2 env                //then
      

let e1v  = eval e1 env
let e2v1 = eval e2 env
let e2v2 = eval e2 [("a", 314)];;
let e3v  = eval e3 env


// Exercise 1.1 b)
(*
eval (Prim("==", CstI 7, CstI 12)) [] -> 0
eval (Prim("==", CstI 7, CstI 7)) [] -> 1
eval (Prim("min", CstI 7, CstI 12)) [] -> 7
eval (Prim("max", CstI 7, CstI 12)) [] -> 12
*)

// Exercise 1.2 i

type aexp = 
  | CstI of int
  | Var of string
  | Add of aexp * aexp
  | Mul of aexp * aexp
  | Sub of aexp * aexp

// Exercise 1.2 ii

let exp1 = Sub (Var "v", Add (Var "w", Var "z"))
let exp2 = Mul (CstI 2, Sub (Var "v", Add (Var "w", Var "z")))
let exp3 = Add (Var "x", Add (Var "y", Add (Var "z", Var "v")))

// Exercise 1.2 iii

let rec fmt (a : aexp) = 
  match a with 
  | CstI x -> string x
  | Var x -> x
  | Add (x, y) -> sprintf "(%s + %s)" (fmt x) (fmt y)
  | Mul (x, y) -> sprintf "(%s * %s)" (fmt x) (fmt y)
  | Sub (x, y) -> sprintf "(%s - %s)" (fmt x) (fmt y)

// Exercise 1.2 iv

let rec simplify (a : aexp) = 
  match a with
  | Add (CstI x, CstI y) -> CstI (x+y)
  | Mul (CstI x, CstI y) -> CstI (x*y)
  | Sub (CstI x, CstI y) -> CstI (x-y)
  | Add (x, y) -> 
    match (x, y) with  
    | (CstI 0, y) -> y
    | (x, CstI 0) -> x
    | _ -> Add (x, y)
  | Mul (x, y) -> 
    match (x, y) with  
    | (CstI 1, y) -> y
    | (x, CstI 1) -> x
    | (x, y) when x = CstI 0 || y = CstI 0 -> CstI 0
    | _ -> Mul (x, y)
  | Sub (x, y) -> 
    match (x, y) with  
    | (x, CstI 0) -> x
    | (CstI x, CstI y) when x = y -> CstI 0
    | (Var x, Var y) when x = y -> CstI 0 
    | _ -> Sub (x, y)
  | _ -> a