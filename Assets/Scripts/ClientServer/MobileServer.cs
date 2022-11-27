using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ClientServer;
using ConsoleForUnity;
using PaneFileBrowser;

public static class MobileServer
{
    private static readonly string _localhost = "127.0.0.1";
    private static readonly int _port = 9090;

    public static async void Start(FileList uiFileList, Action action)
    {
        var hostIp = _localhost;
        var host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork && ip.ToString().Contains("192")) { hostIp = ip.ToString(); }
        }
        ConsoleInTextView.LogInText("MobileServer -> " + hostIp);
        
        var server = new TcpListener(IPAddress.Parse(hostIp), _port);
        server.Start();
        var sb = new ServerBrowser(uiFileList);
        
        // Отсылаем запрос на получение фвйловой системы в Json формате
        action?.Invoke();
        
        // ожидаем клиента
        var listener = await server.AcceptTcpClientAsync();
        // получаем сообщение от клиента
        string data =  await ReadAndSendSuccessAnswer(listener);
        
        // Выводим Json в UI
        sb.ShowInBrowser(data);
        
        // Выводим Json в журнал
        ConsoleInTextView.LogInText(data);
    }
    
    private static async Task<string> ReadAndSendSuccessAnswer(TcpClient client)
    {
        var response = string.Empty;
        try
        {
            // Получаем информацию от клиента
            var stream = client.GetStream();
            response = await  ReadMessage(stream);

            WriteAnswer(stream);
        } catch (Exception e) { Console.WriteLine(e); } finally
        {
            // Закрываем соединение.
            client.Close();
        }

        return response;
    }

    private static void WriteAnswer(NetworkStream stream)
    {
        // Преобразуем полученную строку в массив Байт.
        var msg = Encoding.UTF8.GetBytes("Response: Success");

        // Отправляем данные обратно клиенту (ответ).
        stream.Write(msg, 0, msg.Length);
    }

    private static async Task<string> ReadMessage(Stream stream)
    {
        // StringBuilder для склеивания полученных данных в одну строку
        var response = new StringBuilder();
        
        // буфер для получения данных
        var responseData = new byte[1024];
        int count;
        // !!! deadlock !!!!
        while ((count = await stream.ReadAsync(responseData, 0, responseData.Length)) != 0)
        {
            // Преобразуем данные в UTF8 string.
            String data = Encoding.UTF8.GetString(responseData, 0, count);
            response.Append(data);
        }

        return response.ToString();
    }
}
