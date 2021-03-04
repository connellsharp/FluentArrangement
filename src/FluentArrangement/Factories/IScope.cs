namespace FluentArrangement
{
    public interface IScope
    {
        ICreateResponse Create(ICreateRequest request);
    }
}