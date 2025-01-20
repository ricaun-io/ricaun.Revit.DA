using Nuke.Common;
using Nuke.Common.Execution;
using ricaun.Nuke;
using ricaun.Nuke.Components;

class Build : NukeBuild, IPublishPack, IRevitPackageBuilder, IPrePack//, ITestRevitPackageBuilder
{
    bool IHazPackageBuilderProject.ReleasePackageBuilder => false;
    string IHazPackageBuilderProject.Name => "Example";
    string IHazRevitPackageBuilder.Application => "Revit.App";
    string IHazRevitPackageBuilder.ApplicationType => "DBApplication";
    public static int Main() => Execute<Build>(x => x.From<IPublishPack>().Build);
}

public interface ITestRevitPackageBuilder : IHazTest, IRevitPackageBuilder
{
    Target TestRevitPackageBuilder => _ => _
        .TriggeredBy(PackageBuilder)
        .Before(Release)
        .OnlyWhenStatic(() => IsLocalBuild)
        .Executes(() =>
        {
            var TestLocalProjectName = "*.Tests";
            TestProjects(TestLocalProjectName);
        });
}