using MotorClaimRESTService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorClaimRESTService.Services
{
    public interface IGPSTrackingService
    {
        bool DoesItemExist(string id);
        IEnumerable<PositionItem> Find(string id);
        //IEnumerable<PositionItem> GetData();
        void InsertData(PositionItem item);
        //void UpdateData(GarageItem item);
        //void DeleteData(string id);
    }

    public class GPSTrackingService : IGPSTrackingService
    {
        private readonly IGPSTrackingRepository _repository;

        public GPSTrackingService(IGPSTrackingRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;
        }

        public bool DoesItemExist(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(id);
            }

            return _repository.DoesItemExist(id);
        }

        public IEnumerable<PositionItem> Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id");
            }

            return _repository.Find(id);
        }

        //public IEnumerable<PositionItem> GetData()
        //{

        //    return _repository.All;
        //}

        public void InsertData(PositionItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            _repository.Insert(item);
        }

        //public void UpdateData(GarageItem item)
        //{
        //    if (item == null)
        //    {
        //        throw new ArgumentNullException("item");
        //    }

        //    _repository.Update(item);
        //}

        //public void DeleteData(string id)
        //{
        //    if (string.IsNullOrWhiteSpace(id))
        //    {
        //        throw new ArgumentNullException("id");
        //    }

        //    _repository.Delete(id);
        //}
    }

}