using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using PolyAndCode.UI;
using UnityEngine;
using UnityEngine.Serialization;

public struct ElementInfo
{
    public string Message;
    public string id;
}

public enum ElementType
{
    File,
    Directory,
    Disk
}

public class TestMyMessageList : MonoBehaviour, IRecyclableScrollRectDataSource
{
    private int _messageCounter = 1;

    [SerializeField]
    private RecyclableScrollRect recyclableScrollRect;

    //Data List
    private Stack<ElementInfo> _messageList;

    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {
        InitData();
        recyclableScrollRect.DataSource = this;
    }

    public void AddMessage(string message)
    {
        ElementInfo obj = new ElementInfo()
        {
            id = "item : " + _messageCounter++,
            Message = message
        };
        _messageList.Push(obj);
        recyclableScrollRect.ReloadData();
    }

    private void InitData()
    {
        _messageList = new Stack<ElementInfo>();
        if (_messageList != null) _messageList.Clear();
    }

    private void OnValidate()
    {
        if (recyclableScrollRect == null) Debug.LogAssertion("recyclableScrollRect == null");
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
        var item = cell as ElementCell;
        item?.ConfigureCell(_messageList.ToArray()[index], index);
    }

    #endregion
}
