using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Owin;

namespace Pontinho.Domain.Services
{

    public class CurrentUserService
    {
        //public HttpContext Context => HttpContext.Current.GetOwinContext();
        
        //public ClaimsPrincipal CurrentPrincipal => Context.Authentication.User;
    }
}
