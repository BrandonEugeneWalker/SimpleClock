using SimpleClock.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleClock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPageViewModel ViewModel;
        private int clockSize;
        private bool isTwelveHourFormat;
        private string twelveHourFormat = "hh:mm tt";
        private string twentyFourHourFormat = "HH:mm";
        private DispatcherTimer Timer;

        public MainPage()
        {
            this.InitializeComponent();
            this.InitializeClockSettings();
            this.ViewModel = new MainPageViewModel(this.clockSize, this.isTwelveHourFormat);
            this.InitializeClock();
            
        }

        private void FullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode)
            {
                view.ExitFullScreenMode();
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
            }
            else
            {
                if (view.TryEnterFullScreenMode())
                {
                    ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
                }
            }
        }

        private void UpdateClock(object sender, object e)
        {
            if (this.ViewModel.IsTwelveHourFormat)
            {
                this.SimpleClockDisplay.Text = DateTime.Now.ToString(this.twelveHourFormat);
            }
            else
            {
                this.SimpleClockDisplay.Text = DateTime.Now.ToString(this.twentyFourHourFormat);
            }
        }

        private void InitializeClock()
        {
            this.Timer = new DispatcherTimer();
            Timer.Tick += UpdateClock;
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
        }

        private void InitializeClockSettings()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Object clockFontSettingsValue = localSettings.Values["clockSize"];
            Object clockFormatSettingsValue = localSettings.Values["clockFormat"];
            if (clockFontSettingsValue == null)
            {
                localSettings.Values["clockSize"] = 120;
            }
            else
            {
                this.clockSize = (int)clockFontSettingsValue;
            }
            if (clockFormatSettingsValue == null)
            {
                localSettings.Values["clockFormat"] = false;
            }
            else if (clockFormatSettingsValue is bool value)
            {
                this.isTwelveHourFormat = value;
            } 
            else
            {
                this.isTwelveHourFormat = false;
            }
            
        }
    }
}
