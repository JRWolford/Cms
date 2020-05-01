using Cms.DisplaySettingsService.Contracts;
using Cms.DisplaySettingsService.Data.Entities;
using Cms.DisplaySettingsService.Utils;
using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Cms.DisplaySettingsService
{
    /// <summary>
    ///     The display settings manager. Allows the developer to select the current theme 
    ///     and accent color, as well as to determine the current theme and accent color.
    /// </summary>
    public sealed class DisplaySettingsManager : IDisplaySettingsManager, IDisplaySettingsInitializer
    {
        #region Private Fields

        private readonly Application _currentApplication;

        private readonly IDisplaySettingsUnitOfWork _dispSettingsDb;

        private bool _isDisposed;

        #endregion

        #region Constructors

        public DisplaySettingsManager(IDisplaySettingsUnitOfWork displaySettingsUnitOfWork)
        {
            _dispSettingsDb = displaySettingsUnitOfWork;
            _currentApplication = Application.Current;
            _isDisposed = false;

            LoadCurrentAccent();
            LoadCurrentTheme();
        }

        #endregion

        #region Properties

        /// <inheritdoc cref="IDisplaySettingsManager.CurrentAccent"/>
        public DisplaySettingsService.Data.Entities.Accent CurrentAccent { get; private set; }

        /// <inheritdoc cref="IDisplaySettingsManager.CurrentTheme"/>
        public DisplaySettingsService.Data.Entities.Theme CurrentTheme { get; private set; }

        #endregion

        #region Methods

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Implementation of disposal pattern. Disposes of display settings
        ///     database reference.
        /// </summary>
        /// <param name="isDisposing">
        ///     Indicates if the class is being disposed of or not.
        /// </param>
        private void Dispose(bool isDisposing)
        {
            if (!isDisposing)
                return;

            if (isDisposing)
            {
                _dispSettingsDb.Dispose();
                _isDisposed = true;
            }

        }

        /// <summary>
        ///     Adds all of the available accents to the <see cref="ThemeManager"/>.
        /// </summary>
        private void AddAccents()
        {
            var accents = _dispSettingsDb.Accents.GetAllSettings();
            foreach(var accent in accents.Where(a => a.IsAvailable))
            {
                try
                {
                    ThemeManager.AddAccent(accent.Name, new Uri(accent.UriString));
                }
                catch
                {
                    _dispSettingsDb.Accents.UpdateSetting(accent).MakeSettingUnavailable();
                }
            }
        }

        /// <summary>
        ///     Loads the currently selected accent.
        /// </summary>
        private void LoadCurrentAccent() =>
            CurrentAccent = _dispSettingsDb.Accents.GetCurrentSetting();   
        
        /// <summary>
        ///     Loads the currently selected theme.
        /// </summary>
        private void LoadCurrentTheme() =>
            CurrentTheme = _dispSettingsDb.Themes.GetCurrentSetting();

        /// <summary>
        ///     Adds all of the available themes to the <see cref="ThemeManager"/>.
        /// </summary>
        private void AddThemes()
        {
            var themes = _dispSettingsDb.Themes.GetAllSettings();
            foreach (var theme in themes.Where(t => t.IsAvailable))
            {
                try
                {
                    ThemeManager.AddAppTheme(theme.Name, new Uri(theme.UriString));
                }
                catch
                {
                    _dispSettingsDb.Themes.UpdateSetting(theme).MakeSettingUnavailable();
                }
            }
        }

        /// <inheritdoc cref="IDisplaySettingsManager.RestoreDefaults"/>
        public void RestoreDefaults(Restore option = Restore.All)
        {
            switch (option)
            {
                case Restore.All:
                    _dispSettingsDb.Accents.RestoreDefaultSetting();
                    _dispSettingsDb.Themes.RestoreDefaultSetting();

                    SelectAccent(_dispSettingsDb.Accents.GetDefaultSetting());
                    SelectTheme(_dispSettingsDb.Themes.GetDefaultSetting());
                    break;
                case Restore.Accents:
                    _dispSettingsDb.Accents.RestoreDefaultSetting();

                    SelectAccent(_dispSettingsDb.Accents.GetDefaultSetting());
                    break;
                case Restore.Themes:
                    _dispSettingsDb.Themes.RestoreDefaultSetting();

                    SelectTheme(_dispSettingsDb.Themes.GetDefaultSetting());
                    break;
            }

            _dispSettingsDb.SaveChanges();

            
        }

        /// <inheritdoc cref="IDisplaySettingsManager.SelectAccent(Accent)"/>
        public bool SelectAccent(DisplaySettingsService.Data.Entities.Accent accent)
        {
            if (accent == CurrentAccent)
                return true;

            try
            {
                ThemeManager.ChangeAppStyle(_currentApplication, ThemeManager.GetAccent(accent.Name),
                    ThemeManager.GetAppTheme(CurrentTheme.Name));

                _dispSettingsDb.Accents.UpdateSetting(accent).MakeSettingSelected();
                _dispSettingsDb.SaveChanges();

                CurrentAccent = accent;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc cref="IDisplaySettingsManager.SelectAccent(int)"/>
        public bool SelectAccent(int id)
        {
            if (id == CurrentAccent.Id)
                return true;

            var selectedAccent = _dispSettingsDb.Accents.GetSetting(id);
            return SelectAccent(selectedAccent);
        }

        /// <inheritdoc cref="IDisplaySettingsManager.SelectTheme(Theme)"/>
        public bool SelectTheme(Theme theme)
        {
            if (theme == CurrentTheme)
                return true;

            try
            {
                ThemeManager.ChangeAppTheme(_currentApplication, theme.Name);

                _dispSettingsDb.Themes.UpdateSetting(theme).MakeSettingSelected();
                _dispSettingsDb.SaveChanges();

                CurrentTheme = theme;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <inheritdoc cref="IDisplaySettingsManager.SelectTheme(int)"/>
        public bool SelectTheme(int id)
        {
            if (id == CurrentTheme.Id)
                return true;

            var selectedTheme = _dispSettingsDb.Themes.GetSetting(id);
            return SelectTheme(selectedTheme);
        }

        /// <inheritdoc cref="IDisplaySettingsInitializer.Initialize"/>
        void IDisplaySettingsInitializer.Initialize()
        {
            AddAccents();
            AddThemes();

            Data.Entities.Accent currentAccent = _dispSettingsDb.Accents.GetCurrentSetting();
            Theme currentTheme = _dispSettingsDb.Themes.GetCurrentSetting();

            try
            {
                ThemeManager.ChangeAppStyle(_currentApplication, ThemeManager.GetAccent(currentAccent.Name),
                    ThemeManager.GetAppTheme(currentTheme.Name));

                CurrentAccent = currentAccent;
            }
            catch (Exception)
            {
                SelectAccent(currentAccent);
            }

            try
            {
                ThemeManager.ChangeAppTheme(_currentApplication, currentTheme.Name);
                CurrentTheme = currentTheme;
            }
            catch (Exception)
            {
                SelectTheme(currentTheme);
            }
        }

        /// <inheritdoc cref="IDisplaySettingsManager.GetAvailableAccentColorOptions"/>
        public IEnumerable<DisplaySettingsService.Data.Entities.Accent> GetAvailableAccentColorOptions() =>
            _dispSettingsDb.Accents.GetAllSettings() as IEnumerable<Data.Entities.Accent>;

        public IEnumerable<Theme> GetAvailableThemeOptions() =>
            _dispSettingsDb.Themes.GetAllSettings() as IEnumerable<Theme>;

        #endregion
    }
}
