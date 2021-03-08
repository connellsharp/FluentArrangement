using System;

namespace FluentArrangement
{
    internal class NullableNotNullFactory : IFactory
    {
        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if (!request.Type.IsOfGenericTypeDefinition(typeof(Nullable<>)))
                return new NotCreatedResponse();

            var innerType = Nullable.GetUnderlyingType(request.Type);

            var createdObject = scope.CreateObjectFromType(innerType);

            return new CreatedObjectResponse(createdObject);
        }
    }
}