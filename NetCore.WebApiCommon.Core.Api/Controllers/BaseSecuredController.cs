using Microsoft.AspNetCore.Authorization;

namespace NetCore.Architecture.Api.Controllers;

[Authorize]
public class BaseSecuredController : BaseController
{
}