using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows;



namespace AI_riddle
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
        }
     

        protected override async void OnAppearing()
        {
            
            base.OnAppearing();
          
            SongList.ItemsSource = await TableStorageService.GetRiddles();
        }
        private async void SelectSong(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem != null)
                {
                    await Navigation.PushAsync(new Page2(e.SelectedItem as QA));
                    SongList.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {

            }
        }
        

        
    }
}