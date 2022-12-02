using System;
using UnityEngine;
using MClient;
using TMPro;
using ConsoleForUnity;
using PaneFileBrowser;
using PanelLog;

public class Client : MonoBehaviour
{
    
    [SerializeField]
    private TMP_InputField ipAddressForServer;
    [SerializeField]
    private FileList uiFileList; // для вывода информации JSON файла структуры файловой системы компьютера

    private MobileClient _mobileClient;
    //private MobileServer mobileServer;

    // Start is called before the first frame update
    void Start()
    {
        LogMessageList myMessageList = FindObjectOfType<LogMessageList>();
        ConsoleInTextView.Init(myMessageList);
        ipAddressForServer.text = "192.168.0.101";
    }

    public void StartClientAndServer()
    {
        _mobileClient = new MobileClient(ipAddressForServer.text);
         MobileServer.StartAsync(uiFileList, SendGetFileSystem);
    }
    
    public void SendGetFileSystem()
    {
        _mobileClient.StartMessages("GetFileSystem");
    }
    
    public void SendRight10()
    {
        _mobileClient.StartMessages("Right x 10");
    }

    public void SendRight()
    {
        _mobileClient.StartMessages("Right");
    }

    public void SendLeft10()
    {
        _mobileClient.StartMessages("Left x 10");
    }

    public void SendLeft()
    {
        _mobileClient.StartMessages("Left");
    }

    public void SendVolumeH()
    {
        _mobileClient.StartMessages("Volume +");
    }

    public void SendVolumeL()
    {
        _mobileClient.StartMessages("Volume -");
    }

    public void SendVolumeMute()
    {
        _mobileClient.StartMessages("Mute");
    }

    public void SendPageDown()
    {
        _mobileClient.StartMessages("PageDown");
    }

    public void SendPageUp()
    {
        _mobileClient.StartMessages("PageUp");
    }

    public void SendHibernate()
    {
        _mobileClient.StartMessages("Hibernate");
    }

    public void SendStandBy()
    {
        _mobileClient.StartMessages("StandBy");
    }

    /// <summary>
    /// Space используется по умолчанию на сервере
    /// </summary>
    public void SendSpace()
    {
        _mobileClient.StartMessages("Space");
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
        _mobileClient.StartMessages("SaveName");
    }
}
