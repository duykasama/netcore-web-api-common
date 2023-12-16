using Microsoft.AspNetCore.Authorization;

namespace NetCore.WebApiCommon.Api.Controllers;

[Authorize]
public class BaseSecuredController : BaseController
{
}