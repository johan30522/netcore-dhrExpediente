


using AppExpedienteDHR.Core.Domain.Entities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task Update(Category category)
        {
            Category categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;
                await _context.SaveChangesAsync();
            }
        }
    }
}
