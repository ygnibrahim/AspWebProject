using AspWebProject.DTOs;
using AspWebProject.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspWebProject.Controllers
{
    public class ProjectionController : Controller
    {
        private readonly IMapper _mapper;

        public ProjectionController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(EventDateDto eventDateDto)
        {
            EventDate eventDate= _mapper.Map<EventDate>(eventDateDto);


            ViewBag.Date= eventDate.Date.ToShortDateString();   
            return View();
        }
    }
}
