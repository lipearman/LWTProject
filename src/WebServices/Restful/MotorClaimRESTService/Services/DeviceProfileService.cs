using System;
using System.Collections.Generic;
using MotorClaimRESTService.Models;

namespace MotorClaimRESTService.Services
{

    public interface IDeviceProfileService
    {
        bool DoesItemExist(string id);
        IEnumerable<DeviceProfileItem> Find(string id);
        void InsertData(DeviceProfileItem item);
        //void UpdateData(DeviceProfileItem item);
        //void DeleteData(string id);
    }
    public class DeviceProfileService : IDeviceProfileService
    {
        private readonly IDeviceProfileRepository _repository;

        public DeviceProfileService(IDeviceProfileRepository repository)
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

        public IEnumerable<DeviceProfileItem> Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id");
            }

            return _repository.Find(id);
        }



        public void InsertData(DeviceProfileItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            _repository.Insert(item);
        }

        //public void UpdateData(DeviceProfileItem item)
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