using MahApps.Metro.Controls;
using OMCL.Configuration;
using OMCL.Core.Minecraft;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OMCL
{

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
       
        #region BaseSettings
        public Logger render = new Logger("MainWindow Render Thread");
        public Logger task = new Logger("MainWindow Task Thread");

        public event PropertyChangedEventHandler PropertyChanged;

        public static class SettingKeys
        {
            public static readonly string WIDTH = "omcl\\mainwindow\\properties\\width";
            public static readonly string HEIGHT = "omcl\\mainwindow\\properties\\height";
            public static readonly string TITLE = "omcl\\mainwindow\\properties\\title";
        }
        public MainWindow()
        {
            this.DataContext = this;
            render.info("MainWindow Instance Created");
            RegisterAllSettings();
            RegisterAllListeners();
            InitializeComponent();
            render.info("MainWindow Initialized");
            if (!Properties.Settings.Default.DEBUG)
            {
                PreInit();
                InitAllWindowSettings();
            }
            var paths =Core.Minecraft.MinecraftManager.GetMinecraftDirectories()[0].Versions;
        }
        #endregion
        #region MVVMProperties
        public static readonly string[] MVVMProperties = new string[]
        {
            "SelectedMinecraft"
        };
        public void RefreshAllMVVMProperties()
        {
            foreach(var prop in MVVMProperties)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }
        public MinecraftInstance SelectedMinecraft => MinecraftManager.SelectedDirectory.Versions[0];
        #endregion
        #region BaseMethods
        public void PreInit()
        {
            if (Properties.Settings.Default.DEBUG)
            {
                render.info("Debug Mode MessageBox");
                if (Config.AppConfig.GetValue<bool>("\\&6554*insider\\edgw__\\tf4fre\\feefe444\\nonmsgbx", false) == false)
                {
                    if (MessageBox.Show("你是用的是内测 " + Properties.Settings.Default.DEBUGNUMBER + " 版本的OMCL，感谢您参与内测，请不要泄露，传播内测程序。\n下次是否还要显示此窗口?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        Config.AppConfig.SetValue("\\&6554*insider\\edgw__\\tf4fre\\feefe444\\nonmsgbx", true, false);
                    }
                }
            }
        }
        public void InitAllWindowSettings()
        {
            render.info("Init All WindowSettings");
            Width = Config.AppConfig.GetValue<float>(SettingKeys.WIDTH);
            Height = Config.AppConfig.GetValue<float>(SettingKeys.HEIGHT);
            Title = Config.AppConfig.GetValue<string>(SettingKeys.TITLE);
            if (Properties.Settings.Default.DEBUG)
            {
                Title = "OMCL INSIDE Version " + Properties.Settings.Default.DEBUGNUMBER + " - Signed by Unknown Teams Lab, EDGW_";
            }
        }
        public void RegisterAllListeners()
        {
            render.info("Register All Listenders");
            Config.AppConfig.RegisterListener(SettingKeys.WIDTH, (path, value) =>
            {
                this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    Width = Config.AppConfig.GetValue<float>(path);
                });
            });
            Config.AppConfig.RegisterListener(SettingKeys.HEIGHT, (path, value) =>
            {
                this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    Height = Config.AppConfig.GetValue<float>(path);
                });
            });
            Config.AppConfig.RegisterListener(SettingKeys.TITLE, (path, value) =>
            {
                this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    Title = Config.AppConfig.GetValue<string>(path);
                    if (Properties.Settings.Default.DEBUG)
                    {
                        Title = "OMCL INSIDE Version " + Properties.Settings.Default.DEBUGNUMBER + " - Signed by Unknown Teams Lab, EDGW_";
                    }
                });
            });
        }
        public void RegisterAllSettings()
        {
            render.info("Register All Settings' Default Value");
            Config.AppConfig.RegisterDefaultValue(SettingKeys.WIDTH, 800);
            Config.AppConfig.RegisterDefaultValue(SettingKeys.HEIGHT, 500);
            Config.AppConfig.RegisterDefaultValue(SettingKeys.TITLE, "Original Minecraft Launcher");
        }

        public void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Config.AppConfig.SetValue(SettingKeys.WIDTH, Width, false);
            Config.AppConfig.SetValue(SettingKeys.HEIGHT, Height, false);
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.DEBUG)
            {
                Task.Run(() =>
                {
                    task.info("Window Loaded");
                    task.info("Check Debug Mode");
                    Thread.Sleep(100);
                    this.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        PreInit();
                        InitAllWindowSettings();
                    });
                });
            }
        }
        private void WindowActivated(object sender, EventArgs e)
        {
            RefreshAllMVVMProperties();
        }
        #endregion
    }
}
