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

    struct PanelInfo
    {
        public float yPos;
        public float top;
        public float bottom;
    }

    private void Start()
    {
        startPanel = new PanelInfo()
        {
            yPos = 0,
            top = 0,
            bottom = heightPanel * 0.10f
        };
        playerPanel = new PanelInfo()
        {
            yPos = heightPanel,
            top = heightPanel - heightPanel * 0.2f,
            bottom = heightPanel + heightPanel * 0.2f
        };
        powerPanel = new PanelInfo()
        {
            yPos = heightPanel * 2,
            top = heightPanel * 2 - heightPanel * 0.2f,
            bottom = heightPanel * 2 + heightPanel * 0.2f
        };
    }

    public void CheckPos()
    {
        var yPos = _rectTransform.localPosition.y;

        if (CheckStartPanel(yPos)) return;

        if (CheckPlayerPanel(yPos)) return;

        CheckPowerPanel(yPos);
    }

    private bool CheckPanel(float yPos, PanelInfo panelInfo )
    {
        if (yPos > panelInfo.top && yPos < panelInfo.bottom)
        {
            var pos = _rectTransform.localPosition;
            pos.y = panelInfo.yPos;
            _rectTransform.localPosition = pos;
            return true;
        }

        return false;
    }

    private bool CheckPowerPanel(float yPos)
    {
        return CheckPanel(yPos, powerPanel);
        // if (yPos > powerPanelTop && yPos < powerPanelBottom)
        // {
        //     var pos = _rectTransform.localPosition;
        //     pos.y = heightPanel * 2;
        //     _rectTransform.localPosition = pos;
        //     return;
        // }
    }

    private bool CheckPlayerPanel(float yPos)
    {
        return CheckPanel(yPos, playerPanel);
        // if (yPos > playerPanelTop && yPos < playerPanelBottom)
        // {
        //     var pos = _rectTransform.localPosition;
        //     pos.y = heightPanel;
        //     _rectTransform.localPosition = pos;
        //     return true;
        // }
        //
        // return false;
    }

    private bool CheckStartPanel(float yPos)
    {
        return CheckPanel(yPos, startPanel);
        // if (yPos < startPanel)
        // {
        //     var pos = _rectTransform.localPosition;
        //     pos.y = 0;
        //     _rectTransform.localPosition = pos;
        //     return true;
        // }
        //
        // return false;
    }
}
