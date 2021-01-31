using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace AI_riddle
{
    public partial class App : Application
    {
      
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new splashpage())
            {
                BarBackgroundColor = Color.FromHex("#0e011b"),
                BarTextColor = Color.White

            };
            
        }
       
        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
