using Movie_store.Common;
using Movie_store.Common.DBConnection;
using Movie_store.Domain;
using Movie_store.RepositoryInterface;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movie_store.DBRepository
{
    public class DBRepository : IDBRepository
    {
        private readonly ICacheRepository _cacheRepository;

        public DBRepository(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }
        public async Task<List<RoleDomain>> GetRole()
        {
            List<RoleDomain> roles = _cacheRepository.GetCache<List<RoleDomain>>(CacheConstant.RoleTableCache);
            if (roles.HasRecords())
                return roles;

            using (var db = new MovieStoreConnection())
            using (var trans = await db.BeginTransactionAsync(IsolationLevel.ReadUncommitted))
            {
                roles = await db.GetTable<RoleDomain>()
                               .Where(x => x.R_IsActive)
                               .ToListAsync();
                _cacheRepository.SetCache<List<RoleDomain>>(CacheConstant.RoleTableCache, roles, CacheConstant.CacheExpireTimeInHours);
            }
            return roles;
        }

        public async Task<UserDomain> GetUserDetail(Expression<Func<UserDomain, bool>> predicate)
        {
            if (predicate.IsNull())
                return default;

            using (var db = new MovieStoreConnection())
            using (var trans = await db.BeginTransactionAsync(IsolationLevel.ReadUncommitted))
            {
                return await db.GetTable<UserDomain>()
                               .Where(predicate)
                               .FirstOrDefaultAsync();
            }
        }

        public async Task<List<UserRoleDomain>> GetUserRoles(Expression<Func<UserRoleDomain, bool>> predicate)
        {
            if (predicate.IsNull())
                return default;

            using (var db = new MovieStoreConnection())
            using (var trans = await db.BeginTransactionAsync(IsolationLevel.ReadUncommitted))
            {
                return await db.GetTable<UserRoleDomain>()
                               .Where(predicate)
                               .ToListAsync();
            }
        }
    }
}
