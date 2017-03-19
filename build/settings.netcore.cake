// var toRelativePath = new Func<string, string>((project) => $"./{project}");
// var toTestRelativePath = new Func<string, string>((project) => $"../test/{project}");
// var toAssemblyInfoPath = new Func<string, string>((project) => $"./{project}/Properties/AssemblyInfo.cs");

/**
 * dotnet restore
 */
var getDotNetCoreRestoreSettings  = new Func<DotNetCoreRestoreSettings>(() => new DotNetCoreRestoreSettings()
{
    DisableParallel = false,
});

/**
 * dotnet build
 */
var getDotNetCoreBuildSettings = new Func<DotNetCoreBuildSettings>(() => new DotNetCoreBuildSettings
{
    Configuration = configuration,
});

/**
 * dotnet test
 */

// loggers
var trxLogger = new Func<string, string>((project) => $"--logger \"trx;LogFileName=../../results/{getProjectName(project)}.trx\"");

var getDotNetCoreTestSettings = new Func<string, string, DotNetCoreTestSettings>((project, testType) => new DotNetCoreTestSettings() {
    ArgumentCustomization = args => args
        .Append(trxLogger(project))
});

/**
 * dotnet publish
 */
var getDotNetCorePublishSettings = new Func<string, DotNetCorePublishSettings>((project) => new DotNetCorePublishSettings() {
    OutputDirectory = $"{ArtifactsDir}/apps/{project}",
    Configuration = configuration
});

/**
 * dotnet pack
 */
var getDotNetCorePackSettings = new Func<string, DotNetCorePackSettings>((project) => new DotNetCorePackSettings() {
    OutputDirectory = $"{ArtifactsDir}/packages/{project}"
});