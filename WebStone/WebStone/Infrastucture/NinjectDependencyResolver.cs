using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using CQS;
using CQS.Core;
using CQS.Query;
using DataAccess;
using Ninject;
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
            var docStore = InitDbContext();

            var core = new DatabaseCore(docStore);

            _kernel.Bind<IDatabaseCore>().ToConstant(core);

            _kernel.Bind<IQueryDispatcher>().To<QueryDispatcher>();
            _kernel.Bind<IQueryHandler<AllDeckQuery, AllDeckQueryResult>>().To<AllDeckQueryHandler>();
            _kernel.Bind<IQueryHandler<DeckWithCardsQuery, DeckWithCardsQueryResult>>().To<DeckWithCardsQueryHandler>();
        }

        private DocumentStore InitDbContext()
        {
            var documentStore = new DocumentStore
            {
                Url = ConfigurationManager.AppSettings["DatabaseUrl"],
                DefaultDatabase = ConfigurationManager.AppSettings["DefaultDatabase"]
            };

            documentStore.Initialize();

            return documentStore;
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