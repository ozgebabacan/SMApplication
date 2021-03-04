using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SMApplication.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMApplication.Service
{
    public class BaseService
    {
        public readonly ServiceSettings ServiceSettings;

        public BaseService(IServiceProvider serviceProvider)
        {
            ServiceSettings = serviceProvider.GetRequiredService<IOptionsMonitor<ServiceSettings>>().CurrentValue;
        }
    }
}
