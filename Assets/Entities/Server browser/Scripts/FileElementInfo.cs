using System;
using UnityEngine;
using UnityEngine.UI;

namespace PaneFileBrowser
{
    public struct FileElementInfo
    {
        public string FileName;
        public Sprite icon;
        public int DirectoryCount;
        public int FileCount;
        public Action NextLevel;
    }
}
