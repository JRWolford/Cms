using Prism.Mvvm;
using System.Windows.Media;

namespace Cms.Modules.Settings.Utils
{
    /// <summary>
    ///     The dto for an accent entity model.
    /// </summary>
    public sealed class AccentOption : BindableBase
    {
        #region Private Fields

        private Color _accentColor;

        private int _id;

        private bool _isAvailable;

        private bool _isDefault;

        private bool _isSelected;

        private string _name;

        private string _toolTip;

        #endregion

        #region Properties

        /// <summary>
        ///     The color of the accent.
        /// </summary>
        public Color AccentColor
        {
            get => _accentColor;
            set => SetProperty(ref _accentColor, value);
        }

        /// <summary>
        ///     The primary key of the <see cref="Accent"/> entity model that
        ///     the <see cref="AccentOption"/> 
        /// </summary>
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        ///     Indicates if the accent is currently available or not.
        /// </summary>
        public bool IsAvailable
        {
            get => _isAvailable;
            set => SetProperty(ref _isAvailable, value);
        }

        /// <summary>
        ///     Indicates if the accent option is the default option or not.
        /// </summary>
        public bool IsDefault
        {
            get => _isDefault;
            set => SetProperty(ref _isDefault, value);
        }

        /// <summary>
        ///     Indicates if the accent is currently selected or not.
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        /// <summary>
        ///     The name of the accent.
        /// </summary>
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        /// <summary>
        ///     The tool tip of the accent.
        /// </summary>
        public string ToolTip
        {
            get => _toolTip;
            set => SetProperty(ref _toolTip, value);
        }

        #endregion
    }
}
