using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfHealthCheck
{
    public class HealthCheck : IHealthCheck
    {
        private Health health;

        public HealthCheck(string name)
        {
            this.health = new Health(name);
        }

        public Health GetHealth()
        {
            return this.health;
        }
    }
}
