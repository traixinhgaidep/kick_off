using Ss.Data.Models;
using Ss.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ss.Data.Repository
{
    public class RepositoryContext : IRepositoryContext
    {
        public readonly DatabaseContext context;

        public RepositoryContext(DatabaseContext context)
        {
            this.context = context;
        }

        private static IRepository<User> _userRepository;
        public IRepository<User> UserRepository
        {
            get
            {
                return _userRepository ?? (_userRepository = new BaseRepository<User>(context));
            }
        }
    }
}
