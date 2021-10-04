using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp.Server;

namespace NeosVrCache
{
    public static class CacheInfo
    {
        public static long FileAccessCount;
        public static long FileCount;
        public static long CacheSize;
        public static bool Dirty = true;
    }

    public partial class Form1 : Form
    {
        private const string WsServiceName = "/neosVrCacheDisplay";
        private static readonly WebSocketServer WsServer = new(IPAddress.Loopback, 9099, false);


        private static readonly string[] Suf;

        private readonly Timer _timer;
        private FileSystemWatcher _fw;


        static Form1()
        {
            Suf = new[] {" B", " KB", " MB", " GB", " TB", " PB", " EB"};
        }

        public Form1()
        {
            InitializeComponent();
            Console.SetOut(new ControlWriter(textbox_console));
            CreateFw();


            WsServer.AddWebSocketService<NeosVrCacheService>(WsServiceName);
            WsServer.Start();

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Enabled = true;
            _timer.Tick += (o, args) => TimerOnTick(o, args);
            _timer.Start();
            ReadConfig();
        }

        private void CreateFw()
        {
            if (_fw != null)
            {
                _fw.Changed -= FwOnChanged;
                _fw.Dispose();
            }

            _fw = new FileSystemWatcher(Config.CachePath) {InternalBufferSize = 64000, EnableRaisingEvents = true, IncludeSubdirectories = true};
            _fw.Changed += FwOnChanged;
        }

        private void ReadConfig()
        {
            textBox_cacheLimit.Text = Config.CacheSizeLimit.ToString();
            textBox_timeLimit.Text = Config.CacheTimeLimit.ToString();
            textBox_cachePath.Text = Config.CachePath;
        }

        private void WriteConfig()
        {
            try
            {
                Config.CacheSizeLimit = float.Parse(textBox_cacheLimit.Text);
                Config.CacheTimeLimit = float.Parse(textBox_timeLimit.Text);
                Config.CachePath = textBox_cachePath.Text;
                Config.WriteConfig();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ReadConfig();
            }
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

        private void FwOnChanged(object sender, FileSystemEventArgs e)
        {
            CacheInfo.FileAccessCount++;
            CacheInfo.Dirty = true;
            if (checkBox_showCacheAcces.Checked) Console.WriteLine(e.Name);
        }

        private void UpdateCurrentSize()
        {
            var dirInfo = new DirectoryInfo(Config.CachePath);
            var resultFcount = Task.Run(() => dirInfo.GetFiles("*", SearchOption.AllDirectories).Length);
            var resultCacheSize = Task.Run(() => dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length));
            Task.WaitAll();
            CacheInfo.FileCount = resultFcount.Result;
            CacheInfo.CacheSize = resultCacheSize.Result;
            label_currentSize.Text = "Size: " + BytesToString(CacheInfo.CacheSize);
            label_currentFileCount.Text = "Files: " + CacheInfo.FileCount.ToString("N0", new CultureInfo("EN"));
            label_fileAccess.Text = "Accessed Files: " + CacheInfo.FileAccessCount.ToString("N0", new CultureInfo("EN"));
        }

        private void buttonCleanUp_Click(object sender, EventArgs e)
        {
            button_cleanup.Enabled = false;
            Console.WriteLine("Started Cleanup...");
            var sw = Stopwatch.StartNew();
            var size = 0L;
            var deleteSize = 0L;
            var deleteFcount = 0;
            try
            {
                //var tmp = Environment.GetEnvironmentVariable("TEMP");
                //var cache = Path.Combine(tmp, @"Solirax\NeosVR\Cache");


                var maxSize = Config.CacheSizeLimit * 1024L * 1024L * 1024L;
                var files = new HashSet<FileSystemInfo>();
                foreach (var file in new DirectoryInfo(Config.CachePath).GetFiles().OrderByDescending(e => e.LastAccessTime))
                    if (size < maxSize)
                    {
                        size += file.Length;
                    }
                    else if ((DateTime.UtcNow - file.LastWriteTimeUtc).TotalDays >= Config.CacheTimeLimit)
                    {
                        deleteSize += file.Length;
                        files.Add(file);
                    }

                foreach (var file in files)
                    try
                    {
                        file.Delete();
                        deleteFcount++;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }

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

        private static string BytesToString(long byteCount)
        {
            if (byteCount == 0)
                return "0" + Suf[0];
            var bytes = Math.Abs(byteCount);
            var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return $"{(Math.Sign(byteCount) * num).ToString()} {Suf[place]}";
        }

        public void SendUpdate()
        {
            WsServer.WebSocketServices[WsServiceName].Sessions.Broadcast(
                $"Cache: {BytesToString(CacheInfo.CacheSize)} || " +
                $"Files: {CacheInfo.FileCount.ToString("N0", new CultureInfo("EN"))} || " +
                $"Files Accessed: {CacheInfo.FileAccessCount.ToString("N0", new CultureInfo("EN"))}");
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            WriteConfig();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog {SelectedPath = Config.CachePath})
            {
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    textBox_cachePath.Text = dialog.SelectedPath;
                    WriteConfig();
                    CreateFw();
                    CacheInfo.Dirty = true;
                }
            }
        }
    }
}