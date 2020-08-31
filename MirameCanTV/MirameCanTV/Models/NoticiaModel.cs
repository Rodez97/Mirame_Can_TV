using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MirameCanTV.Models
{
    public class NoticiaModel : INotifyPropertyChanged
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
        string _Subtitulo;
        public string Subtitulo
        {
            set
            {
                if (_Subtitulo != value)
                {
                    _Subtitulo = value;
                    OnPropertyChanged("Subtitulo");
                }
            }
            get
            {
                return _Subtitulo;
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
        string _Contenido;
        public string Contenido
        {
            set
            {
                if (_Contenido != value)
                {
                    _Contenido = value;
                    OnPropertyChanged("Contenido");
                }
            }
            get
            {
                return _Contenido;
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
        string _ImageUrl;
        public string ImageUrl
        {
            set
            {
                if (_ImageUrl != value)
                {
                    _ImageUrl = value;
                    OnPropertyChanged("ImageUrl");
                }
            }
            get
            {
                return _ImageUrl;
            }
        }
        string _Author;
        public string Author
        {
            set
            {
                if (_Author != value)
                {
                    _Author = value;
                    OnPropertyChanged("Author");
                }
            }
            get
            {
                return _Author;
            }
        }
        string _VideoID;
        public string VideoID
        {
            set
            {
                if (_VideoID != value)
                {
                    _VideoID = value;
                    OnPropertyChanged("VideoID");
                }
            }
            get
            {
                return _VideoID;
            }
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Title;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
