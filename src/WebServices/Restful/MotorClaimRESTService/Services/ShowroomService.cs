using System;
using System.Collections.Generic;
using MotorClaimRESTService.Models;

namespace MotorClaimRESTService.Services
{
    public class ShowroomService : IShowroomService
    {
        private readonly IShowroomRepository _repository;

        public ShowroomService(IShowroomRepository repository)
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

        public ShowroomItem Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id");
            }

            return _repository.Find(id);
        }

        public IEnumerable<ShowroomItem> GetData()
        {
            return _repository.All;
        }

        public void InsertData(ShowroomItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            _repository.Insert(item);
        }

        public void UpdateData(ShowroomItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            _repository.Update(item);
        }

        public void DeleteData(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id");
            }

            _repository.Delete(id);
        }
    }
}
