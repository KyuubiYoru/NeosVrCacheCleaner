﻿using System;
using System.IO;
using IniParser;
using IniParser.Model;

namespace NeosVrCache
{
    public static class Config
    {
        private const string configFile = "config.ini";
        private static readonly FileIniDataParser _fileIniDataParser = new();

        public static float CacheSizeLimit = 16;
        public static float CacheTimeLimit = 16;
        public static string CachePath;
        public static int WsPort = 9099;

        static Config()
        {
            if (!File.Exists(configFile))
            {
                var tmp = Environment.GetEnvironmentVariable("TEMP");
                CachePath = Path.Combine(tmp, @"Solirax\NeosVR\Cache");
                WriteConfig();
                return;
            }

            var data = _fileIniDataParser.ReadFile(configFile);
            CacheSizeLimit = float.Parse(data["NeosCacheCleaner"]["CacheSizeLimit"]);
            CacheTimeLimit = float.Parse(data["NeosCacheCleaner"]["CacheTimeLimit"]);
            CachePath = data["NeosCacheCleaner"]["CachePath"];
            WsPort = int.Parse(data["NeosCacheCleaner"]["WsPort"]);
        }

        public static void WriteConfig()
        {
            var data = new IniData();
            data["NeosCacheCleaner"]["CacheSizeLimit"] = CacheSizeLimit.ToString();
            data["NeosCacheCleaner"]["CacheTimeLimit"] = CacheTimeLimit.ToString();
            data["NeosCacheCleaner"]["CachePath"] = CachePath;
            data["NeosCacheCleaner"]["WsPort"] = WsPort.ToString();
            _fileIniDataParser.WriteFile(configFile, data);
        }
    }
}