using MIS.Application._Interfaces.Guests;
using MIS.Application.Guests.Commands.UpdateGuest;
using MIS.Domain.Entities;

namespace MIS.Persistence.Implementations.Guests
{
    public class UpdateGuestActivity : IUpdateGuestActivity
    {
        private readonly AppDbContext dbContext;

        public UpdateGuestActivity(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Execute(Guest guest, UpdateGuestCommand request, CancellationToken cancellationToken)
        {
            guest.ModifiedOn = DateTime.Now;

            guest.FirstName = request.FirstName.Trim();
            guest.LastName = request.LastName.Trim();
            guest.MiddleName = request.MiddleName.Trim();
            guest.BirthDate = request.BirthDate?.Date;
            guest.Address = request.Address;
            guest.Gender = request.Gender;
            guest.ContactNumber = request.ContactNumber;
            guest.CivilStatus = request.CivilStatus;
            guest.Age = request.Age != null && request.Age > 0 ? request.Age : null;
            guest.NetworkId = request.NetworkId;
            guest.Extension = request.Extension;

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
