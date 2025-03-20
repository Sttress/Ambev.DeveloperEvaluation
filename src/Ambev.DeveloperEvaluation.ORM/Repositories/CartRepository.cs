using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of <see cref="ICartRepository"/> using Entity Framework Core.
    /// </summary>
    public class CartRepository : ICartRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of <see cref="CartRepository"/>.
        /// </summary>
        /// <param name="context">The database context</param>
        public CartRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new cart in the database.
        /// </summary>
        /// <param name="cart">The cart to be created</param>
        public void Create(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        /// <summary>
        /// Retrieves a cart by its unique identifier, including items that are not deleted.
        /// </summary>
        /// <param name="id">The unique identifier of the cart</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The cart if found, null otherwise</returns>
        public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(p => p.Items.Where(i => i.PurchaseStatus != PurchaseStatus.Deleted))
                    .ThenInclude(i => i.Product)
                .Where(c => c.PurchaseStatus != PurchaseStatus.Deleted)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a cart by its unique identifier, including active items (not yet purchased).
        /// </summary>
        /// <param name="id">The unique identifier of the cart</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The cart if found with its active items, null otherwise</returns>
        public async Task<Cart?> GetByIdActiveItemsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(p => p.Items.Where(i => i.PurchaseStatus == PurchaseStatus.Created))
                    .ThenInclude(i => i.Product)
                .Where(c => c.PurchaseStatus != PurchaseStatus.Deleted)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves a paginated list of carts, including only items that are not deleted.
        /// </summary>
        /// <param name="paging">The pagination query parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A paginated result of carts</returns>
        public async Task<PaginationQueryResult<Cart>> PaginateAsync(
            PaginationQuery paging,
            CancellationToken cancellationToken = default)
        {
            Expression<Func<Cart, IEnumerable<CartItem>>> itemsNotDeleted = p => p.Items.Where(i => i.PurchaseStatus != PurchaseStatus.Deleted);

            // Note: ordering is applied only to the items of the cart. Other orderings should be done manually and used in the Include method.
            var sortsByRelatedItems = paging.Orders.Where(s => s.Key.StartsWith(nameof(Cart.Items) + '.', StringComparison.OrdinalIgnoreCase));
            itemsNotDeleted = itemsNotDeleted.RewriteExpressionWithOrderBy(sortsByRelatedItems);

            return await _context.Carts
                .AsNoTracking()
                .Include(itemsNotDeleted)
                .Where(c => c.PurchaseStatus != PurchaseStatus.Deleted)
                .ToPaginateAsync(paging, cancellationToken);
        }
    }
}