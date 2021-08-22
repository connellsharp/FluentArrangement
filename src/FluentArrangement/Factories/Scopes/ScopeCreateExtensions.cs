using System;

namespace FluentArrangement
{
    internal static class ScopeCreateExtensions
    {
        internal static object? CreateObject(this IScope scope, ICreateRequest request)
        {
            var response = scope.Create(request, scope);

            if(!response.HasCreated)
                throw request.GetNotCreatedException();

            return response.CreatedObject;
        }

        internal static object? CreateObject<T>(this IScope scope, ICreateRequest request)
            => (T?)scope.CreateObject(request);

        internal static object? CreateObjectFromType(this IScope scope, Type type)
            => scope.CreateObject(new CreateTypeRequest(type));

        internal static T? CreateObjectFromType<T>(this IScope scope)
            => (T?)scope.CreateObjectFromType(typeof(T));
    }
}