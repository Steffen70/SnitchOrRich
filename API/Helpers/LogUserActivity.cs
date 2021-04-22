using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using API.Data;
using API.Data.Repositories;
using API.Extensions;

namespace API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var unitOfWork = resultContext.HttpContext.RequestServices.GetService<UnitOfWork>();
            var user = await unitOfWork.GetRepo<UserRepository>().GetUserByIdAsync(resultContext.HttpContext.User.GetUserId());

            user.LastActive = DateTime.UtcNow;
            await unitOfWork.Complete();
        }
    }
}