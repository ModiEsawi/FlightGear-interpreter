using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace proj12.Model.Helpers
{
    public class CurrentTime : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool Stop { set; get; }
        private string time;
        public string Time
        {
            set
            {
                this.time = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Time"));
            }
            get
            {
                return this.time;
            }
        }

        public CurrentTime()
        {
            Stop = false;
            new Thread(delegate () {
                while (!Stop)
                {
                    this.Time = DateTime.Now.ToString();
                    Thread.Sleep(1000);
                }
            }).Start();

        }

    }
}
