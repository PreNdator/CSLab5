using CSDBapp;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Security.Permissions;

namespace CSLab5.Controllers
{
    public class KingdomController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (GameDb db = new GameDb()) {
                return View(await db.Kingdoms.Include(k => k.Players).ToListAsync());
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Kingdom kingdom)
        {
            if (ModelState.IsValid)
            {
                await CRUD<Kingdom>.GetInstance().AddAsync(kingdom); ;

                FileLoggerTS.GetInstance().LogMessage($"Create kingdom {kingdom.Name}");
                return RedirectToAction("Index");
            }
            return View(kingdom);
        }
        public async Task<IActionResult> Edit(int id)
        {

            Kingdom? king = await CRUD<Kingdom>.GetInstance().GetByIdAsync(id);
            if (king != null)
            {
                return View(king);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Kingdom kingdom)
        {
            if (ModelState.IsValid)
            {
                await CRUD<Kingdom>.GetInstance().UpdateAsync(kingdom, kingdom.Id);

                FileLoggerTS.GetInstance().LogMessage($"Edit kingdom {kingdom.Name}");
                return RedirectToAction("Index");
            }
            return View(kingdom);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            CRUD<Kingdom> crud = CRUD<Kingdom>.GetInstance();
            FileLoggerTS.GetInstance().LogMessage($"Delete kingdom {(await crud.GetByIdAsync(id))?.Name}");
            await crud.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
