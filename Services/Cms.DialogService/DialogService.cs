using Cms.DialogService.Contracts;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;

namespace Cms.DialogService
{
    /// <inheritdoc cref="IDialogService"/>
    public sealed class DialogService : IDialogService
    {
        #region Private Fields

        private readonly MetroWindow _metroWindow;

        #endregion

        #region Constructors

        public DialogService() =>
            _metroWindow = (MetroWindow)Application.Current.MainWindow;

        #endregion

        #region Methods

        /// <inheritdoc cref="IDialogService.ShowDialogAsync(string, string, MessageDialogStyle, MetroDialogSettings)"/>
        public Task<MessageDialogResult> ShowDialogAsync(string title, string message, MessageDialogStyle dialogStyle = MessageDialogStyle.Affirmative, MetroDialogSettings settings = null) =>
            _metroWindow.ShowMessageAsync(title, message, dialogStyle, settings);

        #endregion
    }
}
