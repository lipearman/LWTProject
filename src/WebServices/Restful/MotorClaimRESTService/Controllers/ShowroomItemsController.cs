using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MotorClaimRESTService.Attributes;
using MotorClaimRESTService.Models;
using MotorClaimRESTService.Services;

namespace MotorClaimRESTService.Controllers
{
    public class ShowroomItemsController : BaseApiController
    {
        static readonly IShowroomService ShowroomService = new ShowroomService(new ShowroomRepository());

        [HttpGet]
        [BasicAuthentication(RequireSsl = false)]
        public HttpResponseMessage Get()
        {
            return base.BuildSuccessResult(HttpStatusCode.OK, ShowroomService.GetData());
        }

        //[HttpPost]
        //[BasicAuthentication(RequireSsl = false)]
        //public HttpResponseMessage Create(ShowroomItem item)
        //{
        //    try
        //    {
        //        if (item == null ||
        //            string.IsNullOrWhiteSpace(item.Name) ||
        //            string.IsNullOrWhiteSpace(item.Notes))
        //        {
        //            return base.BuildErrorResult(HttpStatusCode.BadRequest, ErrorCode.ShowroomItemNameAndNotesRequired.ToString());
        //        }

        //        // Determine if the ID already exists
        //        var itemExists = ShowroomService.DoesItemExist(item.ID);
        //        if (itemExists)
        //        {
        //            return base.BuildErrorResult(HttpStatusCode.Conflict, ErrorCode.ShowroomItemIDInUse.ToString());
        //        }
        //        ShowroomService.InsertData(item);
        //    }
        //    catch (Exception)
        //    {
        //        return base.BuildErrorResult(HttpStatusCode.BadRequest, ErrorCode.CouldNotCreateItem.ToString());
        //    }

        //    return base.BuildSuccessResult(HttpStatusCode.Created);
        //}

        //[HttpPut]
        //[BasicAuthentication(RequireSsl = false)]
        //public HttpResponseMessage Edit(ShowroomItem item)
        //{
        //    try
        //    {
        //        if (item == null ||
        //            string.IsNullOrWhiteSpace(item.Name) ||
        //            string.IsNullOrWhiteSpace(item.Notes))
        //        {
        //            return base.BuildErrorResult(HttpStatusCode.BadRequest, ErrorCode.ShowroomItemNameAndNotesRequired.ToString());
        //        }

        //        var todoItem = ShowroomService.Find(item.ID);
        //        if (todoItem != null)
        //        {
        //            ShowroomService.UpdateData(item);
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
        //        var todoItem = ShowroomService.Find(id);
        //        if (todoItem != null)
        //        {
        //            ShowroomService.DeleteData(id);
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
