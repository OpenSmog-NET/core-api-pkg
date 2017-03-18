const string Restore = "Restore";
const string Build = "Build";
const string UnitTests = "UnitTests";
const string IntegrationTests = "IntegrationTests";
const string ComponentTests = "ComponentTests";
const string FunctionalTests = "FunctionalTests";

var target = Argument<string>("target", Build);

var getTestProjects = new Func<string, IEnumerable<string>>((testType) => GetFiles($"./test/**/*.{testType}.csproj").Select(x => x.FullPath));

var unitTests = getTestProjects(UnitTests);


var forEachPath = new Action<IEnumerable<string>, Func<string, string>, Action<string>>((files, filter, action) => {
    foreach(var file in files) {
        if (filter == null) action(file); else action(filter(file));
    }
});
