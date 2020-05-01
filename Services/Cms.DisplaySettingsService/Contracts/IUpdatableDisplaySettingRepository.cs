namespace Cms.DisplaySettingsService.Contracts
{
    /// <summary>
    ///     The base contract for an updatable display setting repository.
    /// </summary>
    /// <typeparam name="TSetting">
    ///     The type of setting being controlled by the repository.
    /// </typeparam>
    public interface IUpdatableDisplaySettingRepository<TSetting> where TSetting : class, IDisplaySettingEntity
    {
        /// <summary>
        ///     Sets the specified setting as being available.
        /// </summary>
        /// <returns>
        ///     The updated setting model.
        /// </returns>
        TSetting MakeSettingAvailable();

        /// <summary>
        ///     Sets the specified setting as the default value for the setting type.
        /// </summary>
        /// <returns>
        ///     The updated setting model.
        /// </returns>
        TSetting MakeSettingDefault();

        /// <summary>
        ///     Sets the specified setting as the currently selected setting.
        /// </summary>
        /// <returns>
        ///     The updated setting model.
        /// </returns>
        TSetting MakeSettingSelected();

        /// <summary>
        ///     Sets the specified setting as being unavailable.
        /// </summary>
        /// <returns>
        ///     The updated setting model.
        /// </returns>
        TSetting MakeSettingUnavailable();
    }
}
