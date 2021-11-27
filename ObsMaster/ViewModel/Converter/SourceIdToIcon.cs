using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace HuyaFX.UI.Converter
{
    public class SourceIdToIcon : IValueConverter
    {
        private static Lazy<SourceIdToIcon> _lazyInstance = new Lazy<SourceIdToIcon>(() => new SourceIdToIcon());
        public static SourceIdToIcon Instance => _lazyInstance.Value;

        private Dictionary<string, BitmapImage> mapIconSource = new Dictionary<string, BitmapImage>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string id = value.ToString();

            if (mapIconSource.ContainsKey(id))
            {
                return mapIconSource[id];
            }

            BitmapImage bitmap = null;
            switch(id)
            {
                case "image_source":
                    bitmap = new BitmapImage(new Uri("/Resources/plugins/picture.png", UriKind.Relative));
                    break;
                case "game_capture":
                    bitmap = new BitmapImage(new Uri("/Resources/plugins/game.png", UriKind.Relative));
                    break;
                case "window_capture":
                    bitmap = new BitmapImage(new Uri("/Resources/plugins/window.png", UriKind.Relative));
                    break;

                case "text_gdiplus":
                    bitmap = new BitmapImage(new Uri("/Resources/plugins/character.png", UriKind.Relative));
                    break;
                case "dshow_input":
                    bitmap = new BitmapImage(new Uri("/Resources/plugins/camera.png", UriKind.Relative));
                    break;
                case "monitor_capture":
                    bitmap = new BitmapImage(new Uri("/Resources/plugins/screen.png", UriKind.Relative));
                    break;                
            }

            mapIconSource.Add(id, bitmap);
            return bitmap;            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
