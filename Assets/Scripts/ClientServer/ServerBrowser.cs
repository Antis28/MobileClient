using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PaneFileBrowser;
using System.IO;
using System.Text;
using MessageObjects;
using UnityEngine;

namespace ClientServer
{
    public class ServerBrowser
    {
        private FileList _uiFileList;

        private string _path =
            @"M:\My_projects\!_Unity\MyRemoteControl\MobileClient\Assets\Scripts\ClientServer\Test.json";

        private string _myJsonResponse;

        public ServerBrowser(FileList uiFileList)
        {
            _uiFileList = uiFileList;

           // WriteJson();
            // ReadJson();
        }

        public void ShowInBrowser(string data)
        {
            var myDeserializedClass = JsonConvert.DeserializeObject<FileSystem>(data);
            _uiFileList.BuildView(myDeserializedClass);
        }

        private void ReadJson()
        {
            using (var fstream = new FileStream(_path, FileMode.Open))
            {
                // выделяем массив для считывания данных из файла
                byte[] buffer = new byte[fstream.Length];
                // считываем данные
                fstream.Read(buffer, 0, buffer.Length);
                // декодируем байты в строку
                string textFromFile = Encoding.Default.GetString(buffer);
                _myJsonResponse = textFromFile;
                ShowInBrowser(textFromFile);
                
                // var myDeserializedClass = JsonConvert.DeserializeObject<FileSystem>(_myJsonResponse);
                // _uiFileList.BuildView(myDeserializedClass);
            }
        }
        //
        // private void WriteJson()
        // {
        //     // запись в файл
        //     using (FileStream fstream = new FileStream(_path, FileMode.OpenOrCreate))
        //     {
        //         var ob = new FileSystem()
        //         {
        //             Disks = new List<Disk>()
        //             {
        //                 new Disk()
        //                 {
        //                     Label = "Arhive"
        //                 }
        //             }
        //         };
        //
        //         var text = JsonConvert.SerializeObject(ob);
        //         // преобразуем строку в байты
        //         byte[] buffer = Encoding.UTF8.GetBytes(text);
        //         // запись массива байтов в файл
        //         fstream.Write(buffer, 0, buffer.Length);
        //         Console.WriteLine("Текст записан в файл");
        //     }
        // }
    }
}
