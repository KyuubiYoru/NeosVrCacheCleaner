﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp.Server;

namespace NeosVrCache
{
    public static class CacheInfo
    {
        public static long FileAccesCount = 0;
        public static long FileCount = 0;
        public static long CacheSize = 0;
        public static bool Dirty = true;
    }

    public partial class Form1 : Form
    {
        private const string wsServiceName = "/neosVrCacheDisplay";
        private static readonly WebSocketServer wsServer = new WebSocketServer(IPAddress.Loopback, 9099, false);
        public static readonly string CachePath = @"E:\NeosVR\Cache";
        FileSystemWatcher fw = new FileSystemWatcher(CachePath);

        private Timer _timer;

        public Form1()
        {
            InitializeComponent();
            Console.SetOut(new ControlWriter(textbox_console));
            fw.InternalBufferSize = 64000;
            fw.EnableRaisingEvents = true;
            fw.IncludeSubdirectories = true;
            fw.Error += FwOnError;
            fw.Changed += FwOnChanged;
            
            wsServer.AddWebSocketService<NeosVrCacheService>(wsServiceName);
            wsServer.Start();

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Enabled = true;
            _timer.Tick += (o, args) => TimerOnTick(o, args);
            _timer.Start();

        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            if (CacheInfo.Dirty)
            {
                UpdateCurrentSize();
                SendUpdate();
                CacheInfo.Dirty = false;
            }
        }


        private void FwOnError(object sender, ErrorEventArgs e)
        {
            label2.Text = e.ToString();
        }

        private void FwOnChanged(object sender, FileSystemEventArgs e)
        {
            CacheInfo.FileAccesCount++;
            CacheInfo.Dirty = true;
            if (checkBox_showCacheAcces.Checked)
            {
                Console.WriteLine(e.Name);
            }
        }

        private async void UpdateCurrentSize()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Form1.CachePath);
            CacheInfo.FileCount = dirInfo.GetFiles("*", SearchOption.AllDirectories).Length;
            CacheInfo.CacheSize = await Task.Run(() => dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length));
            label_currentSize.Text = "Size: " + BytesToString(CacheInfo.CacheSize);
            label_currentFileCount.Text = "Files: " + CacheInfo.FileCount.ToString("N0", new CultureInfo("EN"));
            label_fileAccess.Text = "Accessed Files: " + CacheInfo.FileAccesCount.ToString("N0", new CultureInfo("EN"));
        }

        private void buttonCleanUp_Click(object sender, EventArgs e)
        {
            button_cleanup.Enabled = false;
            Console.WriteLine("Started Cleanup...");
            Stopwatch sw = Stopwatch.StartNew();
            var size = 0L;
            var deleteSize = 0L;
            var deleteFcount = 0;
            try
            {
                //var tmp = Environment.GetEnvironmentVariable("TEMP");
                //var cache = Path.Combine(tmp, @"Solirax\NeosVR\Cache");


                var maxSize = long.Parse(textBox_cacheLimit.Text) * 1024L * 1024L * 1024L;
                var files = new HashSet<FileSystemInfo>();
                foreach (var file in new DirectoryInfo(CachePath).GetFiles().OrderByDescending(e => e.LastAccessTime))
                {
                    if (size < maxSize)
                    {
                        size += file.Length;
                    }
                    else if ((DateTime.UtcNow - file.LastWriteTimeUtc).TotalDays >= float.Parse(textBox_timeLimit.Text))
                    {
                        deleteSize += file.Length;
                        files.Add(file);
                    }
                }

                foreach (var file in files)
                {
                    try
                    {
                        file.Delete();
                        deleteFcount++;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                }

                label_deletedFcount.Text = "Files: " + deleteFcount.ToString("N0", new CultureInfo("EN"));
                label_deletedSize.Text = "Size: " + BytesToString(deleteSize);
                CacheInfo.Dirty = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                sw.Stop();
                button_cleanup.Enabled = true;
                Console.WriteLine($"Cleanup done in {sw.Elapsed.ToString()}");
                Console.WriteLine($"Deleted {BytesToString(deleteSize)} {deleteFcount} Files");
            }
        }

        static String BytesToString(long byteCount)
        {
            string[] suf = {" B", " KB", " MB", " GB", " TB", " PB", " EB"}; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        public void SendUpdate()
        {
            wsServer.WebSocketServices[wsServiceName].Sessions.Broadcast(
                $"Cache: {BytesToString(CacheInfo.CacheSize)} || " +
                $"Files: {CacheInfo.FileCount.ToString("N0", new CultureInfo("EN"))} || " +
                $"Files Accessed: {CacheInfo.FileAccesCount.ToString("N0", new CultureInfo("EN"))}");
        }
    }
}