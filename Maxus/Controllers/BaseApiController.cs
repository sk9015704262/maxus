using Microsoft.AspNetCore.Mvc;

namespace Maxus.Controllers
{

    [ApiController]
   
        public class BaseApiController : ControllerBase
        {
            protected int CurrentUserId
            {
                get
                {
                    if (int.TryParse(User.Identity.Name, out int userId))
                    {
                        return userId;
                    }

                    throw new UnauthorizedAccessException("Invalid user ID.");
                }
            }
        }
    
}
