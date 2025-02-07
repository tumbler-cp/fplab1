#r "nuget: Fake.Core.Target"
#r "nuget: Fake.DotNet.Cli"
#r "nuget: Fake.DotNet.Testing.NUnit"
#r "nuget: Fake.IO.FileSystem"

open Fake.Core
open Fake.DotNet
open Fake.IO

Target.create "Clean" (fun _ ->
    Shell.cleanDirs ["bin"; "temp"]
)

Target.create "Restore" (fun _ ->
    DotNet.restore id "lab.sln"
)

Target.create "Build" (fun _ ->
    DotNet.build id "lab.sln"
)

Target.create "Test" (fun _ ->
    DotNet.test id "Tests/Tests.fsproj"
)

Target.create "ToolRestore" (fun _ ->
    DotNet.exec id "tool" "restore" |> ignore
)

Target.create "Lint" (fun _ ->
    let result = 
        DotNet.exec 
            id 
            "fsharplint" 
            "lint problem12 problem19"  
            
    if not result.OK then 
        result.Errors |> Seq.iter Trace.traceError
        failwith "Linting failed"
)

Target.create "Format" (fun _ ->
    let result = 
        DotNet.exec 
            id 
            "fantomas" 
            " --check problem12 problem19 Tests"  
            
    if not result.OK then
        result.Errors |> Seq.iter Trace.traceError
        failwith "Formatting check failed"
)

Target.create "FormatCheck" (fun _ ->
    let result = 
        DotNet.exec 
            id 
            "fantomas" 
            "--check problem12 problem19 Tests"
    if not result.OK then
        result.Errors |> Seq.iter Trace.traceError
        failwith "Formatting check failed"
)

Target.create "FormatFix" (fun _ ->
    let result = 
        DotNet.exec 
            id 
            "fantomas" 
            "problem12 problem19 Tests"
    if not result.OK then
        result.Errors |> Seq.iter Trace.traceError
        failwith "Formatting fix failed"
)

Target.create "All" ignore 

open Fake.Core.TargetOperators

"Clean"
  ==> "Restore"
  ==> "ToolRestore"
  ==> "FormatCheck"
  ==> "Build"
  ==> "All"

Target.runOrDefault "All"
