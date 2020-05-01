using Cms.DisplaySettingsService.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cms.DisplaySettingsService.Data.Repositories
{
    internal abstract class ReadableDisplaySettingRepositoryBase<TSetting> : IReadableDisplaySettingRepository<TSetting>
        where TSetting : class, IDisplaySettingEntity
    {
        #region Private Fields

        protected readonly DbSet<TSetting> Db;

        #endregion
        
        #region Constructors

        public ReadableDisplaySettingRepositoryBase(DbSet<TSetting> db)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            Db = db;
        }

        #endregion

        #region Methods

        /// <inheritdoc cref="IReadableDisplaySettingRepository{TSetting}.GetAllSettings"/>
        public virtual IEnumerable<TSetting> GetAllSettings() =>
            Db.ToList();

        /// <inheritdoc cref="IReadableDisplaySettingRepository{TSetting}.GetCurrentSetting"/>
        public virtual TSetting GetCurrentSetting()
        {
            TSetting currentSetting;
            currentSetting = Db.FirstOrDefault(s => s.IsSelected);

            if (currentSetting == null)
                currentSetting = Db.First(s => s.IsDefault);

            return currentSetting;
        }

        /// <inheritdoc cref="IReadableDisplaySettingRepository{TSetting}.GetDefaultSetting"/>
        public virtual TSetting GetDefaultSetting()
        {
            TSetting defaultSetting;
            defaultSetting = Db.FirstOrDefault(s => s.IsDefault);

            if (defaultSetting == null)
                defaultSetting = Db.First();

            return defaultSetting;
        }

        /// <inheritdoc cref="IReadableDisplaySettingRepository{TSetting}.GetSetting(int)"/>
        public virtual TSetting GetSetting(int id) =>
            Db.FirstOrDefault(s => s.Id == id);

        /// <inheritdoc cref="IReadableDisplaySettingRepository{TSetting}.RestoreDefaukts"/>
        public virtual void RestoreDefaultSetting()
        {
            foreach (var setting in Db.ToList())
            {
                if (setting.IsDefault)
                    setting.IsSelected = true;
                else
                    setting.IsSelected = false;
            }
        }

        /// <inheritdoc cref="IReadableDisplaySettingRepository{TSetting}.UpdateSetting(TSetting)"/>
        public virtual IUpdatableDisplaySettingRepository<TSetting> UpdateSetting(TSetting setting) => 
            new UpdatableDisplaySettingBase<TSetting>(setting, Db);

        /// <inheritdoc cref="IReadableDisplaySettingRepository{TSetting}.UpdateSetting(int)"/>
        public virtual IUpdatableDisplaySettingRepository<TSetting> UpdateSetting(int settingId)
        {
            var setting = Db.First(s => s.Id == settingId);
            return UpdateSetting(setting);
        }

        #endregion
    }
}
