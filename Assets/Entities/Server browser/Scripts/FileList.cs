using System;
using System.Collections;
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
            recyclableScrollRect.ReloadData();
        }


        private void DirectoriesView(Directory directory)
        {
            _fileList.Clear();
            AddLinkToRoot(directory);
            if (directory.Directories == null) return;

            foreach (var subDirectory in directory.Directories)
            {
                subDirectory.Root = directory;
                var element = CreateFileElementInfo(subDirectory);
                AddPlate(element);
            }
            recyclableScrollRect.ReloadData();
        }

        private FileElementInfo CreateFileElementInfo(Disk disk)
        {
            return new FileElementInfo()
            {
                FileName = $"{disk.Label} ({disk.Name}:)",
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

        private FileElementInfo CreateFileElementInfo(Directory directory)
        {
            return new FileElementInfo
            {
                FileName = directory.Name,
                DirectoryCount = directory.Directories?.Count ?? 0,
                FileCount = directory.Files?.Count ?? 0,
                icon = directoryIcon,
                NextLevel = () =>
                {
                    DirectoriesView(directory);
                    FilesView(directory.Files);
                }
            };
        }

        private void AddLinkToRoot(Directory directory)
        {
            if (directory is Disk)
            {
                var disk = directory as Disk;
                AddPlate(new FileElementInfo()
                {
                    FileName = $"{disk.Label} ({disk.Name}:)",
                    DirectoryCount = directory.Directories?.Count ?? 0,
                    FileCount = directory.Files?.Count ?? 0,
                    icon = driveIcon,
                    NextLevel = () => { DiskView(_fileSystem.Disks); }
                });
                return;
            }

            if (directory == null)
            {
                Debug.LogError("root == null");
                return;
            }

            AddPlate(new FileElementInfo()
            {
                FileName = directory.Name,
                DirectoryCount = directory.Directories?.Count ?? 0,
                FileCount = directory.Files?.Count ?? 0,
                icon = directoryIcon,
                NextLevel = () =>
                {
                    DirectoriesView(directory.Root);
                    FilesView(directory.Root?.Files);
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
                    icon = fileIcon
                });
            }
        }

        private void AddPlate(FileElementInfo fileElementInfo)
        {
            _fileList.Add(fileElementInfo);
        }
    }
}
