using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictionScroll : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rectTransform;

    private int heightPanel = 2340;
    private PanelInfo startPanel;
    private PanelInfo playerPanel;
    private PanelInfo powerPanel;
    
    private void Start()
    {
        startPanel = new PanelInfo()
        {
            YPos = 0,
            Top = 0,
            Bottom = heightPanel * 0.10f
        };
        playerPanel = new PanelInfo()
        {
            YPos = heightPanel,
            Top = heightPanel - heightPanel * 0.2f,
            Bottom = heightPanel + heightPanel * 0.2f
        };
        powerPanel = new PanelInfo()
        {
            YPos = heightPanel * 2,
            Top = heightPanel * 2 - heightPanel * 0.2f,
            Bottom = heightPanel * 2 + heightPanel * 0.2f
        };
    }

    public void CheckPos()
    {
        var yPos = _rectTransform.localPosition.y;

        if (CheckPanel(yPos, startPanel)) return;

        if (CheckPanel(yPos, playerPanel)) return;

        if (CheckPanel(yPos, powerPanel)) return;
    }

    private bool CheckPanel(float yPos, PanelInfo panelInfo )
    {
        if (yPos > panelInfo.Top && yPos < panelInfo.Bottom)
        {
            var pos = _rectTransform.localPosition;
            pos.y = panelInfo.YPos;
            _rectTransform.localPosition = pos;
            return true;
        }

        return false;
    }
    
    private struct PanelInfo
    {
        public float YPos;
        public float Top;
        public float Bottom;
    }
}
