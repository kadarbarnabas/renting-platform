using RentingPlatform.Shared;
using Microsoft.EntityFrameworkCore;

namespace RentingPlatform;

public class RentingPlatformContext : DbContext
{
    public RentingPlatformContext(){}

    public RentingPlatformContext(DbContextOptions<RentingPlatformContext> options) : base(options) {}
    
    public virtual DbSet<Users> User { get; set;} //To do: ez alá tegyétek be a ti DTO-tok conextjét értelem szerűen kicserélve a 'Users' és a 'User' mezőket

    public virtual DbSet<Car> Cars { get; set;}

    public virtual DbSet<Airbnbs> Airbnb { get; set; }
}