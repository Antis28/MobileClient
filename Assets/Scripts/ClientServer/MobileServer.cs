using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ClientServer;
using ConsoleForUnity;
using PaneFileBrowser;
using ThreadViewHelper;

public static class MobileServer
    {
        private static readonly string _localhost = "127.0.0.1";
        private static readonly int _port = 9090;

        public static async Task StartAsync(FileList uiFileList, Action action)
        {
            var hostIp = _localhost;
            var host = await Dns.GetHostEntryAsync(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork && ip.ToString().Contains("192"))
                {
                    hostIp = ip.ToString();
                }
            }
            ThreadViewer.SetPrinter(new UnityPrinter());

            ConsoleInTextView.LogInText("MobileServer -> " + hostIp);

            var server = new TcpListener(IPAddress.Parse(hostIp), _port);
            server.Start();
            var sb = new ServerBrowser(uiFileList);

            // Отсылаем запрос на получение фвйловой системы в Json формате
            ConsoleInTextView.ShowSend("Выслан запрос на JSON.");
            action?.Invoke();

            // ожидаем клиента
            ConsoleInTextView.ShowSend("Ожидаем клиента Async.");
            ThreadViewer.ThreadStarted("AcceptTcpClientAsync");
            var listener = await server.AcceptTcpClientAsync();
            ThreadViewer.ThreadEnded("AcceptTcpClientAsync");

            // Получаем сообщение от клиента
            ConsoleInTextView.ShowSend("Получаем сообщение от клиента.");
            ThreadViewer.ThreadStarted("ReadAndSendSuccessAnswer");
            string data = await ReadAndSendSuccessAnswer(listener);
            ThreadViewer.ThreadEnded("ReadAndSendSuccessAnswer");

            // Выводим Json в UI
            ConsoleInTextView.ShowSend("Выводим Json в UI.");
            sb.ShowInBrowser(data);

            // Выводим Json в журнал
            ConsoleInTextView.ShowSend("Выводим Json в журнал.");
          //  ConsoleInTextView.LogInText(data);
        }

        private static async Task<string> ReadAndSendSuccessAnswer(TcpClient tcpClient)
        {
            string result = string.Empty;
            try
            {
                NetworkStream networkStream = tcpClient.GetStream();
                StreamReader reader = new StreamReader(networkStream, Encoding.UTF8);
                StreamWriter writer = new StreamWriter(networkStream, Encoding.UTF8);
                writer.AutoFlush = true;

                while (true)
                {
                    ThreadViewer.ThreadStarted("ReadLineAsync");
                    string request = await reader.ReadLineAsync();
                    ThreadViewer.ThreadEnded("ReadLineAsync");
                    if (request != null)
                    {
                        result = request;
                        //ConsoleInTextView.ShowMessage("Полученный запрос на обслуживание: " + request);
                        string response = "Success";
                        ConsoleInTextView.ShowMessage("Отправляю ответ : " + response);
                        ThreadViewer.ThreadStarted("WriteLineAsync");
                        await writer.WriteLineAsync(response);
                        ThreadViewer.ThreadEnded("WriteLineAsync");
                    }
                    else
                    {
                        ConsoleInTextView.ShowError("Клиент закрыл соединение");
                        break; // клиент закрыл соединение
                    }
                }

                tcpClient.Close();
            } catch (Exception e) { ConsoleInTextView.ShowError(e); } finally
            {
                // Закрываем соединение.
                tcpClient.Close();
            }

            return result;
        }

        private static void WriteAnswer(Stream stream)
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

            // !!! deadlock !!!!
            while (true)
            {
                int count = 0;
                bool isContinue = false;
                try
                {
                    isContinue = stream != null &&
                                 (count = await stream.ReadAsync(responseData, 0, responseData.Length)) != 0;
                } catch (Exception e) { ConsoleInTextView.ShowError(e); }

                if (!isContinue) break;

                // Преобразуем данные в UTF8 string.
                string data = Encoding.UTF8.GetString(responseData, 0, count);
                if (data == "Response: Success") { break; }

                response.Append(data);

                WriteAnswer(stream);
            }

            return response.ToString();
        }
    }
