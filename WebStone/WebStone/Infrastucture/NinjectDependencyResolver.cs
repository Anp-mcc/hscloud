using System;
using System.Collections.Generic;
using System.Configuration;
using CQS.Core;
using CQS.Models;
using CQS.Query;
using CQS.QueryHandler;
using DataAccess;
using Ninject;
using Raven.Client.Document;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace WebStone.Infrastucture
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBinding();
        }

        private void AddBinding()
        {
            var docStore = InitDbContext();

            var core = new DatabaseCore(docStore);

            _kernel.Bind<IDatabaseCore>().ToConstant(core);
            _kernel.Bind<IQueryDispatcher>().To<QueryDispatcher>();
            


            //TODO move to automatic registration
            _kernel.Bind<IQueryHandler<AllDeckQuery, AllDeckQueryResult>>().To<AllDeckQueryHandler>();
            _kernel.Bind<IQueryHandler<DeckWithCardsQuery, DeckWithCardsQueryResult>>().To<DeckWithCardsQueryHandler>();
            _kernel.Bind<IQueryHandler<AllCardsQuery, AllCardsQueryResult>>().To<AllCardsQueryHandler>();
            _kernel.Bind<IQueryHandler<CardForClassQuery, CardsForClassQueryResult>>().To<CardForClassQueryHandler>();
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