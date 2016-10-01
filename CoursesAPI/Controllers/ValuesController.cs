using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoursesAPI.Controllers
{
    [Route("api/courses")]
    //Authorize
    //stilla af claim
    //eh notandi ma gera x
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        //For everyone
        public String GetCourses()
        {
            //Return all the courses
            return "Raudrofusafi";
        }

        // GET api/values/5
        //Authorize(Policy = "TeachersOnly","StudentsOnly")
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        //(Authorize(Policy = "TeachersOnly"))
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
