using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using WashPassAPI.Models;
namespace WashPassAPI.EntityDataModels;

public class WashPassDataModel
{
    public  IEdmModel GetEntityDataModel()
    {
        var builder = new ODataConventionModelBuilder
        {
            Namespace = "WashPass",
            ContainerName = "WashPassContainer"
        };
        builder.EntitySet<AppUser>("AppUsers");
        builder.EntitySet<AdminAccount>("AdminUsers");
        builder.EntitySet<Vehicle>("Vehicles");
        builder.EntitySet<CarWashStation>("CarWashStations");
        builder.EntitySet<StationImage>("StationImages");
        builder.EntitySet<Service>("Services");
        builder.EntitySet<Booking>("Bookings");
        //builder.EntitySet<BookingService>("BookingServices");
        builder.EntitySet<Review>("Reviews");
        builder.EnableLowerCamelCase(NameResolverOptions.ProcessReflectedPropertyNames);
        return builder.GetEdmModel();
    }
}
