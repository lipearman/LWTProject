using System.Collections.Generic;
using MotorClaimRESTService.Models;

namespace MotorClaimRESTService.Services
{
    public interface IGarageRepository
    {
        bool DoesItemExist(string id);
        IEnumerable<GarageItem> All { get; }
        GarageItem Find(string id);
        //void Insert(GarageItem item);
        //void Update(GarageItem item);
        //void Delete(string id);
    }
}
