namespace MyRoomDig.DependencyServices
{
    using System;
    public interface IAppVersionAndBuild
    {
        string GetVersionNumber();
        string GetBuildNumber();
    }
}
