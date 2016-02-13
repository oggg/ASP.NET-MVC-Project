namespace ApplicationStore.Web.Infrastructure.Helpers
{
    using System.IO;
    using System.Web;

    public class FileSystemChecker
    {
        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(HttpContext.Current.Server.MapPath(path));
        }

        public static bool FileExists(string path, string fileName)
        {
            return File.Exists(HttpContext.Current.Server.MapPath(Path.Combine(path, fileName)));
        }
    }
}
