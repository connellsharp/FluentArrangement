using System;

namespace FluentArrangement
{
    public class CtorAndPropsFactory : IFactory
    {
        public ICreateResponse Create(ICreateRequest request)
        {
            if(!IsDtoModelType(request.Type))
                return new NotCreatedResponse();

            var instance = Activator.CreateInstance(request.Type);

            foreach(var property in request.Type.GetProperties())
            {
                if(!property.CanWrite)
                    continue;

                var response = request.Scope.Create(new CreatePropertyRequest(property, request.Scope));

                if(!response.HasCreated)
                    continue;

                property.SetValue(instance, response.CreatedObject);
            }

            return new CreatedObjectResponse(instance);
        }

        private bool IsDtoModelType(Type type)
        {
            return !type.IsAbstract && !type.IsPrimitive && type != typeof(string);
        }
    }
}