using System;

namespace Url_Shortner.Service
{

    public abstract class Service : IDisposable
    {
        public const int limit = 1000;
        protected DataModelContext urlDbContext = new DataModelContext();
        
        public Service()
        {
            urlDbContext.Database.CommandTimeout = 60; // seconds
          
        }

        public void Dispose()
        {
            urlDbContext.Dispose();
           
        }
    }

}