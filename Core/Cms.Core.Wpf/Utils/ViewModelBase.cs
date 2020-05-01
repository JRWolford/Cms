using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Cms.Core.Wpf.Utils
{
    /// <summary>
    ///     The base class for a viewmodel.
    /// </summary>
    public abstract class ViewModelBase : BindableBase, IDisposable
    {
        #region Private Fields

        protected bool IsDisposed;

        private readonly Dictionary<string, bool> DirtyProperties;

        #endregion

        #region Constructors

        public ViewModelBase()
        {
            DirtyProperties = new Dictionary<string, bool>();
            IsDisposed = false;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Set this action to encapsulate all disposable objects that the viewmodel has used that
        ///     need to be disposed of when <see cref="IDisposable.Dispose"/> is called.
        /// </summary>
        protected Action Disposals { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Disposes of the view model.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     This method handles the disposal patter implementation for the viewmodel.
        /// </summary>
        /// <param name="isDisposing">
        ///     Inidactes if the viewmodel is being disposed of or not.
        /// </param>
        /// <remarks>
        ///     To incorporate your disposables in this dispose method, simply set the action,
        ///     <see cref="Disposals"/> to include them.
        /// </remarks>
        /// <example>
        /// 
        ///     private readonly ISomethingDisposable _disposableObject;
        /// 
        ///     public ClassConstructor(ISomethingDisposable disposableObject)
        ///     {
        ///         _disposableObject = disposableObject;
        ///         Disposals = () => 
        ///         {
        ///             _disposableObject.Dispose();
        ///         }
        ///     }
        /// 
        /// </example>
        private void Dispose(bool isDisposing)
        {
            if (!isDisposing)
                return;

            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);

            Disposals?.Invoke();
            IsDisposed = true;
        }

        /// <summary>
        ///     Tests to see if the specified property is marked as dirty or not.
        /// </summary>
        /// <param name="propertyName">
        ///     The name of the property to be tested.
        /// </param>
        /// <returns>
        ///     True if the property is marked as dirty, otherwise false.
        /// </returns>
        /// <exception cref="NullReferenceException">
        ///     This exception is thrown if the <see cref="DirtyProperties"/> dictionary is not
        ///     instantiated in the constructor.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     This exception is thrown if the <paramref name="propertyName"/> value is not found
        ///     in the properties list of the view model.
        /// </exception>
        protected bool IsPropertyDirty(string propertyName)
        {
            try
            {
                return DirtyProperties[propertyName];
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(
                    $@"The {nameof(DirtyProperties)} dictionary was not instantiated in the constructor.", e);
            }
            catch (KeyNotFoundException)
            {
                if (GetType().GetProperties().First(p => p.Name == propertyName) == null)
                    throw new ArgumentOutOfRangeException($@"The view model contains no property with the name, {propertyName}.");

                return false;
            }
        }

        /// <summary>
        ///     Extends Prism's bindable property framework to include dirty properties for data validation.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of property being set.
        /// </typeparam>
        /// <param name="storage">
        ///     The property's backing field.
        /// </param>
        /// <param name="value">
        ///     The value the property is being set to.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property being set.
        /// </param>
        /// <param name="isNowDirty">
        ///     Indicates if the proprety should be marked as "dirty" or not.
        /// </param>
        /// <returns>
        ///     True if the property has been successfully set, otherwise false.
        /// </returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null, bool isNowDirty = true)
        {
            if (!base.SetProperty(ref storage, value, propertyName))
                return false;

            try
            {
                DirtyProperties[propertyName] = isNowDirty;
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(
                    $@"The {nameof(DirtyProperties)} dictionary was not instantiated in the constructor.", e);
            }
            catch (KeyNotFoundException)
            {
                DirtyProperties.Add(propertyName, isNowDirty);
            }

            return true;
        }

        #endregion
    }
}
