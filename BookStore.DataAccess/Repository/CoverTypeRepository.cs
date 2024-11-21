using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookStore.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public CoverTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(CoverType obj)
        {
            var objFromDb = _context.CoverTypes.FirstOrDefault(c => c.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
            }
        }

        // Implement GetFirstOrDefault method
        public CoverType GetFirstOrDefault(Expression<Func<CoverType, bool>> filter)
        {
            return _context.CoverTypes.FirstOrDefault(filter);
        }
    }
}
