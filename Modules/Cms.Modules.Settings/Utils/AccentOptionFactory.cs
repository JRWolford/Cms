using Cms.DisplaySettingsService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Cms.Modules.Settings.Utils
{
    /// <summary>
    ///     A factory class for manufacturing <see cref="AccentOption"/> object models.
    /// </summary>
    internal static class AccentOptionFactory
    {
        /// <summary>
        ///      Creates an enumerable list of <see cref="AccentOption"/> models from a list
        ///      of <see cref="Accent"/> models.
        /// </summary>
        /// <param name="accents">
        ///     The <see cref="Accent"/> entity models to be converterd to <see cref="AccentOption"/>
        ///     models.
        /// </param>
        /// <returns>
        ///     An enumerable list of fully instantiated <see cref="AccentOption"/> models.
        /// </returns>
        internal static IEnumerable<AccentOption> CreateAccentOptions(IEnumerable<Accent> accents)
        {
            if (accents == null)
                throw new ArgumentNullException(nameof(accents));

            var createdAccents = new Collection<AccentOption>();
            foreach(var accent in accents)
            {
                createdAccents.Add(new AccentOption
                {
                    Name = accent.Name,
                    ToolTip = accent.Description,
                    IsSelected = accent.IsSelected,
                    IsAvailable = accent.IsAvailable,
                    IsDefault = accent.IsDefault,
                    AccentColor = Color.FromRgb(accent.Red, accent.Green, accent.Blue)
                });
            }

            return createdAccents;
        }
    }
}
