using Castle.Components.DictionaryAdapter.Xml;
using CSDBapp;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Numerics;

namespace CSLab5.Controllers
{
    public class PlayerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (GameDb db = new GameDb())
            {
                return View(await db.Players.Include("Kingdom").Include("Stats").ToListAsync());
            }

        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Kingdoms = await CRUD<Kingdom>.GetInstance().GetAllAsync();
            ViewBag.Achievements = await CRUD<Achievement>.GetInstance().GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Player player, List<int> selectedAchievements)
        {
            ViewBag.Kingdoms = await CRUD<Kingdom>.GetInstance().GetAllAsync();
            ViewBag.Achievements = await CRUD<Achievement>.GetInstance().GetAllAsync();
            if (ModelState.IsValid)
            {
                await CRUD<Player>.GetInstance().AddAsync(player);
                CRUD<Achievement> crudAchieve = CRUD<Achievement>.GetInstance();
                using (GameDb db = new GameDb())
                {
                    var playerFromDb = db.Players.Include(p => p.Achievement).FirstOrDefault(p => p.Id == player.Id);

                    if (playerFromDb != null)
                    {
                        foreach (var achievementId in selectedAchievements)
                        {
                            var existingAchievement = db.Achievements.Local.FirstOrDefault(a => a.Id == achievementId);

                            var achievementToAdd = existingAchievement ?? await crudAchieve.GetByIdAsync(achievementId);

                            if (achievementToAdd != null && !playerFromDb.Achievement.Contains(achievementToAdd))
                            {
                                playerFromDb.Achievement.Add(achievementToAdd);
                            }
                        }
                        db.SaveChanges();
                    }
                }
                FileLoggerTS.GetInstance().LogMessage($"Create player {player.Username}");
                return RedirectToAction("Index");
            }
            return View(player);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            CRUD<Player> crud = CRUD<Player>.GetInstance();
            FileLoggerTS.GetInstance().LogMessage($"Delete player {(await crud.GetByIdAsync(id))?.Username}");
            await crud.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Kingdoms = await CRUD<Kingdom>.GetInstance().GetAllAsync();
            ViewBag.Achievements = await CRUD<Achievement>.GetInstance().GetAllAsync();
            using (GameDb db = new GameDb())
            {
                Player? player = await db.Players.Include("Achievement").FirstOrDefaultAsync(p => p.Id == id);
                if (player != null) return View(player);
                else return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Player player, List<int> selectedAchievements)
        {
            ViewBag.Kingdoms = await CRUD<Kingdom>.GetInstance().GetAllAsync();
            ViewBag.Achievements = await CRUD<Achievement>.GetInstance().GetAllAsync();
            if (ModelState.IsValid)
            {
                await CRUD<Player>.GetInstance().UpdateAsync(player, player.Id);
                CRUD<Achievement> crudAchieve = CRUD<Achievement>.GetInstance();
                using (GameDb db = new GameDb())
                {
                    var playerFromDb = db.Players.Include(p => p.Achievement).FirstOrDefault(p => p.Id == player.Id);

                    if (playerFromDb != null)
                    {
                        foreach (var achievement in playerFromDb.Achievement.ToList())
                        {
                            if (!selectedAchievements.Contains(achievement.Id))
                            {
                                playerFromDb.Achievement.Remove(achievement);
                            }
                        }

                        foreach (var achievementId in selectedAchievements)
                        {
                            var existingAchievement = db.Achievements.Local.FirstOrDefault(a => a.Id == achievementId);

                            var achievementToAdd = existingAchievement ?? await crudAchieve.GetByIdAsync(achievementId);

                            if (achievementToAdd != null && !playerFromDb.Achievement.Contains(achievementToAdd))
                            {
                                playerFromDb.Achievement.Add(achievementToAdd);
                            }
                        }

                        db.SaveChanges();
                    }
                }
                FileLoggerTS.GetInstance().LogMessage($"Edit player {player.Username}");
                return RedirectToAction("Index");
            }
            return View(player);
        }
    }
}
