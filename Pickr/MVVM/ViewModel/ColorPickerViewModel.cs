using Pickr.Core;
using Pickr.Helpers;
using Pickr.MVVM.Model;
using System.Threading.Tasks;
using System.Windows;

namespace Pickr.MVVM.ViewModel
{
    internal class ColorPickerViewModel
    {

        /* Commands */

        public RelayCommand GetCurrentPosCommand { get; set; }
        public RelayCommand StopCurrentColorPosCommand { get; set; }


        private ColorModel _color;
        public ColorModel ColorModel
        {
            get { return _color; }
            set { _color = value; }
        }

        private bool _isCapturing;

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
                        var pg = new PixelGetter();
                        var color = pg.GetCursorPosAndColor();

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            ColorModel.Red = color.R;
                            ColorModel.Green = color.G;
                            ColorModel.Blue = color.B;

                        });
                        await Task.Delay(16);

                    }
                });

            }, o => true);

            StopCurrentColorPosCommand = new RelayCommand(o => { _isCapturing = false; }, o => true);
        }
    }
}