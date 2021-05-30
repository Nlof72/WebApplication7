using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class ForumCategorysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForumCategorysController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Forum()
        {
            return View(await _context.ForumCategories.ToListAsync());
        }

        public IActionResult Create()
        {
            return View(new ForumCategory());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ForumCategory model)
        {
            if (!ModelState.IsValid) return View(model);
            await _context.ForumCategories.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Forum));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var forumCategory = await _context.ForumCategories.FindAsync(id);
            if (forumCategory == null) return NotFound();

            return View(forumCategory);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var forumCategory = await _context.ForumCategories.FindAsync(id);
            if (forumCategory == null) return NotFound();

            return View(forumCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, ForumCategory model)
        {
            if (id == null) return NotFound();
            var forumCategory = await _context.ForumCategories.FindAsync(id);
            if (forumCategory == null) return NotFound();

            if (!ModelState.IsValid) return View(model);
            forumCategory.Name = model.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Forum));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var forumCategory = await _context.ForumCategories.FindAsync(id);
            if (forumCategory == null) return NotFound();

            _context.ForumCategories.Remove(forumCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Forum));
        }
    }
}
