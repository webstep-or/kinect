using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace KinectHandTracking
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Set the current UI Culture to current culture - else it will default to en-US
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            // Register single point of exception handling
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += new EventHandler<UnobservedTaskExceptionEventArgs>(TaskScheduler_UnobservedTaskException);
        }

        // Single point of exception handling.
        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs eventArgs)
        {
            //if (eventArgs.Exception is ClientException)
            //{
            //    var clientException = eventArgs.Exception as ClientException;

            //    HandleClientException(clientException);
            //}
            //else
            //{
            //    // Create new client exception with exception as inner exception. Severity is set to critical to ensure application shutdown.
            //    var clientException = new ClientException(innerException: eventArgs.Exception)
            //    {
            //        Origin = ExceptionOrigin.Client,
            //        Severity = ExceptionSeverity.Critical
            //    };

            //    HandleClientException(clientException);
            //}

            eventArgs.Handled = true;
        }

        void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs eventArgs)
        {
            //ClientException clientException = null;

            //if (eventArgs.Exception.InnerException is ClientException)
            //{
            //    clientException = eventArgs.Exception.InnerException as ClientException;
            //}
            //else
            //{
            //    // Create new client exception with exception as inner exception. Severity is set to critical to ensure application shutdown.
            //    clientException = new ClientException(innerException: eventArgs.Exception.InnerException)
            //    {
            //        Origin = ExceptionOrigin.Client,
            //        Severity = ExceptionSeverity.Critical
            //    };
            //}

            // DispatcherHelp is necessary because DXMessageBox is called. Remove on refactor.
            //DispatcherHelp.CheckBeginInvokeOnUI(() => HandleClientException(clientException));

            eventArgs.SetObserved();
        }
    }
}
