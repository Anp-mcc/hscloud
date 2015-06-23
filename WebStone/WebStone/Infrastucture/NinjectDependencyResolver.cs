using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Entity;
using Ninject;
using Raven.Client;
using Raven.Client.Document;

namespace WebStone.Infrastucture
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly StandardKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBinding();
        }

        private void AddBinding()
        {
            InitDbContext();
        }

        private void InitDbContext()
        {
            var connectionString = ConfigurationManager.AppSettings["DatabaseUrl"];
            var databaseName = ConfigurationManager.AppSettings["DefaultDatabase"];
            var documentStore = new DocumentStore {Url = connectionString, DefaultDatabase = databaseName};
            documentStore.Initialize();

            _kernel.Bind<IDocumentStore>().ToConstant(documentStore);
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}