using System;

namespace FluentArrangement
{
    internal class RandomNumberFactory : IFactory
    {
        private Random _random;

        public RandomNumberFactory()
        {
            _random = new Random();
        }

        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            var randomValue = GenerateRandomValue(request.Type);

            if(randomValue == null)
                return new NotCreatedResponse();

            return new CreatedObjectResponse(randomValue);
        }

        private object? GenerateRandomValue(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                    byte[] bytes = new byte[1];
                    _random.NextBytes(bytes);
                    return bytes[0];
                    
                case TypeCode.Int16:
                case TypeCode.UInt16:
                    return (short)_random.NextDouble(short.MinValue, short.MaxValue);

                case TypeCode.Single:
                    return (float)_random.NextDouble(float.MinValue, float.MaxValue);

                case TypeCode.Double:
                    return _random.NextDouble(float.MinValue, float.MaxValue);

                case TypeCode.Decimal:
                    return _random.NextDecimal();

                case TypeCode.Int32:
                case TypeCode.UInt32:
                    return _random.NextInt32();

                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return _random.NextInt64();

                default:
                    return null;
            }
        }
    }
}