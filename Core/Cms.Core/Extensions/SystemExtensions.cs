using System.Linq;
using System.Reflection;

namespace System
{
    /// <summary>
    ///     A collection of extension methods for objects in the <see cref="System"/> namespace.
    /// </summary>
    public static class SystemExtensions
    {
        /// <summary>
        ///     Tests to see if the specified class type implements the specified interface type.
        /// </summary>
        /// <param name="classType">
        ///     The class that should implement the interface.
        /// </param>
        /// <param name="interfaceType">
        ///     The interface that the class should be implementing.
        /// </param>
        /// <returns>
        ///     True if the <paramref name="classType"/> is implementing the <paramref name="interfaceType"/>,
        ///     otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     This exception is raised if either the <paramref name="classType"/> or <paramref name="interfaceType"/>
        ///     parameters are null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     This argument is raised if the <paramref name="classType"/> is not a class. Or if the <paramref name="interfaceType"/>
        ///     is not an interface.
        /// </exception>
        public static bool IsImplementingInterface(this Type classType, Type interfaceType)
        {
            if (!classType.IsClass)
                throw new ArgumentException($@"The Type, {classType.Name}, is not a class type.", nameof(classType));

            if (interfaceType == null)
                throw new ArgumentNullException(nameof(interfaceType));

            if (!interfaceType.IsInterface)
                throw new ArgumentException($@"The Type, {interfaceType.Name}, is not an interface type.", nameof(interfaceType));

            var implementedInterfaces = classType.GetInterfaces();
            if (implementedInterfaces == null)
                return false;

            return implementedInterfaces.Contains(interfaceType);
        }

        /// <summary>
        ///     Tests to see if a type is marked with an attribute or not.
        /// </summary>
        /// <param name="type">
        ///     The type to test for the attribute.
        /// </param>
        /// <param name="attributeType">
        ///     The type of attribute that the type should be marked with.
        /// </param>
        /// <param name="inherit">
        ///     Indicates if the attribute could be inherited (true) or whether it should be marked
        ///     explicitly on the class.
        /// </param>
        /// <returns>
        ///     True, if the <paramref name="type"/> is marked with the <paramref name="attributeType"/>.
        ///     Otherwise false.
        /// </returns>
        public static bool IsMarkedWithAttribute(this Type type, Type attributeType, bool inherit)
        {
            if (attributeType == null)
                throw new ArgumentNullException(nameof(attributeType), $@"The parameter, {nameof(attributeType)}, cannot be null.");

            if (!attributeType.IsSubclassOf(typeof(Attribute)))
                throw new ArgumentException($@"The parameter, {nameof(attributeType)}, is not an attribute.");

            return type.GetCustomAttribute(attributeType, inherit) != null;
        }

        /// <summary>
        ///     This extension method confirms if a class is marked with the specified attribute. Ignores inherited attributes.
        /// </summary>
        /// <param name="classType">
        ///     The class to test for attribute markation.
        /// </param>
        /// <param name="attributeType">
        ///     The type of attribute to test the class for being marked with.
        /// </param>
        /// <returns>
        ///     True if the class is marked with the specified attribute, otherwise false.
        /// </returns>
        public static bool IsMarkedWithAttribute(this Type classType, Type attributeType) =>
            IsMarkedWithAttribute(classType, attributeType, false);
    }
}
