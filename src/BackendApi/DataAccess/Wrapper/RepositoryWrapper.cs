using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;

namespace DataAccess.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper

    {
        private RatingsAndReviewsContext _repoContext;

        private IUserRepository _user;
        public IUserRepository User
        {
            get
            {
                if(_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }

        public RepositoryWrapper (RatingsAndReviewsContext repoContext)
        {
            _repoContext = repoContext;
        }


        public async Task Save()
        {
            _repoContext.SaveChangesAsync();
        }

    }
}
