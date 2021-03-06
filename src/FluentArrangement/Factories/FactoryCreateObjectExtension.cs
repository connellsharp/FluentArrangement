using System;

namespace FluentArrangement
{
    public static class FactoryCreateObjectExtension
    {
        public static T GetRequiredCreatedObject<T>(this ICreateResponse response)
        {
            if(response.HasCreated)
                return (T)response.CreatedObject;

            throw new NoFactoryFoundException();
        }

        public static object GetRequiredCreatedObject(this ICreateResponse response)
        {
            return response.GetRequiredCreatedObject<object>();
        }

        public static T Create<T>(this IScope scope)
        {
            return scope.Create(new CreateTypeRequest(typeof(T)))
                        .GetRequiredCreatedObject<T>();
        }
    }
}