using System;

namespace FluentArrangement
{
    internal static class ScopeCreateExtensions
    {
        internal static object? CreateObject(this IScope scope, ICreateRequest request)
            => scope.CreateObject(request, scope);

        internal static T? CreateObject<T>(this IScope scope, ICreateRequest request)
            => (T?)scope.CreateObject(request);

        internal static object? CreateObjectFromType(this IScope scope, Type type)
            => scope.CreateObject(new CreateTypeRequest(type));

        internal static T? CreateObjectFromType<T>(this IScope scope)
            => (T?)scope.CreateObjectFromType(typeof(T));
    }
}