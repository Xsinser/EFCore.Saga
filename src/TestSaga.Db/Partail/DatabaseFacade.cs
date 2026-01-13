using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Microsoft.EntityFrameworkCore.Infrastructure;

public class DatabaseSageFacade : DatabaseFacade
{
    public DatabaseSageFacade(DbContext context) : base(context)
    {

    }


}

