using System;


using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using System.Threading;
using Microsoft.Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using SQLite;

namespace AI_riddle
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {
        QA qa;
        ObservableCollection<MediaFile> files = new ObservableCollection<MediaFile>();
        public Page2( QA qa)
        {
            InitializeComponent();

            this.BindingContext = qa;
            this.qa = qa;

            if (string.IsNullOrWhiteSpace(qa.PartitionKey))
            {
                ToolbarItems.RemoveAt(1);
                qa.PartitionKey = Guid.NewGuid().ToString();
            }

        }
     
        private MediaFile _mediaFile;
         
        private async void CaptureClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":(No Camera available.)", "OK");
                return;
            }
            else
            {
                _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Name = "myImage.jpg",
                    Directory = "Sample",                                     
                });
                if (_mediaFile == null) return;
                imgCam.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
                var mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
            }
        }

       private async void UploadImage(Stream stream)
        {
            var _blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=riddles;AccountKey=j4ztTtQ39gt8AgA5H+TQ6OTFJCqMxhvYB1LQQYBsSaEIUBTA+9CUCy4orQYTJ6ih1AGUTzEdY+0u4zcWTBNn8A==;EndpointSuffix=core.windows.net");
            var containerClient = _blobServiceClient.GetBlobContainerClient("images");
            var blob = containerClient.GetBlobClient("test.png");
            var blobHttpHeader = new BlobHttpHeaders();
            blobHttpHeader.ContentType = "image/png";
            var uploadedBlob = await blob.UploadAsync(stream, blobHttpHeader);            
        }     
        
        public async void SubmitClicked(Object Sender, EventArgs e)
        {
            
            
            
            if (_mediaFile == null)
            {
                await DisplayAlert("Error", "Please use your camera to take a picture.", "OK");
                return;
            }
            else
            {
                UploadImage(_mediaFile.GetStream());
            }

            string answer = qa.Answer;
            string p3= "https://riddles.blob.core.windows.net/images/test.png";
            const string subscriptionKey = "c28f39879d3f47a98207f9e311067dbe";
            const string endpoint = "https://riddleapp.cognitiveservices.azure.com/";
            ComputerVisionClient client = image_recog.Authenticate(endpoint, subscriptionKey);
            ImageAnalysis results = await image_recog.AnalyzeImageUrl(client, p3);
            var last = results.Tags.Last();
            foreach (var caption in results.Tags )
            {
                await DisplayAlert("Found:", caption.Name, "Next");
                if (caption.Equals(last))
                {
                    await DisplayAlert("Sorry! Incorrect Answer", "Press Repeat to repeat the question", "Repeat" , "cancel");
                 
                }
                else if(caption.Name == answer &&caption.Confidence>0.75)
                {
                    
                    await DisplayAlert("Congratulations! Correct Answer", "Press Next to go to the next question", "Next");
                    await Navigation.PopAsync();
                }
            }
           

        }


    }
}