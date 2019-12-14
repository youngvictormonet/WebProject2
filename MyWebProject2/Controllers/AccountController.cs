using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.ViewModel;
using WebShop.WebShop.Data.Models;
using Microsoft.AspNetCore.Identity;
using WebShop.Data.Models;
using WebShop.Data;
using MyWebProject2.ViewModel;
namespace MyWebProject2.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<ShopUser> _userManager;
        private readonly SignInManager<ShopUser> _signInManager;
        private readonly WebShopContext _context;

        public AccountController(UserManager<ShopUser> userManager, SignInManager<ShopUser> signInManager, WebShopContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            return View(new RegisterVM { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            ShopUser user = new ShopUser { Email = model.Email, UserName = model.UserName };

            if (user.UserName[user.UserName.IndexOf(' ') + 1] == ' ' ||
                user.UserName[0] == ' '
                )
            { ModelState.AddModelError(string.Empty, "Invalid name."); }
            else
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, "user");
                    _context.Carts.Add(new Cart { ShopUser = user });
                    _context.SaveChanges();
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }
                else
                {

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginVM { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login and (or) Password");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }
    }
}

