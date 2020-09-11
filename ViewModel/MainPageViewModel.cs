﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClock.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private int clockFontSize;

        private bool isTwelveHourFormat;

        public event PropertyChangedEventHandler PropertyChanged;

        public int ClockFontSize
        {
            get { return this.clockFontSize; }
            set
            {
                this.clockFontSize = value;
                this.UpdateClockSizeSettings(value);
                this.OnPropertyChanged();
            }
        }

        public double ClockFontSizeDouble
        {
            get { return this.ClockFontSize; }
            set
            {
                this.ClockFontSize = (int)value;
            }
        }

        public bool IsTwelveHourFormat
        {
            get { return this.isTwelveHourFormat; }
            set
            {
                this.isTwelveHourFormat = value;
                this.UpdateClockFormatSettings(value);
                this.OnPropertyChanged();
            }
        }

        public MainPageViewModel(int clockSize, bool isTwelveHourFormat)
        {
            if (clockSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(clockSize), @"The value for clock size cannot be zero or less.");
            }
            this.ClockFontSize = clockSize;
            this.isTwelveHourFormat = isTwelveHourFormat;
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateClockSizeSettings(int value)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["clockSize"] = value;
            
        }

        public void UpdateClockFormatSettings(bool value)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["clockFormat"] = value;
        }
    }
}
