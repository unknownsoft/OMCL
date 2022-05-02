using OMCL.Configuration;
using OMCL.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Image = System.Windows.Controls.Image;

namespace OMCL.Web.News.UI
{
    /// <summary>
    /// NewsControl.xaml 的交互逻辑
    /// </summary>
    public partial class NewsControl : CardGrid , INotifyPropertyChanged
    {
        public Logger logger = new Logger("News Control");
        public NewsControl()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        public NewsEngine SelectedNewsEngine => selection.SelectedItem as NewsEngine;
        public List<New> News { get; set; }
        public List<NewsEngine> NewsEngines => NewsEngine.RegisteredEngines;

        public event PropertyChangedEventHandler PropertyChanged;

        private void selection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
        public void Refresh()
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedNewsEngine"));
            var engine = SelectedNewsEngine;
            Task.Run(() =>
            {
                try
                {
                    News = engine.News;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("News"));
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedNewsEngine"));
                }catch(Exception ex)
                {
                    logger.error("Can't Load Image");
                }
            });
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            (((dynamic)sender).DataContext).OnClick(sender, e);
        }

        private void Tile_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public static Bitmap[] Bitmaps = new Bitmap[3] { Properties.Resources.home_hero_1200x600, Properties.Resources.calcite_car, Properties.Resources.bg_wool_dark };
        int seed = 0;
        Object locker = new Object();
        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Image img = (Image)sender;
            lock (locker)
            {
                img.Visibility = Visibility.Visible;
                {
                    var lok = locker;
                    Bitmap bitmap = Bitmaps[++seed % 3];
                    {
                        IntPtr hBitmap = bitmap.GetHbitmap();
                        this.Dispatcher.Invoke((Action)delegate ()
                        {

                            ImageSource wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                hBitmap,
                                IntPtr.Zero,
                                Int32Rect.Empty,
                                BitmapSizeOptions.FromEmptyOptions());
                            img.Visibility = Visibility.Visible;
                            img.Source = wpfBitmap;
                        });

                        if (!DeleteObject(hBitmap))
                        {
                            throw new System.ComponentModel.Win32Exception();
                        }
                    }
                }
            }
            New @new = img.DataContext as New;
            Task.Run(() =>
            {
                try
                {
                    var bak = @new.Background;
                    if (bak == null)
                    {
                        this.Dispatcher.Invoke((Action)delegate ()
                        {

                        });
                    }
                    else
                    {
                        using (Bitmap bitmap = new Bitmap(@new.Background))
                        {
                            IntPtr hBitmap = bitmap.GetHbitmap();
                            this.Dispatcher.Invoke((Action)delegate ()
                            {

                                ImageSource wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                                    hBitmap,
                                    IntPtr.Zero,
                                    Int32Rect.Empty,
                                    BitmapSizeOptions.FromEmptyOptions());
                                img.Visibility = Visibility.Visible;
                                img.Source = wpfBitmap;
                            });

                            if (!DeleteObject(hBitmap))
                            {
                                throw new System.ComponentModel.Win32Exception();
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    logger.error("Can't Load Image");
                    logger.error(ex);
                }

            });
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

    }
}
