﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WT_Exception;

namespace DataAccessLayer
{
  public  class WorkoutRepository : IRepository<Workout>
    {
        WorkoutContext ObjContext;
        public WorkoutRepository()
        {
            ObjContext = new WorkoutContext();
        }
        public bool Add(Workout item)
        {
            //item.status = "inactive";
             ObjContext.work.Add(item);
            var add = ObjContext.SaveChanges()>0;
            return add;
        }

        public bool Delete(Workout item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            ObjContext.Dispose();
        }

        public Workout FindById(int id)
        {
            try
            {
                return ObjContext.work.Find(id);
            }
            catch (DbException ex)
            {
                throw new WTException("Not found", ex);
            }
        }

        public List<Workout> GetAll()
        {
            try
            {
                return ObjContext.work.ToList();
            }
            catch(DbException ex)
            {
                throw new WTException("Not Found", ex);
            }
        }

        public bool Update(Workout item)
        {
            try
            {
                var items = ObjContext.work.Find(item.Id);
                items.status = "active";
                ObjContext.SaveChanges();
                return true;
            }
            catch (DbException ex)
            {
                throw new WTException("Invalid id", ex);
            }
        }
       public string Display(int id)
        {
            var statusqry = from s in ObjContext.work
                            where s.Id == id
                            select s.status;
            return statusqry.ToString();

        }
    }
}
