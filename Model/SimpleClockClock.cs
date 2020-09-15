using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

/// <summary>
/// A simple clock class that keeps track of elapsed time in order to properly keep information updated for the system.
/// </summary>
/// <remarks>
/// Copyright @Burusutazu 2020
/// </remarks>
namespace SimpleClock.Model
{
    public class SimpleClockClock : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string timeString;

        private DispatcherTimer timer;

        private readonly string twelveHourFormat = "hh:mm tt";

        private readonly string twentyFourHourFormat = "HH:mm";

        private bool isTwelveHourFormat;

        public string TimeString
        {
            get { return this.timeString; }
            set
            {
                this.timeString = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsTwelveHourFormat
        {
            get { return this.isTwelveHourFormat; }
            set
            {
                this.isTwelveHourFormat = value;
                this.UpdateTimeString();
            }
        }

        public SimpleClockClock(bool isTwelveHourFormat)
        {
            this.IsTwelveHourFormat = isTwelveHourFormat;
            this.InitializeClock();
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InitializeClock()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += UpdateTimeStringEvent;
            this.timer.Interval = new TimeSpan(0, 0, 1);
        }

        public void UpdateTimeStringEvent(object sender, object e)
        {
            this.UpdateTimeString();
        }

        private void UpdateTimeString()
        {
            if (this.IsTwelveHourFormat)
            {
                this.TimeString = DateTime.Now.ToString(this.twelveHourFormat);
            }
            else
            {
                this.TimeString = DateTime.Now.ToString(this.twentyFourHourFormat);
            }
        }

        public void StartClock()
        {
            this.timer.Start();
        }

        public void StopClock()
        {
            this.timer.Stop();
        }
    }
}
