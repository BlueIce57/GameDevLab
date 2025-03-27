using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDevLab.Tools
{
    class DataController
    {
        public static void Compress(string sourceDirectory, string destinationFile)
        {
            if (Directory.Exists(sourceDirectory))
            {
                ZipFile.CreateFromDirectory(sourceDirectory, destinationFile);
            }
            else
            {
                throw new DirectoryNotFoundException($"Le répertoire source '{sourceDirectory}' n'existe pas.");
            }
        }

        public static void Decompress(string sourceFile, string destinationDirectory)
        {
            if (File.Exists(sourceFile))
            {
                ZipFile.ExtractToDirectory(sourceFile, destinationDirectory);
            }
            else
            {
                throw new FileNotFoundException($"Le fichier source '{sourceFile}' n'existe pas.");
            }
        }
    }
}
