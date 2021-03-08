using System.Threading;

namespace FluentArrangement
{
    internal class DefaultCancellationTokenFactory : IFactory
    {
        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if (request.Type != typeof(CancellationToken))
                return new NotCreatedResponse();

            return new CreatedObjectResponse(CancellationToken.None);
        }
    }
}