using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Tasks.Presentation.WebApi.Extensions.Services;
using System.Linq;
using Tasks.Core.Application.Interfaces.Contracts;

namespace Tasks.Presentation.WebApi.Extensions.Middlewares
{
    public class UserCachingMiddlewares
    {
        private readonly RequestDelegate next;

        public UserCachingMiddlewares(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IActiveObjectsService usersCaching)
        {
            //HandleUserCache(context, usersCaching);
            await Task.Run(() => HandleUserCache(context, usersCaching));
            await next(context);
        }

        private void HandleUserCache(HttpContext context, IActiveObjectsService usersCaching)
        {
            //string userName = context.User?.FindFirstValue("UserName");
            //usersCaching.AddOrProlong(userName);

            string[] permissions = context.User?.Claims?.Where(x => x.Type == "resources").Select(x => x.Value).ToArray();
            if (permissions.Contains("ROLE.SUPERVAISER"))
            {
                string userName = context.User?.FindFirstValue("UserName");
                usersCaching.AddOrProlong(userName);
            }
        }
    }
}
