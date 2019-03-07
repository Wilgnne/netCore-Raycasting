using System;
using System.IO;
using System.Text;

namespace netCore_Raycasting.Modules.RayCasting
{
    public static class Engine
    {

        public static float horizont = 0;
        public static float maxDistance = 10f;

        public static void FileWrite (string addr, string content)
        {
            string filePath = @""+addr;
            // Delete the file if it exists.
            if (File.Exists(filePath))
                File.Delete(filePath);

            // Create the file.
            using (FileStream fs = File.Create(filePath))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(content);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
                fs.Close();
            }
        }
        public static void ExportViewPGM (Player player, Map Mapa, string adrr)
        {
            float angleInc = player.FOV;
        }
    }
}