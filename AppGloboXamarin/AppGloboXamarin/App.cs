using AppGloboXamarin.Helpers;
using AppGloboXamarin.Pages.Commons;

using Xamarin.Forms;

namespace AppGloboXamarin
{
    public class App : Application
    {
        public static int ScreenHeight;
        public static int ScreenWidth;

        public App()
        {
 
           

           
                MainPage = new Root();
            
        }

        protected override void OnStart()
        {
           
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void ShowMainPage()
        {
            MainPage = new Root();
        }  
            
    }
}
