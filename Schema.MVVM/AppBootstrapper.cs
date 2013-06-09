using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Caliburn.Micro;
using Schema.Core.Reader;
using Schema.MVVM.Helpers;
using Schema.MVVM.ViewModels;
using StructureMap;

namespace Schema.MVVM
{
    public class AppBootstrapper : Bootstrapper<MainViewModel>
    {
        private CompositionContainer container;
        public IContainer Container;
       
        private const string MySql = "";
        private const string MsSql = "";
        protected override void Configure()
        {
            Container = new Container(x =>
            {
                x.For<ISqlReader>().Use<MsSqlSqlReader>().Named(MsSql);
                x.For<ISqlReader>().Use<MySqlSqlReader>().Named(MySql);
            });
            //Container = new Container(x => x.For<ISqlReader>()
            //                                .AddInstances(i =>
            //                                                  {
            //                                                      i.Type(typeof (MsSqlSqlReader)).Named(MsSql);
            //                                                      i.Type(typeof (MySqlSqlReader)).Named(MySql);
            //                                                  })
            //                               .Use(new MsSqlSqlReader()));
            //Container.GetInstance<ISqlReader>();

            var msSqlReaderInst = Container.GetInstance<ISqlReader>(MsSql);
            var mySqlReaderInst = Container.GetInstance<ISqlReader>(MySql);
            container = new CompositionContainer(new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()));

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
           // batch.AddExportedValue<IWindowManager>(new AppWindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(container);

            container.Compose(batch);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
            {
                return exports.First();
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

    }
}
