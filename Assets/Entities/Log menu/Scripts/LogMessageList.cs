using System.Collections.Generic;
using PolyAndCode.UI;
using UnityEngine;
using TMPro;

namespace PanelLog
{
    public class LogMessageList : MonoBehaviour, IRecyclableScrollRectDataSource
    {
        private int _messageCounter = 1;

        [SerializeField]
        private RecyclableScrollRect logScrollList;

        //Data List
        private Stack<LogElementInfo> _messageList;

        //Recyclable scroll rect's data source must be assigned in Awake.
        private void Awake()
        {
            InitData();
            logScrollList.DataSource = this;
            
        }

        public void AddMessage(string message)
        {
            //цвет по умолчанию
            AddMessage(message, Color.black);
        }

        public void AddMessage(string message, Color color)
        {
            var obj = new LogElementInfo()
            {
                Id = "item : " + _messageCounter++,
                Message = message,
                ColorMessage = color
            };
            _messageList.Push(obj);
            logScrollList.ReloadData();
        }

        private void InitData()
        {
            _messageList = new Stack<LogElementInfo>();
        }

        private void OnValidate()
        {
            if (logScrollList == null) Debug.LogAssertion("recyclableScrollRect == null");
        }

        #region DATA-SOURCE

        public int GetItemCount()
        {
            return _messageList.Count;
        }

        /// <summary>
        /// Data source method. Called for a cell every time it is recycled.
        /// Implement this method to do the necessary cell configuration.
        /// </summary>
        public void SetCell(ICell cell, int index)
        {
            //Casting to the implemented Cell
            var item = cell as LogElementCell;
            item?.ConfigureCell(_messageList.ToArray()[index], index);
        }

        #endregion
    }
}
