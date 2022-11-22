using System.Collections.Generic;
using System.Linq;
using Entities.Server_browser.JSON_objects;
using PolyAndCode.UI;
using UnityEngine;

namespace PaneFileBrowser
{
    public class FileList : MonoBehaviour, IRecyclableScrollRectDataSource
    {
        [SerializeField]
        private Sprite driveIcon;

        [SerializeField]
        private Sprite directoryIcon;

        [SerializeField]
        private Sprite fileIcon;

        private FileSystem _fileSystem;

        [SerializeField]
        private RecyclableScrollRect recyclableScrollRect;

        //Data List
        private List<FileElementInfo> _fileList;

        //Recyclable scroll rect's data source must be assigned in Awake.
        private void Awake()
        {
            InitData();
            recyclableScrollRect.DataSource = this;
        }


        private void InitData()
        {
            _fileList = new List<FileElementInfo>();
            if (_fileList != null) _fileList.Clear();
        }

        private void OnValidate()
        {
            if (recyclableScrollRect == null) Debug.LogAssertion("recyclableScrollRect == null");
        }

        #region DATA-SOURCE

        public int GetItemCount()
        {
            return _fileList.Count;
        }

        /// <summary>
        /// Data source method. Called for a cell every time it is recycled.
        /// Implement this method to do the necessary cell configuration.
        /// </summary>
        public void SetCell(ICell cell, int index)
        {
            //Casting to the implemented Cell
            var item = cell as FileElementCell;
            item?.ConfigureCell(_fileList[index], index);
        }

        #endregion

        public void BuildView(FileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            DiskView(_fileSystem.Disks);
        }

        private void DiskView(List<Disk> disks)
        {
            _fileList.Clear();
            if (disks == null) return;

            foreach (var disk in disks)
            {
                var element = CreateFileElementInfo(disk);
                AddPlate(element);
            }
        }


        private void DirectoriesView(List<Directory> directories, Directory root)
        {
            _fileList.Clear();
            if (directories == null) { return; }

            AddLinkToRoot(root);

            foreach (var directory in directories)
            {
                directory.Root = root;
                var element = CreateFileElementInfo(directory, root);
                AddPlate(element);
            }
        }

        private FileElementInfo CreateFileElementInfo(Disk disk)
        {
            return new FileElementInfo()
            {
                FileName = $"{disk.Label} ({disk.Name}:)",
                DirectoryCount = disk.Directories?.Count ?? 0,
                FileCount = disk.Files?.Count ?? 0,
                NextLevel = () =>
                {
                    DirectoriesView(disk.Directories, disk);
                    FilesView(disk.Files);
                }
            };
        }

        private FileElementInfo CreateFileElementInfo(Directory directory, Directory root)
        {
            return new FileElementInfo
            {
                FileName = directory.Name,
                DirectoryCount = directory.Directories?.Count ?? 0,
                FileCount = directory.Files?.Count ?? 0,
                NextLevel = () =>
                {
                    DirectoriesView(directory.Directories, directory);
                    FilesView(directory.Files);
                }
            };
        }

        private void AddLinkToRoot(Directory root)
        {
            if (root is Disk)
            {
                var disk = root as Disk;
                AddPlate(new FileElementInfo()
                {
                    FileName = $"{disk.Label} ({disk.Name}:)",
                    DirectoryCount = root.Directories?.Count ?? 0,
                    FileCount = root.Files?.Count ?? 0,
                    NextLevel = () => { DiskView(_fileSystem.Disks); }
                });
                return;
            }

            AddPlate(new FileElementInfo()
            {
                FileName = root.Name,
                DirectoryCount = root.Directories?.Count ?? 0,
                FileCount = root.Files?.Count ?? 0,
                NextLevel = () =>
                {
                    DirectoriesView(root.Directories, root.Root);
                    FilesView(root.Files);
                }
            });
        }

        private void FilesView(List<File> files)
        {
            if (files == null) return;

            foreach (var file in files)
            {
                AddPlate(new FileElementInfo()
                {
                    FileName = file.Name,
                });
            }
        }

        private void AddPlate(FileElementInfo fileElementInfo)
        {
            _fileList.Add(fileElementInfo);
            recyclableScrollRect.ReloadData();
        }
    }
}
