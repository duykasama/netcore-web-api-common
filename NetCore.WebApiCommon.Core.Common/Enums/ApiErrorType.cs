namespace NetCore.WebApiCommon.Core.Common.Enums;

public enum ApiErrorType
{
    ClientError = 400,
    BusinessError = 409,
    InternalServerError = 500,
}