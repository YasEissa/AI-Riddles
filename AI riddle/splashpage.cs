using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;
using System.Linq;
using Xamarin.Forms;
namespace AI_riddle
{
    public class splashpage : ContentPage
    {
        Image splashimage;
        public splashpage()
            {
            NavigationPage.SetHasNavigationBar(this, false);
            var sub = new AbsoluteLayout();
            splashimage = new Image
            {
                Source = "netways.jpg",
                WidthRequest = 100,
                HeightRequest = 100

            };
            AbsoluteLayout.SetLayoutFlags(splashimage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashimage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            sub.Children.Add(splashimage);
            this.BackgroundColor = Color.FromHex("ffffff");
            this.Content = sub;

            }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await splashimage.ScaleTo(1, 2000);
            await splashimage.ScaleTo(0.9, 1500);
            await splashimage.ScaleTo(150, 1200);
            Application.Current.MainPage = new NavigationPage(new MainPage());

        }
       

        
    }
}
