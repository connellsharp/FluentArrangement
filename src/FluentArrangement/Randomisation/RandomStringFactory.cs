using System;

namespace FluentArrangement
{
    internal class RandomStringFactory : IFactory
    {
        private Random _random;

        public RandomStringFactory()
        {
            _random = new Random();
        }

        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if(request.Type != typeof(string))
                return new NotCreatedResponse();

            var randomString = Guid.NewGuid().ToString();
            return new CreatedObjectResponse(randomString);
        }
    }
}