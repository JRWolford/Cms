using Cms.DisplaySettingsService.Contracts;
using Cms.DisplaySettingsService.Data.Repositories;
using Cms.DisplaySettingsService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cms.DisplaySettingsService.Data
{
    /// <summary>
    ///     The unit of work implementation for the display settings data repositories.
    /// </summary>
    public sealed class DisplaySettingsUnitOfWork : IDisplaySettingsUnitOfWork
    {
        #region Private Fields

        private readonly DisplaySettingsContext _context;

        private bool _isDisposed;

        #endregion

        #region Constructors

        public DisplaySettingsUnitOfWork()
        {
            _context = new DisplaySettingsContext();
            _isDisposed = false;
        }

        #endregion

        #region Methods

        /// <inheritdoc cref="IDisplaySettingsUnitOfWork.Accents"
        public IReadableDisplaySettingRepository<Accent> Accents
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(_context));

                return new AccentsRepository(_context.Accents); ;
            }
        }
            
        /// <inheritdoc cref="IDisplaySettingsUnitOfWork.Themes"
        public IReadableDisplaySettingRepository<Theme> Themes
        {
            get
            {
                if (_isDisposed)
                    throw new ObjectDisposedException(nameof(_context));

                return new ThemesRepository(_context.Themes);
            }
        } 

        /// <inheritdoc cref="IDisplaySettingsUnitOfWork.DiscardChanges"
        public void DiscardChanges()
        {
            foreach(var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        /// <summary>
        ///     Disposes of the database context.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Discards of the changes and disposes of the database context.
        /// </summary>
        /// <param name="disposing">
        /// 
        /// </param>
        private void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                DiscardChanges();
                _context.Dispose();
            }
        }

        /// <inheritdoc cref="IDisplaySettingsUnitOfWork.SaveChanges"/>
        public int SaveChanges()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(_context));

            return _context.SaveChanges();
        }

        #endregion
    }
}
