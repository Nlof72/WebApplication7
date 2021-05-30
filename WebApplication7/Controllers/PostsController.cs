using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication7.Data;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Post()
        {
            var posts = _context.Posts.Include(f => f.Category);
            return View(await posts.ToListAsync());
        }

        public IActionResult Create()
        {
            var categories = new SelectList(_context.ForumCategories, "Id", "Name");
            ViewBag.Categorys = categories;
            return View(new Post());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post model)
        {
            if (!ModelState.IsValid) return View(model);
            await _context.Posts.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Post));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts
                .Include(f => f.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (post == null) return NotFound();

            return View(post);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();

            var categories = new SelectList(_context.ForumCategories, "Id", "Name", post.CategoryId);
            ViewBag.Categorys = categories;
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Post model)
        {
            if (id == null) return NotFound();

            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();
            if (!ModelState.IsValid) return View(model);

            post.Title = model.Title;
            post.Text = model.Text;
            post.Category = model.Category;
            post.CategoryId = model.CategoryId;
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Post));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return NotFound();

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Post));
        }
    }
}
