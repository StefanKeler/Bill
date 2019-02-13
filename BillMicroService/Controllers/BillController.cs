using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BillMicroService.DataAccess;
using BillMicroService.Models;

namespace BillMicroService.Controllers
{
    public class BillController : ApiController
    {
        [Route("shopcart/Bill/"), HttpGet]
        public List<Bill> GetBilles()
        {
            return BillDB.GetAll();
        }

        [Route("shopcart/Bill/{id}"), HttpGet]
        public Bill GetBillById(int id)
        {
            return BillDB.GetById(id);//s
        }


        [Route("shopcart/Bill/"), HttpPost]
        public IHttpActionResult AddBill([FromBody] Bill item)
        {
            if (item == null)
                return StatusCode(HttpStatusCode.BadRequest);

            BillDB.Add(item);

            return StatusCode(HttpStatusCode.OK);
        }


        [Route("shopcart/Bill/"), HttpPut]
        public IHttpActionResult UpdateBill([FromBody] Bill item)
        {
            if (item == null)
                return StatusCode(HttpStatusCode.BadRequest);

            BillDB.Update(item);

            return StatusCode(HttpStatusCode.OK);
        }

        [Route("shopcart/Bill/{id}"), HttpDelete]
        public void DeleteBill(int id)
        {
            BillDB.Delete(id);
        }
    }
}
