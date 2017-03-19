/**
 * Tools
 */

// todo: Reenable this once vso-cake #30 is released
// #tool "nuget:?package=xunit.runner.console"

/**
 * Directories
 */

const string ArtifactsDir = "artifacts";
const string TestDir = "test";

/**
 * Targets
 */

const string Restore = "Restore";
const string Build = "Build";
const string Pack = "Pack";
const string UnitTests = "UnitTests";
const string IntegrationTests = "IntegrationTests";
const string ComponentTests = "ComponentTests";
const string FunctionalTests = "FunctionalTests";
const string UITests = "UITests";

var target          = Argument<string>("target", Build);
var configuration   = Argument<string>("configuration", "Release");
var platform        = Argument<string>("platform", "Any Cpu");

/**
 * Auxiliaries
 */

var getTestProjects = new Func<string, IEnumerable<string>>((testType) => GetFiles($"./test/**/*.{testType}.csproj").Select(x => x.FullPath));
var getProjectsDirs = new Func<IEnumerable<string>, IEnumerable<string>>((paths) => paths.Select(x => $"./src/{x}"));

var unitTests           = getTestProjects(UnitTests);
var integrationTests    = getTestProjects(IntegrationTests);
var componentTests      = getTestProjects(ComponentTests);
var functionalTests     = getTestProjects(FunctionalTests);
var uiTests             = getTestProjects(UITests);

var forEachPath = new Action<IEnumerable<string>, Func<string, string>, Action<string>>((files, filter, action) => {
    foreach(var file in files) {
        if (filter == null) action(file); else action(filter(file));
    }
});

#load settings.netcore.cake