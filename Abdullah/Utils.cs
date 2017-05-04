using Starcounter;

namespace Abdullah
{
    public static class Utils
    {
        public static string GetBaseUrl()
        {
            return "/" + Application.Current.Name + "/";
        }

        public static string GetDbName()
        {
            return Application.Current.Name;
        }

        public static string MakeUrl(string tail)
        {
            return GetBaseUrl() + tail;
        }
    }
}
