namespace JWTAPI.Core.Services.Communication
{
    public class NetworkResponse : BaseResponse
    {
        public NetworkResponse(bool success, string message) : base(success, message)
        {
        }
    }
}
