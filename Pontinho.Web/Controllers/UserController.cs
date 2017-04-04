using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pontinho.Domain;
using Pontinho.Dto;
using Pontinho.Logic;
using Pontinho.Logic.Interfaces;

namespace Pontinho.Web.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly CurrentUserService _currentUserService;
        private readonly IUserLogic _userLogic;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(CurrentUserService currentUserService, IUserLogic userLogic, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _currentUserService = currentUserService;
            _userLogic = userLogic;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserDto model)//string email, string password, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return Ok();
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("logoff")]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok();
                }
                AddErrors(result);
            }
            return BadRequest(ModelState);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", $"{error.Code}: {error.Description}");
            }
        }

        [HttpGet("profile")]
        public UserDto Profile()
        {
            return _userLogic.Get(_currentUserService.CurrentUser, _currentUserService.CurrentUser.UserName);
        }

        [HttpPost("profile")]
        public UserDto UpdateProfile([FromBody]UserDto model)
        {
            return _userLogic.Update(_currentUserService.CurrentUser, model);
        }
    }
}