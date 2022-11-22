using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;

namespace DefaultNamespace
{
    public class ElementCell : MonoBehaviour, ICell
    {
        //UI
        public Text idLabel;
        public Text nameLabel;
        public Image imageLabel;
        
        //Model
        private ElementInfo _elementInfo;
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
        public void ConfigureCell(ElementInfo elementInfo,int cellIndex)
        {
            _cellIndex = cellIndex;
            _elementInfo = elementInfo;
            
            nameLabel.text = _elementInfo.Message;
            idLabel.text = _elementInfo.id;
        }
        
        private void ButtonListener()
        {
            Debug.Log("Index : " + _cellIndex +  ", Name : " + _elementInfo.Message);
        }
    }
}
