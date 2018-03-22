using System.Collections.Generic;
using MotorClaimRESTService.Models;

namespace MotorClaimRESTService.Services
{
    public interface IGarageService
    {
        bool DoesItemExist(string id);
        GarageItem Find(string id);
        IEnumerable<GarageItem> GetData();
        //void InsertData(GarageItem item);
        //void UpdateData(GarageItem item);
        //void DeleteData(string id);
    }
}
