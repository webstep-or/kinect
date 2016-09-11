using System;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Views;

namespace KinectHandTracking.Services
{
    public class DialogService : IDialogService
    {
        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            MessageBox.Show(error.Message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public async Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);            
        }

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            return MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK ? true : false; 
        }

        public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            await ShowMessageBox(message, title);
        }

        public async Task ShowMessage(string message, string title)
        {
            await ShowMessageBox(message, title);
        }

        public async Task ShowMessageBox(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
