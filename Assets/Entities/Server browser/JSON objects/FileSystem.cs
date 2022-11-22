using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Entities.Server_browser.JSON_objects
{
    // FileSystem myDeserializedClass = JsonConvert.DeserializeObject<FileSystem>(myJsonResponse);
    public class FileSystem
    {
        public List<Disk> Disks { get; set; }
        
        public static explicit operator FileSystem(Directory d)
        {
            return new FileSystem()
            {
                Disks = d.Directories.Cast<Disk>().ToList()
            };
        }
        public static explicit operator Directory(FileSystem f)
        {
            return new Directory()
            {
                Name = "Root",
                Directories = f.Disks.Cast<Directory>().ToList()
            };
        }
    }

    public class Disk : Directory
    {
        public string Label { get; set; }
    }

    public class Directory
    {
        public virtual string Name { get; set; }
        public List<Directory> Directories { get; set; }
        public List<File> Files { get; set; }

        [JsonIgnoreAttribute] public Directory Root { get; set; }
    }

    public class File
    {
        public string Name { get; set; }
    }
}
