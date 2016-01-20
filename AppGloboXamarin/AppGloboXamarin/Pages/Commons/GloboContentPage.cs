using AppGloboXamarin.Helpers;
using AppGloboXamarin.Models;
using AppGloboXamarin.Pages.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppGloboXamarin.Pages.Commons
{
    public class GloboContentPage: ContentPage, INotifyPropertyChanged
    {
        public Xamarin.Forms.MasterDetailPage md;

        public ToolbarButton btnMenu;
        public ToolbarButton btnBack;
        public Button btnAction;
        public ToolbarButton btnSave;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool isLoading;
        private RelativeLayout relativeLayout;
        private float pTestata;
        private AbsoluteLayout abs;
        public int? fatherId;
        public Label lblTitle;
        public int hLblTitle = 40;
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }

            set
            {
                this.isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        private double pageHeigth = 0;
        public Image testataImage;

        public double PageHeight
        {
            get
            {
                return pageHeigth;
            }

            set
            {
                if (pageHeigth > 0)
                    return;

                pageHeigth = value;
            }
        }

        public GloboContentPage(int? id)
        {
            this.fatherId = id;
        }

        public GloboContentPage()
        {

        }

        public virtual void Init()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        

            float wTestata = 375;
            float hTestata = 64;

            float statusBarHeight = 20;
            float wButton = 40;

            pTestata = (App.ScreenWidth * hTestata) / wTestata;

            

            BackgroundColor = Color.White;
            Color btnBackColor = Color.Transparent;

            //Testata per navigationbar
            BoxView backAlpha = new BoxView
            {
                BackgroundColor = Color.Yellow
            };

            testataImage = new Image();
            testataImage.Source = UtilityResources.testata_logo_list;
            testataImage.BackgroundColor = Color.Transparent;

            btnMenu = new ToolbarButton(UtilityResources.ico_menu);
            btnMenu.btn.Clicked += BtnMenu_Clicked;
            btnMenu.btn.BackgroundColor = btnBackColor;

            btnBack = new ToolbarButton(UtilityResources.ico_back);
            btnBack.btn.Clicked += BtnBack_Clicked;
            btnBack.BackgroundColor = btnBackColor;
            btnBack.IsVisible = false;

            btnAction = new Button();
            btnAction.TextColor = Color.Black;
            btnAction.Clicked += BtnAction_Clicked;
            btnAction.BackgroundColor = btnBackColor;
            btnAction.IsVisible = false;

            btnSave = new ToolbarButton(UtilityResources.icon_Test);
            btnSave.btn.Clicked += BtnSave_Clicked;
            btnSave.BackgroundColor = btnBackColor;
            btnSave.IsVisible = false;

            lblTitle = new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 25,

                LineBreakMode = LineBreakMode.TailTruncation
            };

            relativeLayout = new RelativeLayout();

            pTestata = (App.ScreenWidth * hTestata) / wTestata;
            relativeLayout.Children.Add(testataImage,
                 Constraint.RelativeToParent((parent) => {
                     return 0;
                 }),
                 Constraint.RelativeToParent((parent) => {
                     return 0;
                 }),
                 Constraint.RelativeToParent((parent) => {
                     return parent.Width;
                 }),
                 Constraint.RelativeToParent((parent) => {
                     return pTestata;
                 })
             );

            relativeLayout.Children.Add(btnMenu,
                Constraint.RelativeToParent((parent) => {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) => {
                    return statusBarHeight;
                }),
                Constraint.RelativeToParent((parent) => {
                    return wButton;
                }),
                Constraint.RelativeToParent((parent) => {
                    return wButton;
                })
            );

            relativeLayout.Children.Add(btnBack,
                Constraint.RelativeToParent((parent) => {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) => {
                    return statusBarHeight;
                }),
                Constraint.RelativeToParent((parent) => {
                    return wButton;
                }),
                Constraint.RelativeToParent((parent) => {
                    return wButton;
                })
            );

            relativeLayout.Children.Add(btnAction,
                Constraint.RelativeToParent((parent) => {
                    return parent.Width - wButton * 2;
                }),
                Constraint.RelativeToParent((parent) => {
                    return statusBarHeight;
                }),
                Constraint.RelativeToParent((parent) => {
                    return wButton * 2;
                }),
                Constraint.RelativeToParent((parent) => {
                    return wButton;
                })
            );

            relativeLayout.Children.Add(btnSave,
                Constraint.RelativeToParent((parent) => {
                    return parent.Width - wButton * 2;
                }),
                Constraint.RelativeToParent((parent) => {
                    return statusBarHeight;
                }),
                Constraint.RelativeToParent((parent) => {
                    return wButton * 2;
                }),
                Constraint.RelativeToParent((parent) => {
                    return wButton;
                })
            );

           
            relativeLayout.Children.Add(lblTitle,
                Constraint.RelativeToParent((parent) => {
                    return 10;
                }),
                Constraint.RelativeToParent((parent) => {
                    return pTestata;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Width - 20;
                }),
                Constraint.RelativeToParent((parent) => {
                    return hLblTitle;
                })
            );

            abs = new AbsoluteLayout();

            AbsoluteLayout.SetLayoutFlags(relativeLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(relativeLayout, new Rectangle(0, 0, 1, 1));
            abs.Children.Add(relativeLayout);

            //****ACTIVITY INDICATOR*******//
            #region activityIndicator
            var backIndicator = new ContentView();
            backIndicator.BackgroundColor = Color.Black;
            backIndicator.IsVisible = IsLoading;
            backIndicator.Opacity = 0.5;
            backIndicator.BindingContext = this;
            backIndicator.SetBinding(ContentView.IsVisibleProperty, "IsLoading");
            AbsoluteLayout.SetLayoutFlags(backIndicator, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(backIndicator, new Rectangle(0, 0, 1, 1));
            abs.Children.Add(backIndicator);

            var actIndicator = new ActivityIndicator();
            actIndicator.IsVisible = IsLoading;
            actIndicator.IsRunning = IsLoading;
            actIndicator.BindingContext = this;
            actIndicator.BackgroundColor = Color.Transparent;
            actIndicator.Color = Color.White;
            actIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsLoading");
            actIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsLoading");
            AbsoluteLayout.SetLayoutFlags(actIndicator, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(actIndicator, new Rectangle(0.5, 0.5, -1, -1));
            abs.Children.Add(actIndicator);
            #endregion

            this.Content = abs;
        }



        public virtual void BtnSave_Clicked(object sender, EventArgs e)
        {
        }

        public virtual void BtnAction_Clicked(object sender, EventArgs e)
        {
        }

        public virtual void BtnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void BtnMenu_Clicked(object sender, EventArgs e)
        {
            md.IsPresented = true;
        }

        //Add children layout to the master layout
        public void AddLayoutToContent(Xamarin.Forms.Layout l)
        {

            relativeLayout.Children.Add(l,
                Constraint.RelativeToParent((parent) => {
                    return 0;
                }),
                Constraint.RelativeToParent((parent) => {
                    return pTestata + hLblTitle;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Width;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Height - pTestata - hLblTitle;
                })
            );
        }

        public void IsRoot(bool root)
        {
            this.btnMenu.IsVisible = root;
            this.btnBack.IsVisible = !root;
        }

        public void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
