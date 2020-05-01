using Cms.DisplaySettingsService.Contracts;

namespace Cms.DisplaySettingsService.Data.Entities
{
    /// <summary>
    ///     The model for an accent entity.
    /// </summary>
    public class Accent : IDisplaySettingEntity
    {
        /// <summary>
        ///     The primary key of the accent color.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The name of the color of the accent.
        /// </summary>
        /// <example>
        ///     Orange
        /// </example>
        public string Name { get; set; }

        /// <summary>
        ///     A short description of the accent.
        /// </summary>
        /// <example>
        ///     A bright orange accent color that really pops!
        /// </example>
        public string Description { get; set; }

        /// <summary>
        ///     Indicates if the accent color is the default color for the application or not.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        ///     Indicates if the user has selected the accent as their preferred accent color.
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        ///     The URI string of the accent's resource dictionary.
        /// </summary>
        public string UriString { get; set; }

        /// <summary>
        ///     The red hue value of the primary accent color.
        /// </summary>
        public byte Red { get; set; }

        /// <summary>
        ///     The blue hue value of the primary accent color.
        /// </summary>
        public byte Blue { get; set; }

        /// <summary>
        ///     The green hue value of the primary accent color.
        /// </summary>
        public byte Green { get; set; }

        /// <summary>
        ///     Indicates if the accent is available or not.
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
