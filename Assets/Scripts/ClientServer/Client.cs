using System;
using System.Threading.Tasks;
using ClientServer;
using UnityEngine;
using MClient;
using TMPro;
using ConsoleForUnity;
using PaneFileBrowser;
using PanelLog;
using ThreadViewHelper;

public class Client : MonoBehaviour
{
    private string _playerName; //"Pot Player" or "YouTube Player" 

    [SerializeField]
    private TMP_InputField ipAddressForServer;

    [SerializeField]
    private FileList uiFileList; // для вывода информации JSON файла структуры файловой системы компьютера

    [SerializeField]
    private TMP_Text playerNameText;

    public static MobileClient MobileClient;
    //private MobileServer mobileServer;

    // Start is called before the first frame update
    void Start()
    {
        ThreadViewer.SetPrinter(new UnityPrinter());
        SwitchPlayerName("Pot Player");
        LogMessageList myMessageList = FindObjectOfType<LogMessageList>();
        ConsoleInTextView.Init(myMessageList);
        ipAddressForServer.text = "192.168.0.101";
    }

    public async void StartClientAndServer()
    {
        MobileClient = new MobileClient(ipAddressForServer.text);

        // Отсылаем запрос на получение фавйловой системы в Json формате
        SendGetFileSystem(null);
        ConsoleInTextView.ShowSend("Выслан запрос на JSON.");

        var data = await MobileServer.AwaitMessageAsync();

        // Выводим Json в UI
        ConsoleInTextView.ShowSend("Выводим Json в UI.");
        var sb = new ServerBrowser(uiFileList);
        sb.ShowInBrowser(data);
        ConsoleInTextView.LogInText("Json в UI завершен.");

        await Task.Delay(5000);
        // Поторно запрашиваем файловую систему, но уже глубже
        SendGetFileSystem("4");
        ConsoleInTextView.ShowSend("Выслан повторный запрос на JSON.");
        data = await MobileServer.AwaitMessageAsync();
        
        sb.UpdateFs(data);

        ConsoleInTextView.LogInText("StartClientAndServer Ended");
    }

    public void SendGetFileSystem(string deep)
    {
        if (string.IsNullOrEmpty(deep))
        {
            deep = "1";
        }
        
        MobileClient.SendMessage("GetFileSystem", deep);
    }

    public void SendRight10()
    {
        MobileClient.SendMessage("Right x 10", _playerName);
    }

    public void SendRight()
    {
        MobileClient.SendMessage("Right", _playerName);
    }

    public void SendLeft10()
    {
        MobileClient.SendMessage("Left x 10", _playerName);
    }

    public void SendLeft()
    {
        MobileClient.SendMessage("Left", _playerName);
    }

    public void SendVolumeH()
    {
        MobileClient.SendMessage("Volume +", _playerName);
    }

    public void SendVolumeL()
    {
        MobileClient.SendMessage("Volume -", _playerName);
    }

    public void SendVolumeMute()
    {
        MobileClient.SendMessage("Mute", _playerName);
    }

    public void SendPageDown()
    {
        MobileClient.SendMessage("PageDown", _playerName);
    }

    public void SendPageUp()
    {
        MobileClient.SendMessage("PageUp", _playerName);
    }

    public void SendHibernate()
    {
        MobileClient.SendMessage("Hibernate", _playerName);
    }

    public void SendStandBy()
    {
        MobileClient.SendMessage("StandBy", _playerName);
    }

    /// <summary>
    /// Space используется по умолчанию на сервере
    /// </summary>
    public void SendSpace()
    {
        MobileClient.SendMessage("Space", _playerName);
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

    public void SwitchPlayerName(string name)
    {
        playerNameText.text = name;
        _playerName = name;
    }
}
