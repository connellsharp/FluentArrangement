namespace FluentArrangement
{
    internal static class ScopeCreateExtensions
    {
        internal static T CreateObject<T>(this IScope scope, ICreateRequestWithException request)
        {
            var response = scope.Create(request);

            if(!response.HasCreated)
                throw request.GetNotCreatedException();

            return (T)response.CreatedObject;
        }

        internal static object CreateObject(this IScope scope, ICreateRequestWithException request)
        {
            return scope.CreateObject<object>(request);
        }

        internal static T CreateObjectFromType<T>(this IScope scope)
        {
            return scope.CreateObject<T>(new CreateTypeRequest(typeof(T)));
        }
    }
}