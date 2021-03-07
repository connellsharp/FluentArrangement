using System;

namespace FluentArrangement
{
    internal class RandomBooleanFactory : IFactory
    {
        private Random _random;

        public RandomBooleanFactory()
        {
            _random = new Random();
        }

        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if(request.Type != typeof(bool))
                return new NotCreatedResponse();

            var randomBool = _random.Next(2) == 1;
            return new CreatedObjectResponse(randomBool);
        }
    }
}