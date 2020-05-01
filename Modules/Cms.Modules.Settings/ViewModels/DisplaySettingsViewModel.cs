using Cms.Core.Wpf.Utils;
using Cms.DialogService.Contracts;
using Cms.DisplaySettingsService.Contracts;
using Cms.DisplaySettingsService.Utils;
using Cms.Modules.Settings.Utils;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Cms.Modules.Settings.ViewModels
{
    public class DisplaySettingsViewModel : ViewModelBase
    {
        #region Private Fields

        private readonly IDialogService _dialogService;

        private readonly IDisplaySettingsManager _displaySettingsManager;

        private ObservableCollection<AccentOption> _accentOptions;

        #endregion

        #region Constructors

        public DisplaySettingsViewModel(IDialogService dialogService, IDisplaySettingsManager displaySettingsManager, IDisplaySettingsUnitOfWork dispSettingsDb)
        {
            if (dialogService == null)
                throw new ArgumentNullException(nameof(dialogService));

            if (displaySettingsManager == null)
                throw new ArgumentNullException(nameof(displaySettingsManager));

            if (dispSettingsDb == null)
                throw new ArgumentNullException(nameof(dispSettingsDb));

            _dialogService = dialogService;
            _displaySettingsManager = displaySettingsManager;

            Disposals = () =>
            {
                _displaySettingsManager.Dispose();
            };

            LoadAccentOptions();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     A collection of options for the accent color.
        /// </summary>
        public ObservableCollection<AccentOption> AccentOptions
        {
            get => _accentOptions;
            private set => SetProperty(ref _accentOptions, value);
        }

        #endregion

        #region Commands

        /// <summary>
        ///     The command for restoring the default display settings.
        /// </summary>
        public DelegateCommand RestoreDefaultsCommand =>
            new DelegateCommand(RestoreDefaults);

        /// <summary>
        ///     The command for selecting a different accent color.
        /// </summary>
        public DelegateCommand<AccentOption> SelectAccentCommand =>
            new DelegateCommand<AccentOption>(SelectAccent, (o) => o.IsAvailable);

        #endregion

        #region Methods

        /// <summary>
        ///     Loads the available accent options from the display settings database.
        /// </summary>
        private void LoadAccentOptions()
        {
            if (_displaySettingsManager == null)
                throw new NullReferenceException($@"The display settings manager was not instantiated in the constructor of the {nameof(DisplaySettingsViewModel)}.");

            try
            {
                AccentOptions = new ObservableCollection<AccentOption>(AccentOptionFactory.CreateAccentOptions(_displaySettingsManager.GetAvailableAccentColorOptions()));
            }
            catch(ObjectDisposedException e)
            {
                throw new ObjectDisposedException($@"The accent options were attempting to load after the display settings manager was disposed of in the {nameof(DisplaySettingsViewModel)}.", e);
            }
            catch(Exception e)
            {
                throw new Exception($@"Some unhandled exception occurred while trying to load the accent options in the {nameof(DisplaySettingsViewModel)}.", e);
            }
        }

        /// <summary>
        ///     Restores the default settings of the 
        /// </summary>
        private void RestoreDefaults()
        {
            if (_displaySettingsManager == null)
                throw new NullReferenceException($@"The display settings manager was not instantiated in the constructor of the {nameof(DisplaySettingsViewModel)}.");

            if (AccentOptions == null)
                throw new NullReferenceException($@"The {nameof(AccentOptions)} was not instantiated when the {nameof(DisplaySettingsViewModel)} was constructed.");

            try
            {
                _displaySettingsManager.RestoreDefaults(Restore.Accents);
            }
            catch(ObjectDisposedException e)
            {
                throw new ObjectDisposedException($@"The {nameof(DisplaySettingsViewModel)} was attempting to restore the default accent option in the database but the display settings manager was disposed of first.", e);
            }
            catch(Exception e)
            {
                throw new Exception($@"Some unhandled exception occurred while trying to restore the default accent option in the display settings manager.", e);
            }

            try
            {
                var currentSelection = AccentOptions.First(o => o.IsSelected);
                if (!currentSelection.IsDefault)
                    currentSelection.IsSelected = false;

                AccentOptions.First(o => o.IsDefault).IsSelected = true;
            }
            catch (Exception e)
            {
                throw new Exception($@"Some unhandled exception occurred while trying to restore the accent options in the {nameof(DisplaySettingsViewModel)}.", e);
            }
        }

        /// <summary>
        ///     Sets the <paramref name="selectedAccent"/> provided as the current display color accent of the application.
        /// </summary>
        /// <param name="selectedAccent">
        ///     The accent to set as the current display color.
        /// </param>
        private async void SelectAccent(AccentOption selectedAccent)
        {
            if (AccentOptions == null)
                throw new NullReferenceException($@"The {nameof(AccentOptions)} were not instantiated when the class was constructed.");

            if (_displaySettingsManager == null)
                throw new NullReferenceException($@"The display settings manager was not instantiated in the constructor of the {nameof(DisplaySettingsViewModel)}.");

            try
            {
                foreach(var option in AccentOptions)
                {
                    if (option.Id == selectedAccent.Id)
                        option.IsSelected = true;
                    else
                        option.IsSelected = false;
                }

                _displaySettingsManager.SelectAccent(selectedAccent.Id);
            }
            catch(ObjectDisposedException e)
            {
                throw new ObjectDisposedException($@"The {nameof(DisplaySettingsViewModel)} was trying to select a different accent but the display settings db was already disposed.", e);
            }
            catch(Exception)
            {
                await _dialogService.ShowDialogAsync("Accent Unavailable", $@"The accent color, {selectedAccent.Name}, is not currently available.");

                selectedAccent.IsAvailable = false;
                selectedAccent.Name = $@"{selectedAccent.Name} (Unavailable)";
            }


        }

        #endregion
    }
}
