using BookStore.Models;
using System;
using System.Linq.Expressions;

namespace BookStore.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        void Update(CoverType obj);
        CoverType GetFirstOrDefault(Expression<Func<CoverType, bool>> filter);
    }
}
