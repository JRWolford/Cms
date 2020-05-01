using Cms.DisplaySettingsService.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Cms.DisplaySettingsService.Data.Repositories
{
    internal sealed class UpdatableDisplaySettingBase<TSetting> : IUpdatableDisplaySettingRepository<TSetting>
        where TSetting : class, IDisplaySettingEntity
    {
        #region Private Fields

        private readonly TSetting _controlledSetting;

        private readonly DbSet<TSetting> _db;

        #endregion

        #region Constructors

        public UpdatableDisplaySettingBase(TSetting controlledSetting, DbSet<TSetting> db)
        {
            if (controlledSetting == null)
                throw new ArgumentNullException(nameof(controlledSetting));

            if (db == null)
                throw new ArgumentNullException(nameof(db));

            _db = db;
            _controlledSetting = _db.First(s => s.Id == controlledSetting.Id);
        }

        #endregion

        #region Methods

        /// <inheritdoc cref="IUpdatableDisplaySettingRepository{TSetting}.MakeSettingAvailable">
        public TSetting MakeSettingAvailable()
        {
            _controlledSetting.IsAvailable = true;
            _db.Update(_controlledSetting);

            return _controlledSetting;
        }

        /// <inheritdoc cref="IUpdatableDisplaySettingRepository{TSetting}.MakeSettingDefault">
        public TSetting MakeSettingDefault()
        {
            var currentDefaultSettings = _db.Where(s => s.IsDefault);
            foreach (var setting in currentDefaultSettings)
                setting.IsDefault = false;

            _controlledSetting.IsDefault = true;

            _db.UpdateRange(currentDefaultSettings);
            _db.Update(_controlledSetting);

            return _controlledSetting;
        }

        /// <inheritdoc cref="IUpdatableDisplaySettingRepository{TSetting}.MakeSettingSelected">
        public TSetting MakeSettingSelected()
        {
            var currentlySelectedSettings = _db.Where(s => s.IsSelected);
            foreach (var setting in currentlySelectedSettings)
                setting.IsSelected = false;

            _controlledSetting.IsSelected = true;

            _db.UpdateRange(currentlySelectedSettings);
            _db.Update(_controlledSetting);

            return _controlledSetting;
        }

        /// <inheritdoc cref="IUpdatableDisplaySettingRepository{TSetting}.MakeSettingUnavailable">
        public TSetting MakeSettingUnavailable()
        {
            _controlledSetting.IsAvailable = false;
            _db.Update(_controlledSetting);

            return _controlledSetting;
        }

        #endregion
    }
}
