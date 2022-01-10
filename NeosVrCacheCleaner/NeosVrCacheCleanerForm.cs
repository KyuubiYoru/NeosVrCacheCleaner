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
    public partial class NeosVrCacheCleanerForm : Form
    {
        private const string WsServiceName = "/neosVrCacheDisplay";
        private static WebSocketServer _wsServer;


        private static readonly string[] Suf;

        private readonly Timer _timer;
        private FileSystemWatcher _fw;


        static NeosVrCacheCleanerForm()
        {
            Suf = new[] {" B", " KB", " MB", " GB", " TB", " PB", " EB"};
        }

        public NeosVrCacheCleanerForm()
        {
            InitializeComponent();
            Console.SetOut(new ControlWriter(textbox_console));
            ReadConfig();
            CreateFw();


            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Enabled = true;
            _timer.Tick += (o, args) => TimerOnTick(o, args);
            _timer.Start();

            try
            {
                _wsServer = new WebSocketServer(IPAddress.Loopback, Config.WsPort, false);
                _wsServer.AddWebSocketService<NeosVrCacheService>(WsServiceName);
                _wsServer.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void CreateFw()
        {
            if (_fw != null)
            {
                _fw.Changed -= FwOnChanged;
                _fw.Dispose();
            }

            _fw = new FileSystemWatcher(Config.CachePath)
                {InternalBufferSize = 64000, EnableRaisingEvents = true, IncludeSubdirectories = true};
            _fw.Changed += FwOnChanged;
            var oldestFile = new DirectoryInfo(Config.CachePath).GetFiles().OrderBy(e => e.LastAccessTimeUtc).First()
                .LastAccessTimeUtc;
            Console.WriteLine($"Oldest file is {(DateTime.UtcNow - oldestFile).TotalDays.ToString("0.00")} Days old.");
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
            var resultCacheSize = Task.Run(() =>
                dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length));
            Task.WaitAll();
            CacheInfo.FileCount = resultFcount.Result;
            CacheInfo.CacheSize = resultCacheSize.Result;
            label_currentSize.Text = "Size: " + BytesToString(CacheInfo.CacheSize);
            label_currentFileCount.Text = "Files: " + CacheInfo.FileCount.ToString("N0", new CultureInfo("EN"));
            label_fileAccess.Text =
                "Accessed Files: " + CacheInfo.FileAccessCount.ToString("N0", new CultureInfo("EN"));
        }

        private void buttonCleanUp_Click(object sender, EventArgs e)
        {
            button_cleanup.Enabled = false;
            Console.WriteLine("Started Cleanup...");

            Task.Run(RunCleanUp);
        }

        private void RunCleanUp()
        {
            var sw = Stopwatch.StartNew();
            var deleteSize = 0L;
            var deleteFcount = 0;
            try
            {
                var maxSize = Config.CacheSizeLimit * 1024L * 1024L * 1024L;
                var files = new HashSet<FileSystemInfo>();

                if (CacheInfo.CacheSize > maxSize)
                {
                    foreach (var file in new DirectoryInfo(Config.CachePath).GetFiles()
                        .OrderBy(e => e.LastAccessTimeUtc))
                        if ((DateTime.UtcNow - file.LastAccessTimeUtc).TotalDays >= Config.CacheTimeLimit &&
                            CacheInfo.CacheSize - deleteSize > maxSize)
                        {
                            deleteSize += file.Length;
                            files.Add(file);
                        }
                        else
                        {
                            break;
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
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                sw.Stop();
                Invoke((MethodInvoker)
                    (
                        () =>
                        {
                            button_cleanup.Enabled = true;
                            Console.WriteLine($"Cleanup done in {sw.Elapsed.ToString()}");
                            Console.WriteLine($"Deleted {BytesToString(deleteSize)} {deleteFcount} Files");
                        }
                    )
                );
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
            _wsServer.WebSocketServices[WsServiceName].Sessions.Broadcast(
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