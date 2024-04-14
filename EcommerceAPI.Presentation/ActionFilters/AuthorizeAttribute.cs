using EcommerceAPI.Entity.Enums;
using EcommerceAPI.Entity.Models;
using EcommerceAPI.Interface.IService;
using EcommerceAPI.Interface.IService.IEntityService;
using EcommerceAPI.Service.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static EcommerceAPI.Entity.AppConstants.AppConstant;

namespace EcommerceAPI.Presentation.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly Role _requiredRole;

        public AuthorizeAttribute(Role requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isError = false;
            var _service = context.HttpContext.RequestServices.GetService<IServiceManager>();
            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrWhiteSpace(token))
                isError = true;
            if (isError)
            {
                SetUnauthorizedResult(context);
            }
            else
            {
                var user = _service.AuthenticationService.ValidateJwtToken(token, true);
                if (user.Role != _requiredRole.ToString())
                    SetUnauthorizedResult(context);
                return;
            }
        }
        private void SetUnauthorizedResult(AuthorizationFilterContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Result = new UnauthorizedObjectResult(Error.DS054)
            {
                Value = new
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Status = "Authorization Failed",
                    Message = Error.DS054
                }
            };
        }
    }
}
