using Broadsign_DOMS.Service;
using System;

namespace Broadsign_DOMS.Model
{
    public class FileModel : ObservableObject
    {
        private Action<FileModel> _onPropertyChanged;
        private bool _isChecked;
        public string FileName {get;set;}
        public string FilePath { get; set; }
        public bool IsChecked 
        { 
            get => _isChecked;
            set
            {
                _isChecked = value;
                _onPropertyChanged?.Invoke(this);
            }
        }

        public FileModel(Action<FileModel> onPropertyChanged)
        {
            _onPropertyChanged = onPropertyChanged;
        }
    }
}
