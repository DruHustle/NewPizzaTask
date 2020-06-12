//using NewPizzaTask.Database;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace NewPizzaTask.Repository
//{
//    public class GenericUnitOfWork : IDisposable
//    {
//        ApplicationDBContext dBContext = new ApplicationDBContext();

//        public IRepository<EntityType> GetRepositoryInstance<EntityType>() where EntityType : class
//        {
//            return new GenericRepository<EntityType>(dBContext);
//        }

//        public void SaveChanges()
//        {
//            dBContext.SaveChanges();
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!this.disposed)
//            {
//                if (disposing)
//                {
//                    dBContext.Dispose();
//                }
//            }
//            this.disposed = true;
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        private bool disposed = false;

//    }
//}