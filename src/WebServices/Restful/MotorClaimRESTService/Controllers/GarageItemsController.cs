using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MotorClaimRESTService.Attributes;
using MotorClaimRESTService.Models;
using MotorClaimRESTService.Services;

namespace MotorClaimRESTService.Controllers
{
    public class GarageItemsController : BaseApiController
    {
        static readonly IGarageService garageService = new GarageService(new GarageRepository());

        [HttpGet]
        [BasicAuthentication(RequireSsl = false)]
        public HttpResponseMessage Get()
        {
            return base.BuildSuccessResult(HttpStatusCode.OK, garageService.GetData());
        }

        //[HttpPost]
        //[BasicAuthentication(RequireSsl = false)]
        //public HttpResponseMessage Create(GarageItem item)
        //{
        //    try
        //    {
        //        if (item == null ||
        //            string.IsNullOrWhiteSpace(item.Name) ||
        //            string.IsNullOrWhiteSpace(item.Notes))
        //        {
        //            return base.BuildErrorResult(HttpStatusCode.BadRequest, ErrorCode.GarageItemNameAndNotesRequired.ToString());
        //        }

        //        // Determine if the ID already exists
        //        var itemExists = garageService.DoesItemExist(item.ID);
        //        if (itemExists)
        //        {
        //            return base.BuildErrorResult(HttpStatusCode.Conflict, ErrorCode.GarageItemIDInUse.ToString());
        //        }
        //        garageService.InsertData(item);
        //    }
        //    catch (Exception)
        //    {
        //        return base.BuildErrorResult(HttpStatusCode.BadRequest, ErrorCode.CouldNotCreateItem.ToString());
        //    }

        //    return base.BuildSuccessResult(HttpStatusCode.Created);
        //}

        //[HttpPut]
        //[BasicAuthentication(RequireSsl = false)]
        //public HttpResponseMessage Edit(GarageItem item)
        //{
        //    try
        //    {
        //        if (item == null ||
        //            string.IsNullOrWhiteSpace(item.Name) ||
        //            string.IsNullOrWhiteSpace(item.Notes))
        //        {
        //            return base.BuildErrorResult(HttpStatusCode.BadRequest, ErrorCode.GarageItemNameAndNotesRequired.ToString());
        //        }

        //        var todoItem = garageService.Find(item.ID);
        //        if (todoItem != null)
        //        {
        //            garageService.UpdateData(item);
        //        }
        //        else
        //        {
        //            return base.BuildErrorResult(HttpStatusCode.NotFound, ErrorCode.RecordNotFound.ToString());
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return base.BuildErrorResult(HttpStatusCode.BadRequest, ErrorCode.CouldNotUpdateItem.ToString());
        //    }

        //    return base.BuildSuccessResult(HttpStatusCode.NoContent);
        //}

        //[HttpDelete]
        //[BasicAuthentication(RequireSsl = false)]
        //public HttpResponseMessage Delete(string id)
        //{
        //    try
        //    {
        //        var todoItem = garageService.Find(id);
        //        if (todoItem != null)
        //        {
        //            garageService.DeleteData(id);
        //        }
        //        else
        //        {
        //            return base.BuildErrorResult(HttpStatusCode.NotFound, ErrorCode.RecordNotFound.ToString());
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return base.BuildErrorResult(HttpStatusCode.BadRequest, ErrorCode.CouldNotDeleteItem.ToString());
        //    }

        //    return base.BuildSuccessResult(HttpStatusCode.NoContent);
        //}
    }
}
