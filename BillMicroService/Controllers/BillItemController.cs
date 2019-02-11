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
    public class BillItemController : ApiController
    {
        [Route("shopcart/BillItem/"), HttpGet]
        public List<BillItem> GetBillItems()
        {
            return BillItemDB.GetAll();
        }

        [Route("shopcart/BillItem/{id}"), HttpGet]
        public BillItem GetBillItemById(int id)
        {
            return BillItemDB.GetById(id);
        }

        [Route("shopcart/BillItemByBillId/{id}"), HttpGet]
        public List<BillItem> GetBillItemByBillId(int id)
        {
            return BillItemDB.GetAllByBillId(id);
        }

        [Route("shopcart/BillItem/"), HttpPost]
        public IHttpActionResult AddBillItem([FromBody] BillItem item)
        {
            if (item == null)
                return StatusCode(HttpStatusCode.BadRequest);

            BillItemDB.Add(item);

            return StatusCode(HttpStatusCode.OK);
        }


        [Route("shopcart/BillItem/"), HttpPut]
        public IHttpActionResult UpdateBillItem([FromBody] BillItem item)
        {
            if (item == null)
                return StatusCode(HttpStatusCode.BadRequest);

            BillItemDB.Update(item);

            return StatusCode(HttpStatusCode.OK);
        }

        [Route("shopcart/BillItem/{id}"), HttpDelete]
        public void DeleteBill(int id)
        {
            BillItemDB.Delete(id);
        }
    }
}
