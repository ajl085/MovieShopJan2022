using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Purchase> GetPurchaseById(int userId, int movieId)
        {
            var purchase = await _dbContext.Purchases.FirstOrDefaultAsync(p => p.MovieId == movieId && p.UserId == userId);
            return purchase;
        }

    }
}
