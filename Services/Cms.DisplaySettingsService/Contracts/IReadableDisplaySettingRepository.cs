using System.Collections.Generic;

namespace Cms.DisplaySettingsService.Contracts
{
    /// <summary>
    ///     The base contract for a readable display setting repository.
    /// </summary>
    /// <typeparam name="TSetting">
    ///     The type of setting being controlled by the repository.
    /// </typeparam>
    public interface IReadableDisplaySettingRepository<TSetting> where TSetting : class, IDisplaySettingEntity
    {
        /// <summary>
        ///     Returns all setting options from the database.
        /// </summary>
        /// <returns>
        ///     All currently available options for the setting from the database.
        /// </returns>
        IEnumerable<TSetting> GetAllSettings();

        /// <summary>
        ///     Returns the current setting value.
        /// </summary>
        /// <returns>
        ///     The currently selected preferred setting. If the user has not selected a preferred setting 
        ///     then the default setting is used.
        /// </returns>
        TSetting GetCurrentSetting();

        /// <summary>
        ///     Returns the default setting from the setting table.
        /// </summary>
        /// <returns>
        ///     The default value from the settings table.
        /// </returns>
        /// <remarks>
        ///     If there is no default entry in the table as indicated by the <see cref="TSetting.IsDefault"/>
        ///     value, the first entry in the table is returned. If there are multiple <see cref="TSetting.IsDefault"/>
        ///     entries then the first of the default values is returned.
        /// </remarks>
        TSetting GetDefaultSetting();

        /// <summary>
        ///     Returns the setting with a matching primary key.
        /// </summary>
        /// <param name="id">
        ///     The primary key of the setting to return.
        /// </param>
        /// <returns>
        ///     The correct setting specified.
        /// </returns>
        TSetting GetSetting(int id);

        /// <summary>
        ///     Restores the setting to its default value.
        /// </summary>
        void RestoreDefaultSetting();

        /// <summary>
        ///     Provides acces to the methods used for updating or controlling the specified setting.
        /// </summary>
        /// <param name="setting">
        ///     The setting being updated.
        /// </param>
        /// <returns>
        ///     A fully instantiated instance of the an updatable display setting repository.
        /// </returns>
        IUpdatableDisplaySettingRepository<TSetting> UpdateSetting(TSetting setting);

        /// <summary>
        ///     Provides acces to the methods used for updating or controlling the specified setting.
        /// </summary>
        /// <param name="settingId">
        ///     The primary key of the setting being updated.
        /// </param>
        /// <returns>
        ///     A fully instantiated instance of the an updatable display setting repository.
        /// </returns>
        IUpdatableDisplaySettingRepository<TSetting> UpdateSetting(int settingId);
    }
}
