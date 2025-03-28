﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevLab.Tools
{
    class DataController
    {
        public static void SaveProjectToGdlf(object projectData, string destinationFile)
        {
            string jsonData = JsonConvert.SerializeObject(projectData, Formatting.Indented);

            using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData)))
            {
                using (FileStream fileStream = new FileStream(destinationFile, FileMode.Create))
                {
                    using (GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Compress))
                    {
                        memoryStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        // ...existing code...
        public static T LoadProjectFromGdlf<T>(string sourceFile)
        {
            if (File.Exists(sourceFile))
            {
                using (FileStream fileStream = new FileStream(sourceFile, FileMode.Open))
                {
                    using (GZipStream decompressionStream = new GZipStream(fileStream, CompressionMode.Decompress))
                    {
                        using (StreamReader reader = new StreamReader(decompressionStream))
                        {
                            string jsonData = reader.ReadToEnd();
                            return JsonConvert.DeserializeObject<T>(jsonData);
                        }
                    }
                }
            }
            else
            {
                throw new FileNotFoundException($"Le fichier source '{sourceFile}' n'existe pas.");
            }
        }
    }
}
