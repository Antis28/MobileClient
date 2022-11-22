using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;

namespace PanelLog
{
    public class LogElementCell : MonoBehaviour, ICell
    {
        //UI
        public Text idLabel;
        public Text nameLabel;
        public Image imageLabel;
        
        //Model
        private LogElementInfo _logElementInfo;
        private int _cellIndex;
        
        
        private void OnValidate()
        {
            if( idLabel == null) Debug.LogAssertion("idLabel == null");
            if( nameLabel == null) Debug.LogAssertion("nameLabel == null");
            if( imageLabel == null) Debug.LogAssertion("imageLabel == null");
        }
        
        private void Start()
        {
            //Can also be done in the inspector
            GetComponent<Button>().onClick.AddListener(ButtonListener);
        }
        
        //This is called from the SetCell method in DataSource
        public void ConfigureCell(LogElementInfo logElementInfo,int cellIndex)
        {
            _cellIndex = cellIndex;
            _logElementInfo = logElementInfo;
            
            nameLabel.text = _logElementInfo.Message;
            idLabel.text = _logElementInfo.id;
        }
        
        private void ButtonListener()
        {
            Debug.Log("Index : " + _cellIndex +  ", Name : " + _logElementInfo.Message);
        }
    }
}
