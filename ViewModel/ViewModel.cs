using proj12.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
 

namespace proj12.ViewModel
{

    class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ISimulatorModel model;

        public ViewModel(ISimulatorModel model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public string VM_Time
        {
            get { return model.Time; }
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
