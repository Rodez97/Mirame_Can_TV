using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MirameCanTV.Models
{
    public class ItemModel : INotifyPropertyChanged
    {
        #region Información General
        string _Title;
        public string Title
        {
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    OnPropertyChanged("Title");
                }
            }
            get
            {
                return _Title;
            }
        }
        string _UploadDate;
        public string UploadDate
        {
            set
            {
                if (_UploadDate != value)
                {
                    _UploadDate = value;
                    OnPropertyChanged("UploadDate");
                }
            }
            get
            {
                return _UploadDate;
            }
        }
        string _Id;
        public string Id
        {
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    OnPropertyChanged("Id");
                }
            }
            get
            {
                return _Id;
            }
        }
        string _Description;
        public string Description
        {
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged("Description");
                }
            }
            get
            {
                return _Description;
            }
        }
        string _Url;
        public string Url
        {
            set
            {
                if (_Url != value)
                {
                    _Url = value;
                    OnPropertyChanged("Url");
                }
            }
            get
            {
                return _Url;
            }
        }
        string _ImageUrlHD;
        public string ImageUrlHD
        {
            set
            {
                if (_ImageUrlHD != value)
                {
                    _ImageUrlHD = value;
                    OnPropertyChanged("ImageUrlHD");
                }
            }
            get
            {
                return _ImageUrlHD;
            }
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Id;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
