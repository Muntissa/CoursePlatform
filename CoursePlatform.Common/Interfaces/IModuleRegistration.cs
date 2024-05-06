﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common.Interfaces
{
    public interface IModuleRegistration
    {
        void Registrate(IServiceCollection services);
    }
}