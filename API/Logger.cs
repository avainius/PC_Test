using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace API
{
    public static class Logger
    {
        public static string LogDirectory = $@"{AppDomain.CurrentDomain.BaseDirectory}\Logs\";
        public static string LogPath = $"{LogDirectory}logs.txt";
        public static void Initialize()
        {
            if (!Directory.Exists(LogDirectory)) Directory.CreateDirectory(LogDirectory);
            if (!File.Exists(LogPath)) File.Create(LogPath).Close();
        }

        public static void Log(string msg)
        {
            if (!File.Exists(LogPath)) return;
            using (var stream = File.AppendText(LogPath))
            {
                stream.WriteLine(FullMessage(msg));
            }
        }

        public static void Log(Exception ex)
        {
            if (!File.Exists(LogPath)) return;
            using (var stream = File.AppendText(LogPath))
            {
                stream.WriteLine(FullMessage(ex));
            }
        }

        internal static string FullMessage(string msg) => $"{DateTime.Now}>> {msg}\n";
        internal static string FullMessage(Exception ex) => $"{DateTime.Now}>> Exception has been thrown: {ex.Message}\n";
    }
}