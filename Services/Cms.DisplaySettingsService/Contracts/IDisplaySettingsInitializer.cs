using System;

namespace Cms.DisplaySettingsService.Contracts
{
    /// <summary>
    ///     Initializes the display settings.
    /// </summary>
    public interface IDisplaySettingsInitializer : IDisposable
    {
        /// <summary>
        ///     Initializes the display settings by selecting the correct accent and theme.
        /// </summary>
        void Initialize();
    }
}
