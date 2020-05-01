using Cms.DisplaySettingsService.Contracts;

namespace Cms.DisplaySettingsService.Data.Entities
{
    /// <summary>
    ///     The model for a theme entity.
    /// </summary>
    public class Theme : IDisplaySettingEntity
    {
        /// <summary>
        ///     The primary key of the theme.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The name of the theme.
        /// </summary>
        /// <example>
        ///     Light
        /// </example>
        public string Name { get; set; }

        /// <summary>
        ///     A short description of the theme.
        /// </summary>
        /// <example>
        ///     A light theme that uses whites and light grays.
        /// </example>
        public string Description { get; set; }

        /// <summary>
        ///     Indicates if the theme is the default theme for the application or not.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        ///     Indicates if the user has selected the theme as their preferred theme.
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        ///     The URI string of the theme's resource dictionary.
        /// </summary>
        public string UriString { get; set; }

        /// <summary>
        ///     Indicates if the theme is available or not.
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
