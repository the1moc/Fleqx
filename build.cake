// Arguments.
var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");

// Solution variables.
var solutions = GetFiles("./**/*.sln");
var solutionPaths = solutions.Select(solution => solution.GetDirectory());

// Setup.
Setup(() =>
{
	// Before tasks are started.
	Information("Beginning to run tasks, good luck!");
});

Teardown(() =>
{
	// After tasks are finished.
	Information("All tasks completed! Noice");
});

// Tasks.
Task("Clean")
	.Description("Cleans all directories that are used during the build process.")
	.Does(() =>
{
	// Clean solution directories.
	foreach(var path in solutionPaths)
	{
		Information("Cleaning {0}", path);
		CleanDirectories(path + "/**/bin/" + configuration);
		CleanDirectories(path + "/**/obj/" + configuration);
	}
});

Task("Restore")
	.Description("Restores all the NuGet packages that are used by the specified solution.")
	.IsDependentOn("Clean")
	.Does(() =>
{
	// Restore all NuGet packages.
	foreach(var solution in solutions)
	{
		Information("Restoring {0}...", solution);
		NuGetRestore(solution);
	}
});

Task("Bower")
	.Description("Restores all the bower packages that are used by the specified solution.")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.Does(() =>
{
	// Restore all bower packages.
	foreach(var solution in solutions)
	{
		Information("Restoring {0}...", solution);
        StartProcess("cmd npm install");
	}
})
.OnError(exception => 
{
	Information(exception.ToString());
});

Task("Build")
	.Description("Builds all the different parts of the project.")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.Does(() =>
{
	// Build all solutions.
	foreach(var solution in solutions)
	{
		Information("Building {0}", solution);
		MSBuild(solution, settings =>
			settings.SetPlatformTarget(PlatformTarget.MSIL)
				.WithProperty("TreatWarningsAsErrors","true")
				.WithProperty("DeployOnBuild", "true")
				.WithProperty("CreatePackageOnPublish", "true")
				.SetConfiguration(configuration));
	}
}).OnError(exception => 
{
	throw new Exception(exception.ToString());
});

// Startup target
Task("Default")
	.Description("This is the default task which will be ran if no specific target is passed in.")
	.IsDependentOn("Build");

// Execution
RunTarget(target);