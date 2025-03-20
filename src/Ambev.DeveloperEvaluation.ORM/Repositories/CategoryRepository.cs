using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of <see cref="ICategoryRepository"/> using Entity Framework Core.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of <see cref="CategoryRepository"/>.
        /// </summary>
        /// <param name="context">The database context</param>
        public CategoryRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new category in the database.
        /// </summary>
        /// <param name="category">The category to create</param>
        public void Create(Category category)
        {
            _context.Categories.Add(category);
        }

        /// <summary>
        /// Retrieves a category by its name.
        /// </summary>
        /// <param name="name">The name of the category to search for</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The category if found, null otherwise</returns>
        public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(u => EF.Functions.ILike(u.Name, $"%{name}%"), cancellationToken);
        }

        /// <summary>
        /// Lists all categories that are currently being used by products.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A collection of category names</returns>
        public async Task<ICollection<string>> ListAllCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Where(c => c.Products.Any())
                .Select(c => c.Name)
                .ToArrayAsync(cancellationToken);
        }
    }
}