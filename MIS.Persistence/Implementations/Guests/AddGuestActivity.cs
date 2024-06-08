using MIS.Application._Helpers;
using MIS.Application._Interfaces.Guests;
using MIS.Domain.Entities;

namespace MIS.Persistence.Implementations.Guests
{
    public class AddGuestActivity : IAddGuestActivity
    {
        private readonly AppDbContext dbContext;

        public AddGuestActivity(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Execute(Guest guest, CancellationToken cancellationToken)
        {
            var dateNow = DateTime.Now;
            guest.CreatedOn = dateNow;
            guest.Code = CodeHelper.GenerateGuestCode();

            dbContext.Guests.Add(guest);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
