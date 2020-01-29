using System.IO;
using SmallFtpServer.Exceptions;

namespace SmallFtpServer.Models
{
    class LoginInfo
    {
        public string username { get; set; }
        public bool islogin { get; private set; }
        public string rename_filename { get; set; }
        public FtpTransferMode transferMode { get; set; } = new FtpTransferMode(FtpFileType.Ascii);

        DirectoryInfo currentDir;
        DirectoryInfo rootDir;
        public void Login(UserInfo user)
        {
            rootDir = new DirectoryInfo(user.rootdirectory);
            currentDir = new DirectoryInfo(user.rootdirectory);
            islogin = true;
        }

        public void LoginOut()
        {
            username = null;
            islogin = false;
        }

        public bool ChangeWorkDirectory(string path)
        {
            var p = GetAbsolutePath(path);
            if (Directory.Exists(p))
            {
                currentDir = new DirectoryInfo(p);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 相对地址转换成绝对地址
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetAbsolutePath(string path)
        {
            var fullPath = "";
            if (path[0] == '/')
            {
                path = path.TrimStart("/".ToCharArray());
                fullPath = Path.GetFullPath(path, RootFullPath);
            }
            else
                fullPath = Path.GetFullPath(path, CurrentFullPath);
            if (fullPath.StartsWith(RootFullPath))
            {
                return fullPath;
            }
            throw new InvalidFileException("文件或目录不存在");
        }

        /// <summary>
        /// 当前目录的虚地址
        /// </summary>
        public string CurrentVirtualPath
        {
            get
            {
                //Uri rootpath = new Uri(RootFullPath, UriKind.Absolute);
                //Uri currentpath = new Uri(CurrentFullPath, UriKind.Absolute);
                //Uri path = rootpath.MakeRelativeUri(currentpath);
                //return "/" + path.OriginalString;
                var path = CurrentFullPath.Substring(RootFullPath.Length, CurrentFullPath.Length - RootFullPath.Length).Replace('\\', '/');
                return path.StartsWith("/") ? path : "/" + path;
            }
        }

        public string RootFullPath
        {
            get
            {
                return rootDir.FullName;
            }
        }

        public string CurrentFullPath
        {
            get
            {
                return currentDir.FullName;
            }
        }
    }
}
