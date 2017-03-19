#load "build/settings.build.cake"

const string SolutionFile = "OS.Core.Api.sln";

var packages = getProjectsDirs(new string[] {
    "OS.Core.Api"
});

Task(Restore).Does(() => {
    DotNetCoreRestore(SolutionFile, getDotNetCoreRestoreSettings());
}); // Restore

Task(Build)
    .IsDependentOn(Restore)
    .Does(() => {
    Information($"Starting build({configuration}, {platform})");
    DotNetCoreBuild(SolutionFile, getDotNetCoreBuildSettings());
}); // Build

Task(UnitTests)
    .IsDependentOn(Build)
    .Does(() => {
    forEachPath(unitTests, null, (test) => {        
        Information($"Running test: {test}");
        DotNetCoreTest(test, getDotNetCoreTestSettings(test, UnitTests));
    });
}); // UnitTests

Task(Pack)
    .IsDependentOn(UnitTests)
    .Does(() => {
    forEachPath(packages, null, (package) => {
        DotNetCorePack(package, getDotNetCorePackSettings(package));
    });
    // pack all nuget projects
}); // Pack

RunTarget(target);