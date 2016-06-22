using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ColorTracker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorPage : Page
    {
        public ColorPage()
        {
            this.InitializeComponent();
        }
        byte[] ar = new byte[4];
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
            ar = e.Parameter as byte[];
            colorGrid.Background = new SolidColorBrush(Color.FromArgb(ar[0],ar[1],ar[2],ar[3]));
            CommandBar1.Visibility = Visibility.Visible;
            InitializeComponent();
        }

        private void AppBarToggleButton_Click(object sender, RoutedEventArgs e)
        {
            //Reject Button
            Frame.Navigate(typeof(MainPage));
        }

        private async void AppBarToggleButton_Click_1(object sender, RoutedEventArgs e)
        {
            //Accept button
            Argb ob = new Argb();
            ob.A = ar[0];
            ob.R = ar[1];
            ob.G = ar[2];
            ob.B = ar[3];
            await fileWrite(ob);
            Frame.Navigate(typeof(MainPage));
        }

        private async Task fileWrite(Argb p)
        {
            List<string> urlList = new List<string>();
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(p);
                urlList.Add(contentsToWriteToFile);             
                StorageFile sampleFile = await localFolder.GetFileAsync("dataFile.txt");
                await FileIO.AppendLinesAsync(sampleFile, urlList);
            }
            catch (FileNotFoundException)
            {
                try
                {
                    StorageFile sampleFile = await localFolder.CreateFileAsync("dataFile.txt",
                    CreationCollisionOption.ReplaceExisting);
                    await FileIO.AppendLinesAsync(sampleFile, urlList);
                    
                }
                catch (Exception)
                {

                }
            }
        }
    }

}
