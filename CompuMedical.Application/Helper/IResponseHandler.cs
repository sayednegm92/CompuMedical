namespace CompuMedical.Application.Helper;

public interface IResponseHandler
{
    public GeneralResponse SuccessMeta(object entity = null, object Meta = null, string Message = null);
    public GeneralResponse Success(object entity = null, string Message = null);
    public GeneralResponse ReturnUserData(object entity = null, string Token = null, string Message = null);
    public GeneralResponse SuccessMessage(string Message = null);
    public GeneralResponse ShowMessage(string Message = null);
    GeneralResponse ErrorMessage(string Message = null);
}
