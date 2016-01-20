using AppGloboXamarin.Helpers;
using AppGloboXamarin.Pages.Commons;
using AppGloboXamarin.Pages.Utilities;
using Plugin.Media;
using System;
using System.IO;
using Xamarin.Forms;

namespace AppGloboXamarin.Pages.ClientPage
{
    public class NewClient : GloboContentPage
    {
        private int gridColumnIndex = 0;
        private Button addProfiloImage;
        private Grid grid;
        private Button takePicture;

        public NewClient()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();

            btnMenu.IsVisible = false;
            btnBack.IsVisible = true;
            btnAction.IsVisible = true;
            btnAction.Text = "Save";

            lblTitle.IsVisible = true;
            lblTitle.Text = "Registration Client";
          

            //Inizializzazione componenti

            Color background = Color.Aqua;
  

            //Icon take picture
            takePicture = new Button
            {
                BackgroundColor = Color.Red,
                Text = "Take a Picture"
            };
            takePicture.Clicked += TakePicture_Clicked;

            // Icona del plus 
            addProfiloImage = new Button
            {
                BackgroundColor = Color.Blue,
                Text = "Pick a Picture"
            };

            addProfiloImage.Clicked += AddProfiloImage_Clicked;

            //Photo
            grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(120, GridUnitType.Absolute) },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) },
                }
            };
            grid.ColumnSpacing = 10;
            grid.BackgroundColor = background;
            grid.ChildAdded += Grid_ChildAdded;


          

            int labelHeight = 50;
            int lblHeight = 25;
            int componentShiftOnY = labelHeight + 35;

            #region Layout
            RelativeLayout form = new RelativeLayout();



            int hContainerImage = 120;
            form.Children.Add(grid,
                Constraint.RelativeToParent((parent) => {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) => {
                    return componentShiftOnY * 2;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) => {
                    return hContainerImage;
                })
            );

            form.Children.Add(addProfiloImage,
                Constraint.RelativeToParent((parent) => {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) => {
                    return grid.Y - lblHeight - 40;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Width / 2 - 10;
                }),
                Constraint.RelativeToParent((parent) => {
                    return lblHeight + 20;
                })
            );

            form.Children.Add(takePicture,
                Constraint.RelativeToParent((parent) => {
                    return parent.Width / 2 + 10;
                }),
                Constraint.RelativeToParent((parent) => {
                    return grid.Y - lblHeight - 40;
                }),
                Constraint.RelativeToParent((parent) => {
                    return addProfiloImage.Width;
                }),
                Constraint.RelativeToParent((parent) => {
                    return lblHeight + 20;
                })
            );          

            ScrollView scr = new ScrollView
            {
                IsClippedToBounds = true,
                Orientation = ScrollOrientation.Vertical,
                Content = form
            };
            #endregion

            AddLayoutToContent(scr);

        }

        private async void TakePicture_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }
            try
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "globo",
                    Name = "glb.jpg",
                });

                if (file == null)
                    return;

                var stream = file.GetStream();
                var tmp = StreamHelper.ReadToEnd(stream);
                file.Dispose();              

                var imgRez = DependencyService.Get<IResizeImage>().ResizeImage(tmp, 50, 50, 50);

                Image picture = new Image();
                picture.Source = ImageSource.FromStream(() => new MemoryStream(imgRez));
                picture.Aspect = Aspect.Fill;
                picture.VerticalOptions = LayoutOptions.Center;              
                   

                var tapGestures = new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1
                };
                tapGestures.Tapped += delegate
                {
           
                    // Go to PhotoGallery
                };

                picture.GestureRecognizers.Add(tapGestures);
            

                grid.Children.Add(picture, gridColumnIndex, 0);
            }
            catch (Exception ex)
            {
                Xamarin.Insights.Report(ex);
                await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured it in Xamarin Insights! Thanks.", "OK");
            }
        }

        private async void AddProfiloImage_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            try
            {
                Stream stream = null;
                var file = await CrossMedia.Current.PickPhotoAsync().ConfigureAwait(true);
                string path = file.Path;

                if (file == null)
                    return;

                stream = file.GetStream();
                var tmp = StreamHelper.ReadToEnd(stream);
                file.Dispose();

                var imgRez = DependencyService.Get<IResizeImage>().ResizeImage(tmp, 100, 150, 40);

                Image picture = new Image();
                picture.Source = ImageSource.FromStream(() => new MemoryStream(imgRez));
                picture.Aspect = Aspect.Fill;
                picture.VerticalOptions = LayoutOptions.Center;

               

                var tapGestures = new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1
                };
                tapGestures.Tapped += delegate
                {
                    //Go to PhotoGallery
                };

                picture.GestureRecognizers.Add(tapGestures);               

                grid.Children.Add(picture, gridColumnIndex, 0);

            }
            catch (Exception ex)
            {
                Xamarin.Insights.Report(ex);
                await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured it in Xamarin Insights! Thanks.", "OK");
            }
        }

        // Is possible add only 3 photo
        private void Grid_ChildAdded(object sender, ElementEventArgs e)
        {
            if (gridColumnIndex < 2)
            {
                gridColumnIndex++;
            }
            else
            {
                addProfiloImage.IsVisible = false;
                takePicture.IsVisible = false;
            }
        }

        //Salva i dati nella tabella client
        public override void BtnAction_Clicked(object sender, EventArgs e)
        {
            base.BtnAction_Clicked(sender, e);              
            
            DisplayAlert("Success", "Data saved successfully", "OK");                

        }
    }
}
