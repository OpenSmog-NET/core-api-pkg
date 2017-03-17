var target = Argument<string>("target", "Build");


Task("Restore").Does(() => {
    DotNetCoreRestore("OS.Core.Api.sln");
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
    DotNetCoreBuild("OS.Core.Api.sln");
});

Task("UnitTests")
    .IsDependentOn("Build")
    .Does(() => {
    DotNetCoreTest(@".\test\OS.Core.Api.UnitTests");
});

RunTarget(target);