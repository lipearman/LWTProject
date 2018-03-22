using System.Collections.Generic;
using System.Linq;
using MotorClaimRESTService.Models;

namespace MotorClaimRESTService.Services
{
    public class ShowroomRepository : IShowroomRepository
    {
        private List<ShowroomItem> _ShowroomList;

        public ShowroomRepository()
        {
            InitializeData();
        }

        public IEnumerable<ShowroomItem> All
        {
            get { return _ShowroomList; }
        }

        public bool DoesItemExist(string id)
        {
            return _ShowroomList.Any(item => item.ID == id);
        }

        public ShowroomItem Find(string id)
        {
            return _ShowroomList.Where(item => item.ID == id).FirstOrDefault();
        }

        public void Insert(ShowroomItem item)
        {
            _ShowroomList.Add(item);
        }

        public void Update(ShowroomItem item)
        {
            var todoItem = this.Find(item.ID);
            var index = _ShowroomList.IndexOf(todoItem);
            _ShowroomList.RemoveAt(index);
            _ShowroomList.Insert(index, item);
        }

        public void Delete(string id)
        {
            _ShowroomList.Remove(this.Find(id));
        }

        #region Helpers

        private void InitializeData()
        {
            _ShowroomList = new List<ShowroomItem>();

            var todoItem1 = new ShowroomItem
            {
                ID = "6bb8a868-dba1-4f1a-93b7-24ebce87e243",
                Name = "1.Learn app development",
                Notes = "Attend Xamarin University",
                Done = true
            };

            var todoItem2 = new ShowroomItem
            {
                ID = "b94afb54-a1cb-4313-8af3-b7511551b33b",
                Name = "2.Develop apps",
                Notes = "Use Xamarin Studio/Visual Studio",
                Done = false
            };

            var todoItem3 = new ShowroomItem
            {
                ID = "ecfa6f80-3671-4911-aabe-63cc442c1ecf",
                Name = "3.Publish apps",
                Notes = "All app stores",
                Done = false,
            };

            _ShowroomList.Add(todoItem1);
            _ShowroomList.Add(todoItem2);
            _ShowroomList.Add(todoItem3);
        }

        #endregion
    }
}
