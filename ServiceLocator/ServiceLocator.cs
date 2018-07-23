using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.ServiceAgent;
using WebApplication1.ServiceAgentInterfaces;
using WebApplication1.ServiceLocatorInterfaces;

namespace WebApplication1.ServiceLocator
{
    class ServiceLocatorTool
    {
        public ServiceLocator ObjServiceLocator;
        public ServiceLocatorTool()
        {
            
            ObjServiceLocator = new ServiceLocator("https://bc13-new.test.tdc.dk/bc/secure/");
            //ObjServiceLocator._bcAgentHelper._serviceAreaMappings.Add(typeof(ISubscriptionAgent), "");

            ObjServiceLocator.Map(ObjServiceLocator._bcAgentHelper.CreateChannel<ISubscriptionAgent>());
            ObjServiceLocator.Map(ObjServiceLocator._bcAgentHelper.CreateChannel<IFasAgent>());

        }

    }

    public partial class ServiceLocator : IServiceLocator
    {
        public readonly Dictionary<Type, object> _serviceMappings = new Dictionary<Type, object>();
        public readonly BcAgentHelper _bcAgentHelper;

        public ServiceLocator()
        {
            _bcAgentHelper = new BcAgentHelper("https://bc13-new.test.tdc.dk/bc/secure/");
            Map(_bcAgentHelper.CreateChannel<ISubscriptionAgent>());
            Map(_bcAgentHelper.CreateChannel<IFasAgent>());
        }

        public ServiceLocator(string url)
        {            
            _bcAgentHelper = new BcAgentHelper(url);
        }

        public T GetService<T>()
        {
            if (IsTypeMapped(typeof(T)))
            {
                return (T)_serviceMappings[typeof(T)];
            }
            else
            {
                throw new NotImplementedException(
                    string.Format("The interface {0} has not been mapped in the ServiceLocator.", typeof(T).Name));
            }
        }

        public void Map<TService>(TService instance)
        {
            _serviceMappings[typeof(TService)] = instance;
        }

        public bool IsTypeMapped(Type type)
        {
            return _serviceMappings.ContainsKey(type);
        }
    }
}