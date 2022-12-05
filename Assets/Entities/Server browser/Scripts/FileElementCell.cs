using System;
using System.IO;
using ConsoleForUnity;
using MClient;
using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;
using TMPro;

namespace PaneFileBrowser
{
    public class FileElementCell : MonoBehaviour, ICell
    {
        //UI
        [SerializeField]
        private TMP_Text nameLabel;

        [SerializeField]
        private Image iconSprite;

        [SerializeField]
        private TMP_Text filesCount;

        [SerializeField]
        private TMP_Text directoryCount;

        //Model
        private FileElementInfo _fileElementInfo;
        private int _cellIndex;


        private void OnValidate()
        {
            if (nameLabel == null) Debug.LogAssertion("nameLabel == null");
            if (filesCount == null) Debug.LogAssertion("filesCount == null");
            if (directoryCount == null) Debug.LogAssertion("directoryCount == null");
        }

        private void Start()
        {
            //Can also be done in the inspector
            GetComponent<Button>().onClick.AddListener(ButtonListener);
        }

        //This is called from the SetCell method in DataSource
        public void ConfigureCell(FileElementInfo fileElementInfo, int cellIndex)
        {
            _cellIndex = cellIndex;
            _fileElementInfo = fileElementInfo;

            iconSprite.sprite = _fileElementInfo.icon;
            nameLabel.text = _fileElementInfo.FileName;
            filesCount.text = _fileElementInfo.FileCount.ToString();
            directoryCount.text = _fileElementInfo.DirectoryCount.ToString();
        }

        private void ButtonListener()
        {
            _fileElementInfo.NextLevel?.Invoke();
            ConsoleInTextView.LogInText("Name : " + _fileElementInfo.FileName);


            if (_fileElementInfo.FileName.EndsWith(@":\)"))
            {
                ConsoleInTextView.LogInText("Its a disk");
                return;
            }
            
            
            for (int i = _fileElementInfo.FileName.Length-1; i >= 0; i--)
            {
                var character = _fileElementInfo.FileName[i];
            
                ConsoleInTextView.LogInText("character -> " + character);
                if (character == '.')
                {
                    ConsoleInTextView.LogInText("Exception -> ExecutableFile" + _fileElementInfo.FileName);
                    Client.MobileClient.SendMessage("ExecutableFile", _fileElementInfo.FileName);
                    return;
                }
            }

            // try
            // {
            //     // get the file attributes for file or directory
            //     FileAttributes attr = File.GetAttributes(_fileElementInfo.FileName);
            //
            //     //detect whether its a directory or file
            //     if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            //         // Its a directory
            //         ConsoleInTextView.LogInText("Its a directory");
            //     else
            //     {
            //         ConsoleInTextView.LogInText("Its a file -> ExecutableFile");
            //         Client.MobileClient.SendMessage("ExecutableFile", _fileElementInfo.FileName);
            //     }
            // } catch (Exception e)
            // {
            //     ConsoleInTextView.LogInText("Exception -> " + _fileElementInfo.FileName + _fileElementInfo.FileName.Length);
            //
            //     
            // }
        }
    }
}
