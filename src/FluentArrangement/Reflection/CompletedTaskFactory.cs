using System.Threading.Tasks;

namespace FluentArrangement
{
    internal class CompletedTaskFactory : IFactory
    {
        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if (request.Type == (typeof(Task)))
            return new CreatedObjectResponse(Task.CompletedTask);

            var taskType = request.Type.GetGenericSubclass(typeof(Task<>));

            if(taskType == null)
                return new NotCreatedResponse();

            var innerType = taskType.GenericTypeArguments[0];

            var createdObject = scope.CreateObjectFromType(innerType);
            
            var taskResult = typeof(Task).GetMethod(nameof(Task.FromResult))!
                                         .Invoke(null, new object?[] { createdObject });

            return new CreatedObjectResponse(taskResult);
        }
    }
}