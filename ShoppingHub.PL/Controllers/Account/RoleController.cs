using examole.Serviese;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingHub.PL.Controllers.Account
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string model)
        {
            if (model is not null)
                return View(model);

            var result = await _roleService.CreateAsync(model);
            if (result)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Role already exists or failed to create.");
            return View(model);
        }
    }
}
