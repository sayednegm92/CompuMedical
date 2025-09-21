namespace CompuMedical.Application.Helper;

public class ResponseHandler : IResponseHandler
{
    public GeneralResponse ErrorMessage(string Message = null)
    {
        return new GeneralResponse()
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest,
            Succeeded = false,
            Message = Message ?? "Done Successfully"
        };
    }

    public GeneralResponse ShowMessage(string Message = null)
    {
        return new GeneralResponse()
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest,
            Message = Message ?? "Done Successfully"
        };
    }

    public GeneralResponse SuccessMeta(object entity = null, object Meta = null, string Message = null)
    {
        return new GeneralResponse()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = Message ?? "Done Successfully"
        };
    }
    public GeneralResponse Success(object entity = null, string Message = null)
    {
        return new GeneralResponse()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = Message ?? "Done Successfully"
        };
    }
    public GeneralResponse ReturnUserData(object entity = null, string Token = null, string Message = null)
    {
        return new GeneralResponse()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Token = Token,
            Message = Message ?? "Done Successfully"
        };
    }
    public GeneralResponse SuccessMessage(string Message = null)
    {
        return new GeneralResponse()
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = Message ?? "Done Successfully"
        };
    }
}
