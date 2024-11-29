using Microsoft.EntityFrameworkCore;
using RiraCRUD.Domain.Entities;

namespace RiraCRUD.Domain.Interface
{
    public interface IAppDbContext
    {
        DbSet<Person> Persons { get; }
        Task SaveChangesAsync(CancellationToken cancellation);
    }
}
