// include Fake lib
#r @"packages/FAKE/tools/FakeLib.dll"


open Fake.FscHelper
open Fake


let source = "./src/"
let buildDir =  "./build/"
let deployDir = "./deploy/";

Target "Clean" (fun _ ->
    trace "taget:  Clean"
    CleanDirs [buildDir; deployDir]
  )

Target "Build" (fun _ ->
    trace "target: Build"
  )

  
Target "go-server.old" (fun _ ->
  ["game.fs";"alpha.fs"] |> List.map (fun c -> source + c)
  |>  Compile [FscParam.Target TargetType.Library; FscParam.Out (buildDir + "go-server.dll")] )

Target "go-server" (fun _ ->
      MSBuildRelease buildDir "Build" ["go-server.fsproj"]
      |> Log "AppBuild-Output: "
    )

// Default target
Target "Default" (fun _ ->
  trace "target:  Default"
)

// Dependencies
"Clean"
  ==> "go-server"
  ==> "Build"
  ==> "Default"
  

// start build
RunTargetOrDefault "Default"