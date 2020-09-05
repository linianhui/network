var target       = Argument("target", "default");
var gitCommitSha = Argument("git-commit-sha", EnvironmentVariable("GIT_COMMIT_SHA"));
var versionSuffix= string.IsNullOrWhiteSpace(gitCommitSha) ? "" : "+git+commit+" + gitCommitSha.Substring(0, 7);

var rootPath     = "../";
var srcPath      = rootPath + "1-src/";
var testPath     = rootPath + "2-test/";
var distPath     = rootPath + "3-dist/";

var solution     = rootPath + "networking.sln";
var srcProjects  = GetFiles(srcPath + "**/*.csproj");
var testProjects = GetFiles(testPath + "**/*.csproj");

Task("clean")
    .Description("清理项目缓存")
    .Does(() =>
{
    DeleteFiles(distPath + "*.trx");
    DeleteFiles(distPath + "*.nupkg");
    DeleteFiles(distPath + "*.snupkg");
    CleanDirectories(srcPath + "**/bin");
    CleanDirectories(srcPath + "**/obj");
    CleanDirectories(testPath + "**/bin");
    CleanDirectories(testPath + "**/obj");
});

Task("restore")
    .Description("还原项目依赖")
    .Does(() =>
{
    DotNetCoreRestore(solution);
});

Task("build")
    .Description("编译项目")
    .IsDependentOn("clean")
    .IsDependentOn("restore")
    .Does(() =>
{
    var buildSetting = new DotNetCoreBuildSettings {
        ArgumentCustomization = args => args.Append("-property:CUSTOM_VERSION_SUFFIX=" + versionSuffix)
                                            .Append("-property:GIT_COMMIT_SHA=" + gitCommitSha),
        NoRestore             = true
    };

    DotNetCoreBuild(solution, buildSetting);
});

Task("test")
    .Description("运行测试")
    .IsDependentOn("build")
    .Does(() =>
{
    var testSetting = new DotNetCoreTestSettings {
        NoRestore        = true,
        NoBuild          = true,
        Logger           = "trx",
        Verbosity        = DotNetCoreVerbosity.Normal,
        ResultsDirectory = distPath
    };

    foreach(var testProject in testProjects)
    {
        DotNetCoreTest(testProject.FullPath, testSetting);
    }
});

Task("pack")
    .Description("nuget打包")
    .IsDependentOn("test")
    .Does(() =>
{
    var packSetting = new DotNetCorePackSettings {
        ArgumentCustomization = args => args.Append("-property:CUSTOM_VERSION_SUFFIX=" + versionSuffix)
                                            .Append("-property:GIT_COMMIT_SHA=" + gitCommitSha),
        Configuration         = "Release",
        OutputDirectory       = distPath,
        NoBuild               = false
    };

    foreach(var srcProject in srcProjects)
    {
        DotNetCorePack(srcProject.FullPath, packSetting);
    }
});

Task("default")
    .Description("默认-运行测试(-target test)")
    .IsDependentOn("test");

RunTarget(target);
