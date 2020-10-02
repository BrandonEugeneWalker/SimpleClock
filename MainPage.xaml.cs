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
    /// The main page of the application, contains all of the ui items needed for the program.
    /// </summary>
    /// <remarks>
    /// Copyright @Burusutazu 2020
    /// </remarks>
    public sealed partial class MainPage : Page
    {

        public MainPageViewModel ViewModel;
        private int clockSize;
        private bool isTwelveHourFormat;

        public MainPage()
        {
            this.InitializeComponent();
            this.clockSize = 120;
            this.InitializeClockSettings();
            this.ViewModel = new MainPageViewModel(this.clockSize, this.isTwelveHourFormat);
            
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

        private void InitializeClockSettings()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Object clockFontSettingsValue = localSettings.Values["clockSize"];
            Object clockFormatSettingsValue = localSettings.Values["clockFormat"];

            if (clockFontSettingsValue == null)
            {
                localSettings.Values["clockSize"] = 120;
            }
            else if ((int)clockFontSettingsValue <= 0)
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
