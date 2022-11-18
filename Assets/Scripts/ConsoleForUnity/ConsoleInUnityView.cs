// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ConsoleForUnity
{
    public static class ConsoleInUnityView
    {
        public static void Init()
        {
            // Регистрируем кодировки для core проекта
            // EncodingProvider inst = CodePagesEncodingProvider.Instance;
            // Encoding.RegisterProvider(inst);

            // Применяем кодировку
            // Console.OutputEncoding = Encoding.GetEncoding(866);
        }
        public static void ShowConfigServer(string localAddr, string port, string maxThreadsCount)
        {
            
            Debug.Log("Конфигурация многопоточного сервера:");
            Debug.Log($"  IP-адрес  : {localAddr}");
            Debug.Log($"  Порт      : {port}");
            Debug.Log($"  Потоки    : {maxThreadsCount}");
            Debug.Log("\nСервер запущен\n");
        }
        public static void ShowWaitServer()
        {
            Debug.Log("\nОжидание соединения... ");
        }

        public static void ShowNumberConection(string counter)
        {
            Debug.Log($"\nСоединение №{counter}!");
        }

        public static void ShowSend(string message)
        {
            Debug.Log($"Отправлено: {message}");
        }

        public static void ShowMessage(string message)
        {
            Debug.Log($"\nСообщение: {message}");
        }

        public static void ShowError(Exception e)
        {
            Debug.LogError($"Error: {e.Message}");
        }
        public static void WaitUserInput()
        {
            Debug.Log("\nНажмите Enter...");
            Console.Read();
        }

        public static void ShowConnect(string localAddr, string message)
        {
            Debug.Log($"Адрес:{localAddr}\tсообщение:{message}");
        }

        public static void ShowReceived(string message)
        {
            Debug.Log($"Получено: {message}");
        }
    }
}