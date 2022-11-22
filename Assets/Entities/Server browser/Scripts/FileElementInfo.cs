using System;

namespace PaneFileBrowser
{
    public struct FileElementInfo
    {
        public string FileName;
        public int DirectoryCount;
        public int FileCount;
        public Action NextLevel;
    }
}
