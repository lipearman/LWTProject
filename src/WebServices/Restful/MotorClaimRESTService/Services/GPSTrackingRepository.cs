using MotorClaimRESTService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorClaimRESTService.Services
{
    public interface IGPSTrackingRepository
    {
        bool DoesItemExist(string id);
        //IEnumerable<PositionItem> All { get; }
        IEnumerable<PositionItem> Find(string id);
        void Insert(PositionItem item);
        //void Update(GarageItem item);
        //void Delete(string id);
    }
    public class GPSTrackingRepository : IGPSTrackingRepository
    {
        private List<PositionItem> _positionList;

        public GPSTrackingRepository()
        {
            InitializeData();
        }

        //public IEnumerable<PositionItem> All
        //{
        //    get
        //    {
        //        InitializeData();
        //        return _positionList;
        //    }
        //}

        public bool DoesItemExist(string id)
        {
            return _positionList.Any(item => item.DeviceId == id);
        }

        public IEnumerable<PositionItem> Find(string id)
        {
            InitializeData();
            return _positionList.Where(item => item.DeviceId == id).ToList();
        }

        public void Insert(PositionItem item)
        {
            _positionList.Add(item);
        }

        //public void Update(GarageItem item)
        //{
        //    var todoItem = this.Find(item.ID);
        //    var index = _garageList.IndexOf(todoItem);
        //    _garageList.RemoveAt(index);
        //    _garageList.Insert(index, item);
        //}

        //public void Delete(string id)
        //{
        //    _garageList.Remove(this.Find(id));
        //}

        #region Helpers

        private void InitializeData()
        {
            _positionList = new List<PositionItem>();

            //var todoItem1 = new GarageItem
            //{
            //    ID = "6bb8a868-dba1-4f1a-93b7-24ebce87e243",
            //    Name = "Learn app development",
            //    Notes = "Attend Xamarin University",
            //    Done = true
            //};

            //var todoItem2 = new GarageItem
            //{
            //    ID = "b94afb54-a1cb-4313-8af3-b7511551b33b",
            //    Name = "Develop apps",
            //    Notes = "Use Xamarin Studio/Visual Studio",
            //    Done = false
            //};

            //var todoItem3 = new GarageItem
            //{
            //    ID = "ecfa6f80-3671-4911-aabe-63cc442c1ecf",
            //    Name = "Publish apps",
            //    Notes = "All app stores",
            //    Done = false,
            //};

            //_garageList.Add(todoItem1);
            //_garageList.Add(todoItem2);
            //_garageList.Add(todoItem3);

            /*
            using (Context.DataClasses_MotorClaimExt dc = new Context.DataClasses_MotorClaimExt())
            {
                _garageList = dc.tblGarages.Where(i => i.IsActive == true
                    && i.TelNo != ""
                    && i.Latitude > 0
                    ).Select(i => new GarageItem
                    {
                        ID = i.ID,
                        GarageCode = i.GarageCode,
                        GarageName = i.GarageName,
                        Address = i.Address,
                        Province = i.Province,
                        ZipCode = i.Zipcode,
                        TelCode = i.TelCode,
                        TelNo = string.Format("{0}-{1}", i.TelCode, i.TelNo),
                        FaxNo = string.Format("{0}-{1}", i.TelCode, i.FaxNo),
                        Region = i.Region,
                        SalesArea = i.SalesArea,
                        GarageType = i.GarageType,
                        Longitude = i.Longitude,
                        Latitude = i.Latitude,
                    }).ToList();
            }
             */
        }

        #endregion
    }


}