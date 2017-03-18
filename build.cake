#load "build/settings.build.cake"

const string SolutionFile = "OS.Core.Api.sln";

Task(Restore).Does(() => {
    DotNetCoreRestore(SolutionFile);
});

Task(Build)
    .IsDependentOn(Restore)
    .Does(() => {
    DotNetCoreBuild(SolutionFile);
});

Task(UnitTests)
    .IsDependentOn(Build)
    .Does(() => {
    forEachPath(unitTests, null, (test) => {
        DotNetCoreTest(test);
    });
});

RunTarget(target);