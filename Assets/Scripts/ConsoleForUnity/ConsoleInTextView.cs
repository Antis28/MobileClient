// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using PanelLog;
using TMPro;
using UnityEngine;

namespace ConsoleForUnity
{
    public static class ConsoleInTextView
    {
        private static TextMeshProUGUI _console;
        private static LogMessageList _console2;


        private static readonly Color _defaultColor;

        static ConsoleInTextView()
        {
            _defaultColor = new Color(0.1647059f, 0, 0.4823529f, 1);
        }

        public static void LogInText(string msg)
        {
            LogInText(string.Empty, msg, _defaultColor);
        }
        public static void LogInText(string src, string msg)
        {
            LogInText(src, msg, _defaultColor);
        }
        private static void LogInText(string msg, Color color)
        {
            LogInText(string.Empty, msg, color);
        }

        private static void LogInText(string src, string msg, Color color)
        {
            //_console.text = $"{msg}\n{_console.text}" ;
            _console2.AddMessage(src == string.Empty ? $"{msg}" : $"{src} -> {msg}", color);

            Debug.Log($"{src} -> {msg}");
        }


        public static void Init(TextMeshProUGUI txt)
        {
            _console = txt;
        }

        public static void Init(LogMessageList txt)
        {
            _console2 = txt;
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
            ShowSend(string.Empty,message);
        }

        public static void ShowSend(string src, string message)
        {
            LogInText(src, $"Отправлено: {message}", Color.blue);
        }

        public static void ShowMessage(string message)
        {
            // green color
            LogInText($"Сообщение: {message}", new Color(0, .5f, 0, 1));
        }

        public static void ShowError(Exception e)
        {
            ShowError(e.Message);
        }

        public static void ShowError(string message)
        {
            LogInText($"Error: {message}", Color.red);
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
            // LogInText($"Получено: {message}",new Color(.6f,.4f,0,1));
            LogInText(message,new Color(.6f,.4f,0,1));
        }
    }
}
