using Infra.Connection;
using Infra.Enitty;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class BaseRepositoryEntityFramework
    {
        protected readonly ContextEntityFramework Context;

        public BaseRepositoryEntityFramework(ContextEntityFramework context)
        {
            Context = context;
        }
    }
}
