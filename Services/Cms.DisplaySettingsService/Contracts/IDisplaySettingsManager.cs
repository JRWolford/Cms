using Cms.DisplaySettingsService.Data.Entities;
using Cms.DisplaySettingsService.Utils;
using System;
using System.Collections.Generic;

namespace Cms.DisplaySettingsService.Contracts
{
    /// <summary>
    ///     The contract for the display settings manager service.
    /// </summary>
    public interface IDisplaySettingsManager : IDisposable
    {
        /// <summary>
        ///     Returns the currently selected accent.
        /// </summary>
        Accent CurrentAccent { get; }

        /// <summary>
        ///     Returns the currently selected theme.
        /// </summary>
        Theme CurrentTheme { get; }

        /// <summary>
        ///     Sets the specified accent color and indicates if the accent was 
        ///     successfully applied or not.
        /// </summary>
        /// <param name="accent">
        ///     The accent color that should be selected.
        /// </param>
        /// <returns>
        ///     True if the accent was successfully selected, otherwise false.
        /// </returns>
        bool SelectAccent(Accent accent);

        /// <summary>
        ///     Sets the specified accent color and indicates if the accent was 
        ///     successfully applied or not.
        /// </summary>
        /// <param name="id">
        ///     The primary key of the accent color that should be selected.
        /// </param>
        /// <returns>
        ///     True if the accent was successfully selected, otherwise false.
        /// </returns>
        bool SelectAccent(int id);

        /// <summary>
        ///     Sets the specified theme and indicates if the theme was 
        ///     successfully set or not.
        /// </summary>
        /// <param name="theme">
        ///     The theme that should be selected.
        /// </param>
        /// <returns>
        ///     True if the theme was successfully selected, otherwise false.
        /// </returns>
        bool SelectTheme(Theme theme);

        /// <summary>
        ///     Sets the specified theme and indicates if the theme was 
        ///     successfully set or not.
        /// </summary>
        /// <param name="id">
        ///     The primary key of the theme that should be selected.
        /// </param>
        /// <returns>
        ///     True if the theme was successfully selected, otherwise false.
        /// </returns>
        bool SelectTheme(int id);

        /// <summary>
        ///     Returns a list of available accent color options.
        /// </summary>
        /// <returns>
        ///     All available color options;
        /// </returns>
        IEnumerable<Accent> GetAvailableAccentColorOptions();

        /// <summary>
        ///     Returns a list of available theme options.
        /// </summary>
        /// <returns>
        ///     All available theme options.
        /// </returns>
        IEnumerable<Theme> GetAvailableThemeOptions();

        /// <summary>
        ///     Restores the default display settings within the local data cache.
        /// </summary>
        /// <param name="option">
        ///     Indicates what display setting is to be restored.
        /// </param>
        void RestoreDefaults(Restore option = Restore.All);
    }
}
