using Dworks.Application.Responses.ApiResponse;

namespace Dworks.Application.Util
{
    public class ResponseHandlerUtil<T> : ResponseHandler<T> where T : class
    {
        public void setData(T response)
        {
            Response.Data = response;
        }

        public ApiResponse<T> getApiResponse()
        {
            return Response;
        }
    }
}
