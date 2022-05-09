using FindingJob.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FindingJobDLL.Repository;
using FindingJobDLL.Entities;
using System.Text.Json;


namespace FindingJob.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private FindingJobContext _context = new FindingJobContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            JobRepository jobRepository = new(_context);
            IEnumerable<Job> jobs = jobRepository.GetAll();
            //IEnumerable<Job> jobs = jobRepository.FindJobBySkill("PHP");
            foreach (var job in jobs) {
            _context.Entry(job).Reference(j => j.City).Load();
            _context.Entry(job).Reference(j => j.JobTitle).Load();
            }
            return View(jobs);
        }

        public IActionResult Privacy()
        {
            IEnumerable<Job> jobs = _context.Jobs.Where(j => j.Skill.SkillId == 1);
            return View();
        }
        
        public IActionResult Skill()
        {
            //JobRepository jobRepository = new(_context);
            //IEnumerable<Job> jobs = jobRepository.FindJobBySkill("PHP");
            //foreach (var job in jobs)
            //{
            //    _context.Entry(job).Reference(j => j.City).Load();
            //    _context.Entry(job).Reference(j => j.JobTitle).Load();
            //}
            return View(/*jobs*/);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            JobRepository jobRepository = new(_context);
            Job job = jobRepository.GetById(id);
            _context.Entry(job).Reference(j => j.City).Load();
            _context.Entry(job).Reference(j => j.JobTitle).Load();
            return View(job);
        }

        public IActionResult HRPost()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
