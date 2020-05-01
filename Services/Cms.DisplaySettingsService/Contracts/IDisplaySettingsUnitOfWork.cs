using Cms.DisplaySettingsService.Data.Entities;
using System;

namespace Cms.DisplaySettingsService.Contracts
{
    /// <summary>
    ///     The base contract for the display settings unit of work.
    /// </summary>
    public interface IDisplaySettingsUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Save any changes made to the data repositories.
        /// </summary>
        /// <returns>
        ///     An integer indicating the result of the save changes call.
        /// </returns>
        int SaveChanges();

        /// <summary>
        ///     Discard any changes made to the data repositories.
        /// </summary>
        void DiscardChanges();
        
        /// <summary>
        ///     The accents repository.
        /// </summary>
        IReadableDisplaySettingRepository<Accent> Accents { get; }

        /// <summary>
        ///     The themes repository.
        /// </summary>
        IReadableDisplaySettingRepository<Theme> Themes { get; }
    }
}
