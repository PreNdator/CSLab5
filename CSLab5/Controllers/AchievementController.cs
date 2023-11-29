using CSDBapp;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace CSLab5.Controllers
{
    public class AchievementController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (GameDb db = new GameDb())
            {
                return View(await db.Achievements.Include(k => k.PlayerAchievement).ToListAsync());
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Achievement achievement)
        {
            if (ModelState.IsValid)
            {
                await CRUD<Achievement>.GetInstance().AddAsync(achievement);;
                
                FileLoggerTS.GetInstance().LogMessage($"Create achievement {achievement.Name}");
                return RedirectToAction("Index");
            }
            return View(achievement);
        }
        public async Task<IActionResult> Edit(int id)
        {

            Achievement? ach = await CRUD<Achievement>.GetInstance().GetByIdAsync(id);
            if (ach != null)
            {
                return View(ach);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Achievement achievement)
        {
            if (ModelState.IsValid)
            {
                await CRUD<Achievement>.GetInstance().UpdateAsync(achievement, achievement.Id);

                FileLoggerTS.GetInstance().LogMessage($"Edit achievement {achievement.Name}");
                return RedirectToAction("Index");
            }
            return View(achievement);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            CRUD<Achievement> crud = CRUD<Achievement>.GetInstance();
            FileLoggerTS.GetInstance().LogMessage($"Delete achievement {(await crud.GetByIdAsync(id))?.Name}");
            await crud.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
