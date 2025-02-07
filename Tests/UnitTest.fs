module Tests

open NUnit.Framework
open Problem12

[<SetUp>]
let Setup () = ()

[<Test>]
let ``Problem 12 monolit tail recursion`` () =
    let res = Recursion.tailRecursiveSolution 500
    Assert.That(res, Is.EqualTo(76576500))

[<Test>]
let ``Problem 12 monolit recursion`` () =
    let res = Recursion.recursiveSolution 500
    Assert.That(res, Is.EqualTo(76576500))

[<Test>]
let ``Problem 12 modular implementation`` () =
    let res = Modular.modularSolution ()
    Assert.That(res, Is.EqualTo(76576500))

[<Test>]
let ``Problem 12 generation using map`` () =
    let res = MapGen.mapGenSolution ()
    Assert.That(res, Is.EqualTo(76576500))

[<Test>]
let ``Loops`` () =
    let res = Loops.loopSolution ()
    Assert.That(res, Is.EqualTo(76576500))

[<Test>]
let ``Infinite sequence`` () =
    let res = InfSeq.infiniteSequenceSolution ()
    Assert.That(res, Is.EqualTo(76576500))
