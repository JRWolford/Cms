namespace Cms.DisplaySettingsService.Contracts
{
    /// <summary>
    ///     The base contract for a display setting entity model.
    /// </summary>
    public interface IDisplaySettingEntity
    {
        /// <summary>
        ///     The primary key of the display setting entity.
        /// </summary>
        int Id { get; }

        /// <summary>
        ///     The name of the display setting.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     A short description of the setting.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        ///     Indicates if the setting is the default setting of the application or not.
        /// </summary>
        bool IsDefault { get; set; }

        /// <summary>
        ///     Indicates if the user has selected the setting as their preferred setting.
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        ///     Indicates if the setting is currently available for selection or not.
        /// </summary>
        bool IsAvailable { get; set; }
    }
}
