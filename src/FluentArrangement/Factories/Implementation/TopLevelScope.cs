using System;

namespace FluentArrangement
{
    internal class TopLevelScope : IScope
    {
        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if(request.Type == typeof(void))
                return new CreatedVoidResponse();

            return new NotCreatedResponse();
        }
    }
}