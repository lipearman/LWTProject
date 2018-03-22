using System.Collections.Generic;
using MotorClaimRESTService.Models;

namespace MotorClaimRESTService.Services
{
    public interface IShowroomRepository
    {
        bool DoesItemExist(string id);
        IEnumerable<ShowroomItem> All { get; }
        ShowroomItem Find(string id);
        void Insert(ShowroomItem item);
        void Update(ShowroomItem item);
        void Delete(string id);
    }
}
