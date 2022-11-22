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
        private TMP_Text  nameLabel;
        [SerializeField]
        private TMP_Text  filesCount;
        [SerializeField]
        private TMP_Text  directoryCount;
        
        //Model
        private FileElementInfo _fileElementInfo;
        private int _cellIndex;
        
        
        private void OnValidate()
        {
            if( nameLabel == null) Debug.LogAssertion("nameLabel == null");
            if( filesCount == null) Debug.LogAssertion("filesCount == null");
            if( directoryCount == null) Debug.LogAssertion("directoryCount == null");
        }
        
        private void Start()
        {
            //Can also be done in the inspector
            GetComponent<Button>().onClick.AddListener(ButtonListener);
        }
        
        //This is called from the SetCell method in DataSource
        public void ConfigureCell(FileElementInfo fileElementInfo,int cellIndex)
        {
            _cellIndex = cellIndex;
            _fileElementInfo = fileElementInfo;
            
            nameLabel.text = _fileElementInfo.FileName;
            filesCount.text = _fileElementInfo.FileCount.ToString();
            directoryCount.text = _fileElementInfo.DirectoryCount.ToString();
        }
        
        private void ButtonListener()
        {
            _fileElementInfo.NextLevel();
            Debug.Log("Name : " + _fileElementInfo.FileName);
        }
    }
}
