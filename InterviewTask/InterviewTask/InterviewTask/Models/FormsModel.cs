using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InterviewTask.Models
{
    public class FormsModel : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string ControlType { get; set; }
        public string LabelEnglish { get; set; }
        public string LabelArabic { get; set; }
        public bool IsCompulsory { get; set; }
        public bool IsHeader { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

