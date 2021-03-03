namespace FluentArrangement
{
    public interface IFactory
    {
        ICreateResponse Create(ICreateRequest request);
    }
}