using System.Collections.Generic;
using MotorClaimRESTService.Models;

namespace MotorClaimRESTService.Services
{
    public interface IShowroomService
    {
        bool DoesItemExist(string id);
        ShowroomItem Find(string id);
        IEnumerable<ShowroomItem> GetData();
        void InsertData(ShowroomItem item);
        void UpdateData(ShowroomItem item);
        void DeleteData(string id);
    }
}
