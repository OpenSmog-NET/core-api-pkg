var toRelativePath = new Func<string, string>((project) => $"./{project}");
var toTestRelativePath = new Func<string, string>((project) => $"../test/{project}");
var toAssemblyInfoPath = new Func<string, string>((project) => $"./{project}/Properties/AssemblyInfo.cs");



// dotnet restore
var getDotNetCoreRestoreSettings  = new Func<DotNetCoreRestoreSettings>(() => new DotNetCoreRestoreSettings()
{
    Sources = new string[] {
        "https://api.nuget.org/v3/index.json",

        },
    FallbackSources = new string[] {
        //"https://www.nuget.org/api/v2"
        },
    //PackagesDirectory = "./packages",
    Verbosity = DotNetCoreRestoreVerbosity.Error,
    DisableParallel = false,
});

// dotnet build
var getDotNetCoreBuildSettings = new Func<DotNetCoreBuildSettings>(() => new DotNetCoreBuildSettings
{
    Configuration = debug ? "Debug" : "Release"
});

// dotnet test
var getDotNetCoreTestSettings = new Func<string, string, DotNetCoreTestSettings>((project, testType) => new DotNetCoreTestSettings() {
    ArgumentCustomization = args=>args
    .Append($"-xml {TestDir}/results/{project}.xml")    
});

// dotnet publish
var getDotNetCorePublishSettings = new Func<string, DotNetCorePublishSettings>((project) => new DotNetCorePublishSettings() {
    OutputDirectory = $"{AppsDir}/{project}",
    Configuration = debug ? "Debug" : "Release"
});