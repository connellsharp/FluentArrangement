using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentArrangement
{
    internal static class TypeExtensions
    {
        public static bool InheritsGenericInterface(this Type type, Type genericInterfaceDefinition)
        {
            return GetGenericInterface(type, genericInterfaceDefinition) != null;
        }

        public static bool IsOfGenericTypeDefinition(this Type type, Type genericInterfaceDefinition)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == genericInterfaceDefinition;
        }
        
        public static Type? GetGenericInterface(this Type type, Type genericInterfaceDefinition)
        {
            if (type.IsOfGenericTypeDefinition(genericInterfaceDefinition))
                return type;

            return type.GetInterfaces().FirstOrDefault(t => t.IsOfGenericTypeDefinition(genericInterfaceDefinition));
        }
        
        public static bool IsSubclassOfGeneric(this Type type, Type genericSubclass)
        {
            return GetGenericSubclass(type, genericSubclass) != null;
        }

        public static Type? GetGenericSubclass(this Type type, Type genericSubclass)
        {
            Type? currentType = type;

            while (currentType != null && currentType != typeof(object))
            {
                Type genericDefinition = currentType.IsGenericType ? currentType.GetGenericTypeDefinition() : currentType;

                if (genericDefinition == genericSubclass)
                    return currentType;

                currentType = currentType.BaseType;
            }

            return null;
        }
    }
}