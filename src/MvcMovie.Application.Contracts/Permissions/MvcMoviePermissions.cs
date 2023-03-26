namespace MvcMovie.Permissions;

public static class MvcMoviePermissions
{
    public const string GroupName = "MvcMovie";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public static class Movies
    {
        public const string Default = GroupName + ".Movie";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Detail = Default + ".Detail";
        public const string Delete = Default + ".Delete";
    }

    // *** ADDED a NEW NESTED CLASS ***
    public static class Genres
    {
        public const string Default = GroupName + ".Genre";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Authors
    {
        public const string Default = GroupName + ".Author";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
