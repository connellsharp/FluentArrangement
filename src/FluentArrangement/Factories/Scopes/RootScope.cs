namespace FluentArrangement
{
    /// <summary>
    /// A scope that cannot resolve any objects and just throws a <see cref="NotCreatedException" />.
    /// Used as the top-level scope.
    /// </summary>
    internal class RootScope : IScope
    {
        public object? CreateObject(ICreateRequest request, IScope scope)
        {
            throw request.GetNotCreatedException();
        }
    }
}