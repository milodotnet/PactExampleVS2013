using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TeamB.Producer.Controllers
{
    public class SomethingsController : ApiController
    {

        public Something Get(string id)
        {
            return new Something { Id = "tester", FirstName = "Totally", LastName = "Awesome"};
        }
    }
}
