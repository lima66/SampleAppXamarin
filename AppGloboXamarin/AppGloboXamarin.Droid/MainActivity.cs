using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.IO;
using XLabs.Forms;
using XLabs.Ioc;
using XLabs.Platform.Mvvm;
using XLabs.Platform.Device;
using SQLite.Net;
using XLabs.Platform.Services.Media;
using XLabs.Platform.Services;

namespace AppGloboXamarin.Droid
{
    [Activity(Label = "AppGloboXamarin", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize, Theme = "@android:style/Theme.Holo.Light")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);        


            global::Xamarin.Forms.Forms.Init(this, bundle);          

            #region Catch Crash Start
            //Catch inizialize crash
            Insights.HasPendingCrashReport += (sender, isStartupCrash) =>
            {
                if (isStartupCrash)
                {
                    Insights.PurgePendingCrashReports().Wait();
                }
            };
            #endregion

            Forms.SetTitleBarVisibility(AndroidTitleBarVisibility.Never);

            // Initialize Insights
            Xamarin.Insights.Initialize("e3423df86c1db48e61fb5b6e33e729a45ff51a05", this);
            Xamarin.Insights.ForceDataTransmission = true;

            //Save width and heigth and remove forms title bar
            App.ScreenHeight = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
            App.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);

            LockOrientation();            

            #region UnHandleErrors
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            #endregion

            LoadApplication(new App());
        }

        // Blocco l'orientamento su portrait se è uno smartphone
        private void LockOrientation()
        {
            var activity = (Activity)Forms.Context;
            if(Device.Idiom == TargetIdiom.Phone)
            {
                activity.RequestedOrientation = ScreenOrientation.Portrait;
            }
            else
            {
                activity.RequestedOrientation = ScreenOrientation.Unspecified;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #region catchAndLogUnhandleError
        private void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            var newExc = new Exception("AndroidEnvironmentUnhandledExceptionRaiser", e.Exception as Exception);
            LogUnhandledException(newExc);
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var newExc = new Exception("CurrentDomainOnUnhandledException", e.ExceptionObject as Exception);
            LogUnhandledException(newExc);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", e.Exception);
            LogUnhandledException(newExc);
        }


        internal static void LogUnhandledException(Exception newExc)
        {
            try
            {
                const string errorFileName = "Fatal.log";
                var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var errorFilePath = Path.Combine(libraryPath, errorFileName);
                var errorMessage = String.Format("Time: {0}\r\nError: Unhandled Exception\r\n{1}",
                DateTime.Now, newExc.ToString());
                File.WriteAllText(errorFilePath, errorMessage);

                // Log to Android Device Logging.
                Android.Util.Log.Error("Crash Report", errorMessage);

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

