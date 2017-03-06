#tool "nuget:https://www.nuget.org/api/v2/?package=KuduSync.NET"
#addin "nuget:https://www.nuget.org/api/v2/?package=Cake.Kudu"

string          target              = Argument<string>("target", "Default");
DirectoryPath   functionsSourcePath = Directory("./src");

Setup(context =>
{
    if (!Kudu.IsRunningOnKudu)
    {
        throw new Exception("Not running on Kudu");
    }
});

Task("Publish")
    .Does(() =>
{
    Kudu.Sync(functionsSourcePath);
});


Task("Default")
    .IsDependentOn("Publish");

RunTarget(target);