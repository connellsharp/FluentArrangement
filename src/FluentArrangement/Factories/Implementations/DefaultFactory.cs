using System;

namespace FluentArrangement
{
    public class DefaultFactory : IFactory
    {
        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            var value = request.Type.IsValueType ? Activator.CreateInstance(request.Type) : null;
            return new CreatedObjectResponse(value);
        }
    }
}