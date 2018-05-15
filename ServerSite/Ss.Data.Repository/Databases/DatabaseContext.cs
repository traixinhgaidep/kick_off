﻿using Ss.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Repository
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=DatabaseServerSite")
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
