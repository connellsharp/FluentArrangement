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
    }
}