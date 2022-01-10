using System;
using System.Globalization;
using System.IO;
using IniParser;
using IniParser.Model;

namespace NeosVrCache
{
    public static class Config
    {
        private const string ConfigFile = "config.ini";
        private static readonly FileIniDataParser FileIniDataParser = new();

        public static float CacheSizeLimit = 16;
        public static float CacheTimeLimit = 16;
        public static string CachePath;
        public static int WsPort = 9099;

        static Config()
        {
            if (!File.Exists(ConfigFile))
            {
                var tmp = Environment.GetEnvironmentVariable("TEMP");
                CachePath = Path.Combine(tmp, @"Solirax\NeosVR\Cache");
                WriteConfig();
                return;
            }

            var data = FileIniDataParser.ReadFile(ConfigFile);
            CacheSizeLimit = float.Parse(data["NeosCacheCleaner"]["CacheSizeLimit"]);
            CacheTimeLimit = float.Parse(data["NeosCacheCleaner"]["CacheTimeLimit"]);
            CachePath = data["NeosCacheCleaner"]["CachePath"];
            WsPort = int.Parse(data["NeosCacheCleaner"]["WsPort"]);
        }

        public static void WriteConfig()
        {
            var data = new IniData();
            data["NeosCacheCleaner"]["CacheSizeLimit"] = CacheSizeLimit.ToString(CultureInfo.InvariantCulture);
            data["NeosCacheCleaner"]["CacheTimeLimit"] = CacheTimeLimit.ToString(CultureInfo.InvariantCulture);
            data["NeosCacheCleaner"]["CachePath"] = CachePath;
            data["NeosCacheCleaner"]["WsPort"] = WsPort.ToString();
            FileIniDataParser.WriteFile(ConfigFile, data);
        }
    }
}