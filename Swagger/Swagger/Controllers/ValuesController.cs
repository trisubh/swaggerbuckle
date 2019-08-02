using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Swagger.Controllers
{
    //values controller
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(string id)
        {
            return id;
        }

        [HttpGet]
        [Route("{module}/{id}")]
        public string Get(string module, int id)
        {
            return "module : " + module +", id : " + id;
        }
        [HttpGet]
        [Route("uri")]
        public HttpResponseMessage url()
        {
            var path = System.Web.HttpContext.Current.Server.MapPath(@"~\\v1.json");
            FileStream fs = new FileStream(path,FileMode.Open,FileAccess.Read);
            StreamReader streamReader = new StreamReader(fs, Encoding.UTF8);
            string text = streamReader.ReadToEnd();
            if (!string.IsNullOrEmpty(text))
            {
                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(text, Encoding.UTF8, "application/json");
                fs.Close();
                fs.Dispose();
                streamReader.Close();
                streamReader.Dispose();
                return response;
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);

        }



        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
