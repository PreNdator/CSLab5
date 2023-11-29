using CSDBapp;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Numerics;

namespace CSLab5.Controllers
{
    public class StatsController : Controller
    {

        public async Task<IActionResult> Edit(int id)
        {
            using (GameDb db = new GameDb())
            {
                return View(await db.PlayerStats.Include("Player").FirstOrDefaultAsync(s => s.Id == id));
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(PlayerStats playerStats)
        {
            if (ModelState.IsValid)
            {
                FileLoggerTS.GetInstance().LogMessage(playerStats.PlayerId.ToString());
                await CRUD<PlayerStats>.GetInstance().UpdateAsync(playerStats, playerStats.Id);

                return RedirectToAction("Index", "Player");
            }
            return View(playerStats);
        }
    }
}
