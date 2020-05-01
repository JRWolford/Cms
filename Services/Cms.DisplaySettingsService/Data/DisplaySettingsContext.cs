using Cms.DisplaySettingsService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cms.DisplaySettingsService.Data
{
    /// <summary>
    ///     The data context of the display settings database.
    /// </summary>
    public sealed class DisplaySettingsContext : DbContext
    {
        #region Methods

        /// <summary>
        ///     Configures the DbContext with the SQLite data connection.
        /// </summary>
        /// <param name="optionsBuilder">
        ///     The context option builder.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Filename = .\EmbeddedDisplaySettings.db");
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The Accents table.
        /// </summary>
        public DbSet<Accent> Accents { get; set; }

        /// <summary>
        ///     The Themes table.
        /// </summary>
        public DbSet<Theme> Themes { get; set; }

        #endregion
    }
}
