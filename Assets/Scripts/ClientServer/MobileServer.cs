using System;
using System.Net;
using System.Net.Sockets;
using ConsoleForUnity;

public static class MobileServer
{
    private static readonly string _localhost = "127.0.0.1";
    private static readonly int _port = 9090;

    public async static void Start()
    {
        var hostIp = _localhost;
        var host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork) { hostIp = ip.ToString(); }
        }
        ConsoleInTextView.LogInText("MobileServer -> " + hostIp);
        
        var server = new TcpListener(IPAddress.Parse(hostIp), _port);
        server.Start();
        while (true)
        {
            var listener = await server.AcceptTcpClientAsync();
            string data = ReadAndSendSuccessAnswer(listener);
            ConsoleInTextView.LogInText(data);
        }
    }
    
    private static string ReadAndSendSuccessAnswer(TcpClient client)
    {
        // Буфер для принимаемых данных.
        Byte[] bytes = new Byte[1024];
        String data = String.Empty;
        
        try
        {
            // Получаем информацию от клиента
            var stream = client.GetStream();
            int count;
            while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Преобразуем данные в UTF8 string.
                data = System.Text.Encoding.UTF8.GetString(bytes, 0, count);

                // Преобразуем полученную строку в массив Байт.
                var msg = System.Text.Encoding.UTF8.GetBytes("Данные получены мобильным клиентом");

                // Отправляем данные обратно клиенту (ответ).
                stream.Write(msg, 0, msg.Length);
            }
        } catch (Exception e) { Console.WriteLine(e); } finally
        {
            // Закрываем соединение.
            client.Close();
        }

        return data;
    }
}
