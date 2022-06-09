using System;
using System.Windows.Media;

namespace Pickr.MVVM.Model
{
    class ColorModel : ObservableObject
    {
        private byte _red;

        public byte Red
        {
            get { return _red; }
            set
            {
                _red = value;
                ColorStop = new SolidColorBrush(Color.FromRgb((byte)_red, (byte)_green, (byte)_blue));
                ColorHex = $"#{Red.ToString("X2") + Green.ToString("X2") + Blue.ToString("X2")}";
                OnPropertyChanged();
            }
        }

        private byte _green;
        public byte Green
        {
            get { return _green; }
            set
            {
                _green = value;
                ColorStop = new SolidColorBrush(Color.FromRgb((byte)_red, (byte)_green, (byte)_blue));
                ColorHex = $"#{Red.ToString("X2") + Green.ToString("X2") + Blue.ToString("X2")}";
                OnPropertyChanged();
            }
        }


        private byte _blue;
        public byte Blue
        {
            get { return _blue; }
            set
            {
                _blue = value;
                ColorStop = new SolidColorBrush(Color.FromRgb((byte)_red, (byte)_green, (byte)_blue));
                ColorHex = $"#{Red.ToString("X2") + Green.ToString("X2") + Blue.ToString("X2")}";
                OnPropertyChanged();
            }
        }


        private Brush _colorStop;

        public Brush ColorStop
        {
            get { return _colorStop; }
            set
            {
                _colorStop = value;
                OnPropertyChanged();
            }
        }

        private string _colorHex;

        public string ColorHex
        {
            get { return _colorHex; }
            set
            {
                _colorHex = value;
                OnPropertyChanged();
            }
        }


    }
}
