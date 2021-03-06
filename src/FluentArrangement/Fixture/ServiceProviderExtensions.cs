using System;

namespace FluentArrangement
{
    internal static class ServiceProviderExtensions
    {
        internal static T GetService<T>(this IServiceProvider serviceProvider)
            => (T)serviceProvider.GetService(typeof(T));
    }
}