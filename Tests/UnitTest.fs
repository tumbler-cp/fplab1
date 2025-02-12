module Tests

open NUnit.Framework
open Lab1

[<SetUp>]
let Setup () = ()

[<Test>]
let ``Problem 12 monolit tail recursion`` () =
    let res = Problem12.Recursion.tailRecursiveSolution 500
    Assert.That(res, Is.EqualTo(76576500))

[<Test>]
let ``Problem 12 modular implementation`` () =
    let res = Problem12.Modular.modularSolution ()
    Assert.That(res, Is.EqualTo(76576500))

[<Test>]
let ``Problem 12 generation using map`` () =
    let res = Problem12.MapGen.mapGenSolution ()
    Assert.That(res, Is.EqualTo(76576500))

[<Test>]
let ``Problem 12 Loops`` () =
    let res = Problem12.Loops.loopSolution ()
    Assert.That(res, Is.EqualTo(76576500))

[<Test>]
let ``Problem 12 Infinite sequence`` () =
    let res = Problem12.InfSeq.infiniteSequenceSolution ()
    Assert.That(res, Is.EqualTo(76576500))

[<Test>]
let ``Problem 19 Tail recursion`` () =
    let res = Problem19.TailRec.tailRecSolution ()
    Assert.That(res, Is.EqualTo(171))

[<Test>]
let ``Problem 19 recursion`` () =
    let res = Problem19.Recursive.recursiveSolution ()
    Assert.That(res, Is.EqualTo(171))

[<Test>]
let ``Problem 19 Modular`` () =
    let res = Problem19.Modular.modularSolution ()
    Assert.That(res, Is.EqualTo(171))

[<Test>]
let ``Problem 19 map generation`` () =
    let res = Problem19.MapGen.mapGenSolution ()
    Assert.That(res, Is.EqualTo(171))

[<Test>]
let ``Problem 19 loops`` () =
    let res = Problem19.Loops.loopsSolution ()
    Assert.That(res, Is.EqualTo(171))

[<Test>]
let ``Problem 19 infinite sequence`` () =
    let res = Problem19.InfSeq.infSeqSolution ()
    Assert.That(res, Is.EqualTo(171))
