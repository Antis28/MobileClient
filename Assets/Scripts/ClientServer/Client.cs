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

    public static MobileClient MobileClient;
    //private MobileServer mobileServer;

    // Start is called before the first frame update
    void Start()
    {
        LogMessageList myMessageList = FindObjectOfType<LogMessageList>();
        ConsoleInTextView.Init(myMessageList);
        ipAddressForServer.text = "192.168.0.101";
    }

    public async void StartClientAndServer()
    {
        MobileClient = new MobileClient(ipAddressForServer.text);
         await MobileServer.StartAsync(uiFileList, SendGetFileSystem);
    }
    
    public void SendGetFileSystem()
    {
        MobileClient.SendMessage("GetFileSystem");
    }
    
    public void SendRight10()
    {
        MobileClient.SendMessage("Right x 10");
    }

    public void SendRight()
    {
        MobileClient.SendMessage("Right");
    }

    public void SendLeft10()
    {
        MobileClient.SendMessage("Left x 10");
    }

    public void SendLeft()
    {
        MobileClient.SendMessage("Left");
    }

    public void SendVolumeH()
    {
        MobileClient.SendMessage("Volume +");
    }

    public void SendVolumeL()
    {
        MobileClient.SendMessage("Volume -");
    }

    public void SendVolumeMute()
    {
        MobileClient.SendMessage("Mute");
    }

    public void SendPageDown()
    {
        MobileClient.SendMessage("PageDown");
    }

    public void SendPageUp()
    {
        MobileClient.SendMessage("PageUp");
    }

    public void SendHibernate()
    {
        MobileClient.SendMessage("Hibernate");
    }

    public void SendStandBy()
    {
        MobileClient.SendMessage("StandBy");
    }

    /// <summary>
    /// Space используется по умолчанию на сервере
    /// </summary>
    public void SendSpace()
    {
        MobileClient.SendMessage("Space");
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
        MobileClient.SendMessage("SaveName");
    }
}
