using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Pontinho.Domain;
using Pontinho.Logic.Interfaces;

namespace Pontinho.Logic
{
    public class CurrentUserService
    {
        private readonly IUserLogic _userLogic;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IUserLogic userLogic)
        {
            _httpContextAccessor = httpContextAccessor;
            _userLogic = userLogic;
        }

        public HttpContext Context => _httpContextAccessor.HttpContext;

        public ClaimsPrincipal CurrentPrincipal => Context.User;

        public ApplicationUser CurrentUser => _userLogic.GetEntity(CurrentPrincipal.Identity.Name);
    }
}
