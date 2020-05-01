namespace System.Reflection
{
    public static class SystemReflectionExtensions
    {
        /// <summary>
        ///     Tests to see if the specified property is marked with the specified attribute.
        /// </summary>
        /// <param name="propertyInfo">
        ///     The property that is being tested for attribute marcation.
        /// </param>
        /// <param name="attributeType">
        ///     The type of attribute that the property should be marked with.
        /// </param>
        /// <param name="isInherited">
        ///     Indicates if the attribute can be inherited (true) or not (false).
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     This exception is thrown if the parameter, <paramref name="attributeType"/> is 
        ///     null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     This exception is thrown if the parameter, <paramref name="attributeType"/> is
        ///     not an attribute.
        /// </exception>
        /// <returns>
        ///     True if the property is marked with the specified attribute, otherwise false.
        /// </returns>
        public static bool IsMarkedWithAttribute(this PropertyInfo propertyInfo, Type attributeType, bool isInherited)
        {
            if (attributeType == null)
                throw new ArgumentNullException(nameof(attributeType));

            if (!attributeType.IsSubclassOf(typeof(Attribute)))
                throw new ArgumentException($@"The Type, {attributeType.Name}, is not an interface type.", nameof(attributeType));

            return propertyInfo.GetCustomAttribute(attributeType, isInherited) != null;
        }

        /// <summary>
        ///     Tests to see if the specified property is marked with the specified attribute. The attribute cannot be inherited.
        /// </summary>
        /// <param name="propertyInfo">
        ///     The property that is being tested for attribute marcation.
        /// </param>
        /// <param name="attributeType">
        ///     The type of attribute that the property should be marked with.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     This exception is thrown if the parameter, <paramref name="attributeType"/> is 
        ///     null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     This exception is thrown if the parameter, <paramref name="attributeType"/> is
        ///     not an attribute.
        /// </exception>
        /// <returns>
        ///     True if the property is marked with the specified attribute, otherwise false.
        /// </returns>
        public static bool IsMarkedWithAttribute(this PropertyInfo propertyInfo, Type attributeType) =>
            IsMarkedWithAttribute(propertyInfo, attributeType, false);
    }
}
