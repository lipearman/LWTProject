using System;
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
    public class GarageService : IGarageService
    {
        private readonly IGarageRepository _repository;

        public GarageService(IGarageRepository repository)
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

        public GarageItem Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id");
            }

            return _repository.Find(id);
        }

        public IEnumerable<GarageItem> GetData()
        {

            return _repository.All;
        }

        //public void InsertData(GarageItem item)
        //{
        //    if (item == null)
        //    {
        //        throw new ArgumentNullException("item");
        //    }

        //    _repository.Insert(item);
        //}

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
