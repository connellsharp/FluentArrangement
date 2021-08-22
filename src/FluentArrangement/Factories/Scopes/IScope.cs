namespace FluentArrangement
{
    public interface IScope
    {
        object? CreateObject(ICreateRequest request, IScope scope);
        
        RequestCollection Requests { get; }
    }
}