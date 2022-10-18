// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using TMPro;

namespace ConsoleForUnity
{
    public class ConsoleInTextView
    {
        private static TextMeshProUGUI _console;

        public static void LogInText(string msg)
        {
            _console.text = $"{msg}\n{_console.text}" ;
        }

        public static void Init(TextMeshProUGUI txt)
        {
            _console = txt;
        }

        public static void ShowConfigServer(string localAddr, string port, string maxThreadsCount)
        {
            
            LogInText("Конфигурация многопоточного сервера:");
            LogInText($"  IP-адрес  : {localAddr}");
            LogInText($"  Порт      : {port}");
            LogInText($"  Потоки    : {maxThreadsCount}");
            LogInText("\nСервер запущен\n");
        }

        public static void ShowWaitServer()
        {
            LogInText("\nОжидание соединения... ");
        }

        public static void ShowNumberConection(string counter)
        {
            LogInText($"\nСоединение №{counter}!");
        }

        public static void ShowSend(string message)
        {
            LogInText($"\nОтправлено: {message}");
        }

        public static void ShowMessage(string message)
        {
            LogInText($"\nСообщение: {message}");
        }

        public static void ShowError(Exception e)
        {
            LogInText($"Error: {e.Message}");
        }
        public static void ShowError(string message)
        {
            LogInText($"Error: {message}");
        }

        public static void WaitUserInput()
        {
            LogInText("\nНажмите Enter...");
            Console.Read();
        }

        public static void ShowConnect(string localAddr, string message)
        {
            LogInText($"Адрес:{localAddr}\tсообщение:{message}");
        }

        public static void ShowReceived(string message)
        {
            LogInText($"Получено: {message}");
        }
    }
}