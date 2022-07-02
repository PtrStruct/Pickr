using Pickr.Core;
using Pickr.Helpers;
using Pickr.MVVM.Model;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pickr.MVVM.ViewModel
{
    internal class ColorPickerViewModel : ObservableObject
    {

        /* Commands */

        public RelayCommand GetCurrentPosCommand { get; set; }
        public RelayCommand StopCurrentColorPosCommand { get; set; }
        public RelayCommand CopyHexCommand { get; set; }


        private ColorModel _color;
        public ColorModel ColorModel
        {
            get { return _color; }
            set { _color = value; }
        }


        private BitmapImage _previewBitmap;
        public BitmapImage PreviewBitmap
        {
            get { return _previewBitmap; }
            set
            {
                _previewBitmap = value;
                OnPropertyChanged();
            }
        }


        private bool _isCapturing;
        PixelGetter _pixelGetter = new PixelGetter();

        public ColorPickerViewModel()
        {
            /* Instantiate a new instance of the object I'm binding to, so that it's not null */
            /* We can't access any properties on a null object. */
            ColorModel = new ColorModel
            {
                Red = 255,
                Green = 255,
                Blue = 255
            };

            GetCurrentPosCommand = new RelayCommand(o =>
            {
                _isCapturing = true;
                Task.Factory.StartNew(async () =>
                {
                    while (_isCapturing)
                    {
                        var capture = _pixelGetter.GetCursorPosAndColor();

                        using var bitmap = new Bitmap(20, 20);
                        using (var g = Graphics.FromImage(bitmap))
                            g.CopyFromScreen(capture.X - 10, capture.Y - 10, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            ColorModel.Red = capture.Color.R;
                            ColorModel.Green = capture.Color.G;
                            ColorModel.Blue = capture.Color.B;
                            PreviewBitmap = Convert(bitmap);
                        });

                        await Task.Delay(16);
                    }

                });

            }, o => true);

            StopCurrentColorPosCommand = new RelayCommand(o =>
            {
                _isCapturing = false;
                Clipboard.SetText(ColorModel.ColorHex);
            }, o => true);
        }

        public BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}