using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Cms.Core.Utils
{
    /// <summary>
    ///     A utility class for interacting with embedded resources.
    /// </summary>
    public sealed class EmbeddedResourceUtility
    {
        #region Private Fields

        private readonly IEnumerable<string> _embeddedResourceNames;

        private readonly Assembly _assembly;

        #endregion

        #region Constructors

        public EmbeddedResourceUtility()
        {
            _assembly = Assembly.GetCallingAssembly();
            _embeddedResourceNames = _assembly.GetManifestResourceNames();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns the stream for the specified embedded resource in the calling assembly.
        /// </summary>
        /// <param name="resourceName">
        ///     The name of the embedded resource that the stream should be returned for.
        /// </param>
        /// <returns>
        ///     The stream for the specified resource.
        /// </returns>
        public Stream GetEmbeddedResourceStream(string resourceName)
        {
            if (!_embeddedResourceNames.Contains(resourceName))
                throw new ArgumentOutOfRangeException(nameof(resourceName), $@"The Assembly, {_assembly.GetName().Name}, does not contain an embedded resource with the name, {resourceName}.");

            var stream = _assembly.GetManifestResourceStream(resourceName);

            var streamType = stream.GetType();

            var uStream = stream as UnmanagedMemoryStream;


            return _assembly.GetManifestResourceStream(resourceName);
        }

        /// <summary>
        ///     Returns a list of the names of all the embedded resources for the calling assembly.
        /// </summary>
        /// <returns>
        ///     A list of 
        /// </returns>
        public IEnumerable<string> GetEmbeddedResourceNames() =>
            _embeddedResourceNames;

        #endregion
    }
}
