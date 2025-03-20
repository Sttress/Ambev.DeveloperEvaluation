using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IProductRepository"/> using Entity Framework Core.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of ProductRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new product in the database
        /// </summary>
        /// <param name="product">The product to create</param>
        public void Create(Product product)
        {
            _context.Products.Add(product);
        }

        /// <summary>
        /// Deletes a product from the database
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete</param>
        public void Delete(Guid id)
        {
            var product = new Product { Id = id };
            _context.Products.Attach(product);
            _context.Products.Remove(product);
        }

        /// <summary>
        /// Retrieves a product by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The product if found, null otherwise</returns>
        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a product by its title
        /// </summary>
        /// <param name="name">The title of the product</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The product if found, null otherwise</returns>
        public async Task<Product?> GetByTitleAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .FirstOrDefaultAsync(u => u.Title == name, cancellationToken);
        }

        /// <summary>
        /// Retrieves a collection of products by their unique identifiers
        /// </summary>
        /// <param name="ids">An array of unique identifiers</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A collection of products</returns>
        public async Task<ICollection<Product>> ListByIdsAsync(Guid[] ids, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Where(p => ids.Contains(p.Id))
                .ToArrayAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves a paginated list of products
        /// </summary>
        /// <param name="paging">The pagination query parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A paginated result of products</returns>
        public async Task<PaginationQueryResult<Product>> PaginateAsync(
            PaginationQuery paging,
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .ToPaginateAsync(paging, cancellationToken);
        }

        /// <summary>
        /// Searches for products by their category name with pagination
        /// </summary>
        /// <param name="categoryName">The name of the category to search</param>
        /// <param name="paging">The pagination query parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A paginated result of products that belong to the specified category</returns>
        public async Task<PaginationQueryResult<Product>> ListPaginatedByCategoryNameAsync(
            string categoryName,
            PaginationQuery paging,
            CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Where(p => EF.Functions.ILike(p.Category.Name, categoryName))
                .ToPaginateAsync(paging, cancellationToken);
        }
    }
}