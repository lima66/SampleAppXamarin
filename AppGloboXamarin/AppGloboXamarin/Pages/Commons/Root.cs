using AppGloboXamarin.Pages.ClientPage;
using AppGloboXamarin.Pages.MenuApp;
using System;
using Xamarin.Forms;

namespace AppGloboXamarin.Pages.Commons
{
    public partial class Root: MasterDetailPage
    {
        private MenuPage menuPage;

        public Root()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            menuPage = new MenuPage();
            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem);
            menuPage.md = this;

            Master = menuPage;         

            GetNewHomePage();
            IsPresentedChanged += Root_IsPresentedChanged;
            Title = "Root";            
        }

        private void Root_IsPresentedChanged(object sender, EventArgs e)
        {
            if (IsPresented && (Device.Idiom == TargetIdiom.Phone))
            {
                //MenuPage.cs -> line 23
                MessagingCenter.Send<object, Guid>(this, "updateMenu", Guid.Empty);
            }
            else
            {
                //Non è visibile se smartphone
            }
        }

        //setta l'opacita quando il menu è aperto
        private void FnMDPresented(object sender, EventArgs e)
        {
            this.Detail.Opacity = IsPresented ? 0.5 : 1;
        }

        private void GetNewHomePage()
        {    
            NewClient h = new NewClient();
            h.md = this;
            Detail = new NavigationPage(h);         
        }

        private void NavigateTo(object menu)
        {
            
                return; 

            //Detail.Navigation.PushAsync(new EditClientDetails(menu));               

            //menuPage.Menu.SelectedItem = null;
            //if(MasterBehavior != MasterBehavior.Split)
            //{
            //    IsPresented = false;
            //}
          
        }

        protected override void OnAppearing()
        {
            //Se smartphone il menu funziona normalmente, se tablet il menu rimane visibile
            if (Device.Idiom == TargetIdiom.Phone)
            {
                IsPresentedChanged += FnMDPresented;
            }
            else
            {
                MasterBehavior = MasterBehavior.Split;
            }

            base.OnAppearing();
        }
    }
}
