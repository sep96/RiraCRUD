using Grpc.Core.Interceptors;
using Grpc.Core;

namespace RiraCRUD.Api.Interceptors
{
    public class GlobalExceptionInterceptor : Interceptor
    {
        private readonly ILogger<GlobalExceptionInterceptor> _logger;

        public GlobalExceptionInterceptor(ILogger<GlobalExceptionInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred during gRPC call");

                // Convert to appropriate gRPC status
                var status = ex switch
                {
                    ValidationException => Status.DefaultCancelled,
                    UnauthorizedAccessException => Status.DefaultUnavailable,
                    KeyNotFoundException => Status.DefaultNotFound,
                    _ => Status.DefaultInternal
                };

                // Throw gRPC status exception
                throw new RpcException(status, ex.Message);
            }
        }
    }
}
