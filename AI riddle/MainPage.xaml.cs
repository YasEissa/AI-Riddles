using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace AI_riddle
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
          
        }

        private void StartClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page1());
        }
    }
}
