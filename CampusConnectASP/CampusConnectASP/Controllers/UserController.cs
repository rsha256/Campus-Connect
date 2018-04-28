using CampusConnect.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CampusConnect.Controllers
{

    [Route("user")]
    public class UserController : Controller
    {
        public UserContext context;

        public UserController(UserContext c) =>
            context = c;

        //http://api.ironic.serivces/user/

        [HttpPost("add")]
        public IActionResult addUser()
        {
            string text = "";

            MemoryStream memstream = new MemoryStream();
            Request.Body.CopyTo(memstream);
            memstream.Position = 0;
            using (StreamReader reader = new StreamReader(memstream))
                text = reader.ReadToEnd();

            User u = JsonConvert.DeserializeObject<User>(text);
            u.time = DateTime.Now;

            User nu = context.Users.FirstOrDefault(us => us.name == u.name);
            if (nu != null)
                return Content("Username taken");

            context.Add(u);
            context.SaveChanges();

            return Content("Success");
        }

        [HttpPost("updateUser")]
        public IActionResult updateUser()
        {
            string text = "";

            MemoryStream memstream = new MemoryStream();
            Request.Body.CopyTo(memstream);
            memstream.Position = 0;
            using (StreamReader reader = new StreamReader(memstream))
                text = reader.ReadToEnd();

            User u = JsonConvert.DeserializeObject<User>(text);

            User nu = context.Users.FirstOrDefault(us => us.username == u.username);
            if (nu == null)
                return Content("Username not found");

            if (u.campus != null)
                nu.campus = u.campus;
            if (u.name != null)
                nu.name = u.name;
            if (u.tags != null)
                nu.tags = u.tags;

            nu.time = DateTime.Now;

            context.Update(nu);
            context.SaveChanges();
            
            return Content("User updated");
        }

        [HttpGet("get")]
        public IActionResult getUser()
        {
            string user = Request.Query["user"];

            User nu = context.Users.FirstOrDefault(us => us.username == user);
            if (nu == null)
                return Content("Username not found");

            return Content(JsonConvert.SerializeObject(nu));
        }

        [HttpGet("getTags")]
        public IActionResult getTags()
        {
            string user = Request.Query["user"];

            User nu = context.Users.FirstOrDefault(us => us.username == user);
            if (nu == null)
                return Content("Username not found");
           
            string tList = Request.Query["tags"];
            string[] filterTags = tList.Split(",");

            foreach (User usr in context.Users.Where(us => us.username != user))
            {
                string[] tags = usr.tags.Split(",");
                foreach (string tag in tags)
                {
                    if (filterTags.Contains(tag))
                        return Content("User '" + usr.name + "' returned '" + tag + "'");
                }
            }

            return Content("No users found.");
        }
    }
}