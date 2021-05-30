using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Data;

namespace WebApplication7.Controllers
{
    public class MockupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MockupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AllForums()
        {
            return View(await _context.ForumCategories.ToListAsync());
        }

        public IActionResult SingleForum()
        {
            return View();
        }

        public IActionResult SingleTopic()
        {
            return View();
        }
    }
}
