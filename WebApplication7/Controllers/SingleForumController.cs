using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using WebApplication7.Data;
using WebApplication7.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Controllers
{
    public class SingleForumController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> UserManager;



        public SingleForumController(ApplicationDbContext context, UserManager<IdentityUser> manager)
        {
            _context = context;
            UserManager = manager;
        }
        public async Task<IActionResult> SingleForums()
        {
            var topics = _context.topicCreatorModels;
            //try{
                ViewBag.ReplCount = _context.Topics.CountAsync().Result;
            //}
            //catch
            //{
            //    ViewBag.ReplCount = 0;
            //}
            
            return View(await topics.ToListAsync());
        }

        public IActionResult Create()
        {
            return View(new TopicCreatorModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TopicCreatorViewModel model)
        {
            if (!ModelState.IsValid) return View();

            CultureInfo culture = new("en-US");
            TopicCreatorModel topic = new();
            topic.Title = model.Title;
            topic.Author = UserManager.GetUserName(User);
            topic.CreateDate = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss", culture);

            await _context.topicCreatorModels.AddAsync(topic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SingleForums));
        }
    }
}
