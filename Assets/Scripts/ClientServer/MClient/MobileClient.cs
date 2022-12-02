// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Net.Sockets;
using ConsoleForUnity;
using UnityEngine;

namespace MClient
{
    public class MobileClient
    {
        string hostIp = string.Empty;

        public MobileClient()
        {
            hostIp = "192.168.1.101";
            ConsoleInTextView.LogInText("MobileClient -> " + hostIp);
        }

        public MobileClient(string address)
        {
            hostIp = address;
            ConsoleInTextView.LogInText(GetType().Name, hostIp);
        }

        public void StartMessages(string message)
        {
            try
            {
                ConsoleInTextView.LogInText(GetType().Name, hostIp);
                Connect(hostIp, message);
            } catch (Exception e) { ConsoleInUnityView.ShowError(e); }
        }

        static async void  Connect(string server, string message)
        {
            try
            {
                // Создаём TcpClient.
                // Для созданного в предыдущем проекте TcpListener 
                // Настраиваем его на IP нашего сервера и тот же порт.

                Int32 port = 9595;
                TcpClient client = new TcpClient();
                await client.ConnectAsync(server, port);
                
                // Переводим наше сообщение в UTF8, а затем в массив Byte.
                Byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
                // Получаем поток для чтения и записи данных.
                NetworkStream stream = client.GetStream();
                
                // Отправляем сообщение нашему серверу. 
                await stream.WriteAsync(data, 0, data.Length);
                ConsoleInTextView.ShowSend(message);

                // Получаем ответ от сервера.
                // Буфер для хранения принятого массива bytes.
                data = new Byte[256];
                // Строка для хранения полученных ASCII данных.
                // Читаем первый пакет ответа сервера. 
                // Можно читать всё сообщение.
                // Для этого надо организовать чтение в цикле как на сервере.
                Int32 bytes = await stream.ReadAsync(data, 0, data.Length);
                var responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
                ConsoleInTextView.ShowMessage(responseData);
                
                // Закрываем всё.
                stream.Close();
                client.Close();
            } catch (ArgumentNullException e) { ConsoleInTextView.ShowError(e); } catch (SocketException e)
            {
                ConsoleInTextView.ShowError(e);
            }
        }
    }
}
