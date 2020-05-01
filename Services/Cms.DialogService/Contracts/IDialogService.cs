using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace Cms.DialogService.Contracts
{
    /// <summary>
    ///     Provides services for interacting with the MahApps message dialog system.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        ///     Shows a simple dialog window that is populated with the specified parameters, asynchronously.
        /// </summary>
        /// <param name="title">
        ///     The title applied to the dialog window.
        /// </param>
        /// <param name="message">
        ///     The message displayed on the dialog window.
        /// </param>
        /// <param name="dialogStyle">
        ///     The dialog style of the window, this changes the available buttons. To change the button names adjust the <paramref name="settings"/>.
        /// </param>
        /// <param name="settings">
        ///     Any specific style settings to be applied to the dialog window.
        /// </param>
        /// <returns>
        ///     The dialog result of the user's interaction with the dialog window.
        /// </returns>
        Task<MessageDialogResult> ShowDialogAsync(string title, string message, MessageDialogStyle dialogStyle = MessageDialogStyle.Affirmative, MetroDialogSettings settings = null);
    }
}
