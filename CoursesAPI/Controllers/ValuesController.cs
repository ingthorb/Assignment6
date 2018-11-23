using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CoursesAPI.Controllers
{
    [Route("api/courses")]
    [Authorize]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        public String GetCourses()
        {
            string joke = 
            "Q: Why do Java programmers have to wear glasses?" +
            "A: Because they don't C#";
            // so bad jokes 
            return joke;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize(Policy = "TeacherORStudent" )]
        public string Get(int id)
        {
            string joke = 
            "Q:Why can't a blonde dial 911? " +
            "A: She can't find the eleven.";
            return joke;
        }

        // POST api/values
        [HttpPost]
        [Authorize(Policy = "TeachersOnly")]
        public string Post()
        {
            string lol =  "Raudrofusafi"; 
            return lol;
        }

    }
}
