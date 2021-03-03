namespace FluentArrangement
{
    public interface IFactory
    {
        ICreateResponse Create(CreateRequest request);
    }
}