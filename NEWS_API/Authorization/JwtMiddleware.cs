namespace News_API.Authorization;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using News_API.Helpers;
using News_API.Interfaces;
using Microsoft.AspNetCore.Mvc.Abstractions;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IRegisterRepository userService, IJwtUtils jwtUtils)
    {
       var endpoint = context.GetEndpoint();

        if (endpoint == null || endpoint.Metadata.Any(x => x is AllowAnonymousAttribute))
        {
        // Endpoint is not decorated with [Authorize] or there is no endpoint selected
        // Just call the next middleware in the pipeline
        await _next(context);
        return;
        }

        //var actionDescriptor = context.GetRouteData().Values["actionDescriptor"] as ActionDescriptor;
        //var actionMethodInfo = actionDescriptor?.GetMethodInfo();
        //if (actionMethodInfo != null && Attribute.IsDefined(actionMethodInfo, typeof(AllowAnonymousAttribute)))
        //{
        //    await _next(context);
        //    return;
        //}



        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateJwtToken(token);

        if (userId == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid or No Token");
            return;
        }

        context.Items["User"] = userService.GetById(userId.Value);
        await _next(context);
    }
}