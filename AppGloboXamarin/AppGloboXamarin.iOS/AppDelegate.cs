using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.Threading.Tasks;
using Xamarin;
using System.IO;
using XLabs.Forms;
using XLabs.Platform.Device;
using XLabs.Ioc;
using XLabs.Platform.Services.Media;
using XLabs.Platform.Mvvm;
using XLabs.Forms.Services;
using XLabs.Serialization;
using XLabs.Platform.Services;
using XLabs.Platform.Services.Email;
using Xamarin.Forms;

namespace AppGloboXamarin.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            App.ScreenHeight = (int)UIScreen.MainScreen.Bounds.Size.Height;
            App.ScreenWidth = (int)UIScreen.MainScreen.Bounds.Size.Width;

            // Initialize Insights
            Xamarin.Insights.Initialize("e3423df86c1db48e61fb5b6e33e729a45ff51a05");
            Xamarin.Insights.ForceDataTransmission = true;

            //Catch the inizialize crash
            Insights.HasPendingCrashReport += (sender, isStartupCrash) =>
            {
                if (isStartupCrash)
                {
                    Insights.PurgePendingCrashReports().Wait();
                }
            };

            #region UnHandleErrors
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
            #endregion

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);


        }

        // Blocco l'orientamento su portrait se è uno smartphone
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, UIWindow forWindow)
        {
            if(Device.Idiom == TargetIdiom.Phone)
            {
                return UIInterfaceOrientationMask.Portrait;
            }
            else
            {
                return UIInterfaceOrientationMask.All;
            }           
        }

        #region UnhandledErrors
        private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", e.Exception);
            LogUnhandledException(newExc);
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var newExc = new Exception("CurrentDomainOnUnhandledException", e.ExceptionObject as Exception);
            LogUnhandledException(newExc);
        }

        internal static void LogUnhandledException(Exception newExc)
        {
            try
            {
                const string errorFileName = "Fatal.log";
                var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources); // iOS: Environment.SpecialFolder.Resources
                var errorFilePath = Path.Combine(libraryPath, errorFileName);
                var errorMessage = String.Format("Time: {0}\r\nError: Unhandled Exception\r\n{1}",
                DateTime.Now, newExc.ToString());
                File.WriteAllText(errorFilePath, errorMessage);

                Console.WriteLine("Crash Report  " + errorMessage);

                Insights.Report(newExc);
            }
            catch
            {
                // just suppress any error logging exceptions
            }
        }
        #endregion
    }
}
