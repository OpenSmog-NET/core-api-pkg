/**
 * File: settings.netcore.cake
 * Desc: CAKE settings for DotNetCore* Tasks
 * Author: mmisztal1980
 */

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
    OutputDirectory = $"{ArtifactsDir}/apps/{getProjectName(project)}",
    Configuration = configuration
});

/**
 * dotnet pack
 */
var getDotNetCorePackSettings = new Func<string, DotNetCorePackSettings>((project) => new DotNetCorePackSettings() {
    OutputDirectory = $"{ArtifactsDir}/packages/{getProjectName(project)}"
});