using System;
using System.Collections.Generic;
using System.IO;
using ConsoleForUnity;
using PolyAndCode.UI;
using UnityEngine;
using MessageObjects;
using Directory = MessageObjects.Directory;
using File = MessageObjects.File;


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
            if (_fileSystem == null)
            {
                ConsoleInTextView.LogInText("!!! _fileSystem == null");
                return;
            }

            DiskView(_fileSystem.Disks);
        }

        private void DiskView(List<Disk> disks)
        {
            _fileList.Clear();
            if (disks == null) return;

            foreach (var disk in disks)
            {
                var element = CreateDiskElementInfo(disk);
                AddPlate(element);
            }
        }

        private void DirectoriesView(Directory directory)
        {
            _fileList.Clear();
            AddLinkToRoot(directory);
            if (directory.Directories == null) return;

            foreach (var subDirectory in directory.Directories)
            {
                subDirectory.Root = directory;
                var element = CreateDirectoryElementInfo(subDirectory, () =>
                {
                    DirectoriesView(directory);
                    FilesView(directory.Files);
                });
                AddPlate(element);
            }
           
        }

        private void AddLinkToRoot(Directory directory)
        {
            if (directory is Disk disk)
            {
                CreateDiskElementInfo(disk);
                return;
            }
            
            if (directory == null)
            {
                Debug.LogError("root == null");
                return;
            }

            var element = CreateDirectoryElementInfo(directory, () =>
                {
                    DirectoriesView(directory.Root);
                    FilesView(directory.Root?.Files);
                }
            );
            AddPlate(element);
        }

        private void FilesView(List<File> files)
        {
            if (files == null) return;

            foreach (var file in files)
            {
                var element = new FileElementInfo()
                {
                    FileName = file.Name,
                    icon = fileIcon
                };
                AddPlate(element);
            }
        }

        private void AddPlate(FileElementInfo fileElementInfo)
        {
            _fileList.Add(fileElementInfo);
            recyclableScrollRect.ReloadData();
        }
        
        private FileElementInfo CreateDiskElementInfo(Disk disk)
        {
            return new FileElementInfo()
            {
                FileName = $"{disk.Label} ({disk.Name})",
                DirectoryCount = disk.Directories?.Count ?? 0,
                FileCount = disk.Files?.Count ?? 0,
                icon = driveIcon,
                NextLevel = () =>
                {
                    DirectoriesView(disk);
                    FilesView(disk.Files);
                }
            };
        }

        private FileElementInfo CreateDirectoryElementInfo(Directory directory, Action action)
        {
            return new FileElementInfo
            {
                FileName = directory.Name,
                DirectoryCount = directory.Directories?.Count ?? 0,
                FileCount = directory.Files?.Count ?? 0,
                icon = directoryIcon,
                NextLevel = action
            };
        }
    }
}
