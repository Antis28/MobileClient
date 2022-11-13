using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using MClient;
using TMPro;
using UnityEngine.UI;
using ConsoleForUnity;
using Task = System.Threading.Tasks.Task;

public class Client : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txt;

    private MobileClient mobileClient;
    //private MobileServer mobileServer;

    // Start is called before the first frame update
    void Start()
    {
        mobileClient = new MobileClient();
        txt.text = "Start";
        ConsoleInTextView.Init(txt);
        
        MobileServer.Start();
    }

    public void SendRight10()
    {
        mobileClient.StartMessages("Right x 10");
    }

    public void SendRight()
    {
        mobileClient.StartMessages("Right");
    }

    public void SendLeft10()
    {
        mobileClient.StartMessages("Left x 10");
    }

    public void SendLeft()
    {
        mobileClient.StartMessages("Left");
    }

    public void SendVolumeH()
    {
        mobileClient.StartMessages("Volume +");
    }

    public void SendVolumeL()
    {
        mobileClient.StartMessages("Volume -");
    }

    public void SendVolumeMute()
    {
        mobileClient.StartMessages("Mute");
    }

    public void SendPageDown()
    {
        mobileClient.StartMessages("PageDown");
    }

    public void SendPageUp()
    {
        mobileClient.StartMessages("PageUp");
    }

    public void SendHibernate()
    {
        mobileClient.StartMessages("Hibernate");
    }

    public void SendStandBy()
    {
        mobileClient.StartMessages("StandBy");
    }

    /// <summary>
    /// Space используется по умолчанию на сервере
    /// </summary>
    public void SendSpace()
    {
        mobileClient.StartMessages("Space");
    }

    public void SendWol()
    {
        try { WakeOnLan.WakeFunction("001E58A044B5"); } catch (Exception e)
        {
            ConsoleInTextView.ShowError($"формат : 001E58A044B5  \n{e.Message}");
            ConsoleInUnityView.ShowError(e);
        }
    }

    public void SendSaveName()
    {
        mobileClient.StartMessages("SaveName");
    }
}
