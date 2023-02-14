using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBrokerLibrary
{
    public interface IPublisher : IDisposable
    {
        void Publish(
            string message, 
            string routingKey, 
            IDictionary<string, object> headers, 
            string timeToLive = null);
    }
}
