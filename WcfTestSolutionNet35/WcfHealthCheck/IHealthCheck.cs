using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfHealthCheck
{
    [ServiceContract]
    public interface IHealthCheck
    {
        [OperationContract]
        Health GetHealth();
    }

    [DataContract]
    public class Health
    {
        private bool isHealthy = true;
        private string name = "HealthCheck";

        public Health(string name)
        {
            this.name = name;
        }

        [DataMember]
        public bool IsHealthy
        {
            get { return isHealthy; }
            set { isHealthy = value; }
        }

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}

