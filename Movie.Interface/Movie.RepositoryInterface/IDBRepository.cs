using Movie_store.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Movie_store.RepositoryInterface
{
    public interface IDBRepository
    {
        Task<UserDomain> GetUserDetail(Expression<Func<UserDomain, bool>> predicate);
        Task<List<UserRoleDomain>> GetUserRoles(Expression<Func<UserRoleDomain, bool>> predicate);
        Task<List<RoleDomain>> GetRole();
    }
}
