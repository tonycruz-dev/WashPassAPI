using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WashPassAPI.DTOs;
using WashPassAPI.Models;
using WashPassAPI.SeedModel;

namespace WashPassAPI.Data;

public class DbInitializer
{
    public static async Task SeedData(AppDbContext context, UserManager<User> userManager)
    {

        if (context.AppUsers.Any()) return;
        // get washpass.json from data folder
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "data", "washpass.json");
        // get appusers from json file
        var json = await File.ReadAllTextAsync(jsonFilePath);
        var rootobject = System.Text.Json.JsonSerializer.Deserialize<Rootobject> (json);
        var appUsers = rootobject?.Washpass.AppUsers;
        foreach (var u in appUsers)
        {
            var user = new User
            {
                UserName = u.Email,
                Email = u.Email,
                DisplayName = u.FullName
            };
            await userManager.CreateAsync(user, "Pa$$w0rd");
            var location = u.Location ?? "";
            var userId = Guid.Parse((user.Id));
            var au = new AppUser
            {
                FullName = u.FullName,
                Email = u.Email,
                UserId = userId,
                Location = location,
                CreatedAt = DateTime.UtcNow.ToLocalTime(),
            };
            context.AppUsers.Add(au);
            await context.SaveChangesAsync();
        }
        var adminAccounts = rootobject?.Washpass.AdminAccounts;
        foreach (var a in adminAccounts)
        {
            var user = new User
            {
                UserName = a.Email,
                Email = a.Email,
                DisplayName = a.FullName
            };
            await userManager.CreateAsync(user, "Pa$$w0rd");

            var aa = new AdminAccount
            {
                FullName = a.FullName,
                Email = a.Email,
                Location = a.Location,
                Role = a.Role,
                UserId = Guid.Parse(user.Id)
            };
            context.AdminAccounts.Add(aa);
            await context.SaveChangesAsync();
        }
        var carWashStations = rootobject?.Washpass.CarWashStations;
        var cwCount = 1;

        var adminAccount = await context.AdminAccounts.ToListAsync();
        var firstAdmin = adminAccount.FirstOrDefault();
        var secondAdmin = adminAccount.Skip(1).FirstOrDefault();
        foreach (var s in carWashStations)
        {
            int adminId = 0;
            adminId = (cwCount == 1 || cwCount == 2) ? firstAdmin.Id : secondAdmin.Id;

            var station = new CarWashStation
            {
                Name = s.Name,
                Address = s.Address,
                Description = s.Description,
                PhoneNumber = s.PhoneNumber,
                Latitude = s.Latitude,
                Longitude = s.Longitude,
                AdminId = adminId,
                OpeningTime = s.OpeningTime,
                ClosingTime = s.ClosingTime,
                CreatedAt = DateTime.UtcNow.ToLocalTime(),
            };
            context.CarWashStations.Add(station);
            await context.SaveChangesAsync();
            cwCount++;
        }
        
        var services = rootobject?.Washpass.Services;
        var cws = await context.CarWashStations.ToListAsync();
        var firstCarWashStation = cws.FirstOrDefault();
        var secondCarWashStation = cws.Skip(1).FirstOrDefault();
        var thirdCarWashStation = cws.Skip(2).FirstOrDefault();
        var serviceCount = 1;
        foreach (var s in services)
        {
            int cwsId;
            if (serviceCount == 1 || serviceCount == 2)
            {
                cwsId = firstCarWashStation.Id;
            }
            else if (serviceCount == 3 )
            {
                cwsId = secondCarWashStation.Id;
            }
            else
            {
                cwsId = thirdCarWashStation.Id;
            }
            var service = new Models.Service
            {
                Name = s.Name,
                Price = s.Price,
                DurationMinutes = s.DurationMinutes,
                CarWashStationId = cwsId,
                CreatedAt = DateTime.UtcNow.ToLocalTime(),
                TokenValue = s.TokenValue,
                ServiceType = s.ServiceType,
                CommissionPercent = s.CommissionPercent,
            };
            context.Services.Add(service);
            await context.SaveChangesAsync();
            serviceCount++;
        }
        var subscriptions = rootobject?.Washpass.Subscriptions;

        var aus = await context.AppUsers.ToListAsync();
        var firstAppUser = aus.FirstOrDefault();
        var secondAppUser = aus.Skip(1).FirstOrDefault();
        var thirdAppUser = aus.Skip(2).FirstOrDefault();
        var subscriptionCount = 1;
        foreach (var s in subscriptions)
        {
            int appUserId;
            if (subscriptionCount == 1)
            {
                appUserId = firstAppUser.Id;
            }
            else if (subscriptionCount == 3)
            {
                appUserId = secondAppUser.Id;
            }
            else
            {
                appUserId = thirdAppUser.Id;
            }
            var subscription = new Models.Subscription
            {
                AppUserId = appUserId,
                PlanName = s.PlanName,
                MonthlyFee = s.MonthlyFee,
                NextPaymentDate = DateTime.Parse(s.NextPaymentDate),
                CreatedAt = DateTime.UtcNow.ToLocalTime(),
            };
            context.Subscriptions.Add(subscription);
            await context.SaveChangesAsync();
            subscriptionCount++;
        }

        var vehicles = rootobject?.Washpass.Vehicles;
        var vihicleCount = 1;
        foreach (var v in vehicles)
        {
            var vUserId = 0;
            if(vihicleCount == 1 || vihicleCount == 2)
            {
                vUserId = firstAppUser.Id;
            }
            else if (vihicleCount == 3)
            {
                vUserId = secondAppUser.Id;
            }
            else
            {
                vUserId = thirdAppUser.Id;
            }

            var vehicle = new Models.Vehicle
            {
                AppUserId = vUserId,
                Make = v.Make,
                Model = v.Model,
                LicensePlate = v.LicensePlate,
                VehicleType = v.VehicleType,
                PhotoUrl = v.PhotoUrl,

            };
            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();
            vihicleCount++;
        }

        var tokens = rootobject?.Washpass.Tokens;
        var tokenCount = 1;
        foreach (var t in tokens)
        {
            int appUserId;

            if (tokenCount == 1 )
            {
                appUserId = firstAppUser.Id;
            }
            else if (tokenCount == 2)
            {
                appUserId = secondAppUser.Id;
            }
            else
            {
                appUserId = thirdAppUser.Id;
            }


            var token = new Models.Token
            {
                AppUserId = appUserId,
                Amount = t.Amount,
                Source = t.Source,
                AcquiredAt = t.AcquiredAt,
            };
            context.Tokens.Add(token);
            await context.SaveChangesAsync();
            tokenCount++;
        }
        
        
        var stationImages = rootobject?.Washpass.StationImages;
        var carWashStationsList = await context.CarWashStations.ToListAsync();
        var firstCarWashStationImage = carWashStationsList.FirstOrDefault();
        var secondCarWashStationImage = carWashStationsList.Skip(1).FirstOrDefault();
        var thirdCarWashStationImage = carWashStationsList.Skip(2).FirstOrDefault();
        var stationImageCount = 1;
        foreach (var i in stationImages)
        {
            var stationId = 0;

            if (stationImageCount == 1)
            {
                stationId = firstCarWashStationImage.Id;
            }
            else if (stationImageCount == 2)
            {
                stationId = secondCarWashStationImage.Id;
            }
            else
            {
                stationId = thirdCarWashStationImage.Id;
            }

            var image = new Models.StationImage
            {
                StationId = stationId,
                ImageUrl = i.ImageUrl,
                CreatedAt = DateTime.UtcNow.ToLocalTime(),
            };
            context.StationImages.Add(image);
            await context.SaveChangesAsync();
            stationImageCount++;
        }


        var bookings = rootobject?.Washpass.Bookings;
        var bookingCount = 1;
        foreach (var b in bookings)
        {
            var CarWashStationId = 0;
            var userIs = 0;
            if (bookingCount == 1)
            {
                userIs = firstAppUser.Id;
                CarWashStationId = firstCarWashStation.Id;
            }
            else if (bookingCount == 2)
            {
                userIs = secondAppUser.Id;
                CarWashStationId = secondCarWashStation.Id;
            }
            else
            {
                userIs = thirdAppUser.Id;
                CarWashStationId = thirdCarWashStation.Id;
            }


            var booking = new Models.Booking
            {
                UserId = userIs,
                CarWashStationId = CarWashStationId,
                VehicleId = b.VehicleId,
                BookingDate = DateTime.Parse(b.BookingDate),
                ArrivalTimeStart = TimeSpan.Parse(b.ArrivalTimeStart),
                ArrivalTimeEnd = TimeSpan.Parse(b.ArrivalTimeEnd),
                TotalPrice = b.TotalPrice,
                Note = b.Note,
                CreatedAt = DateTime.UtcNow.ToLocalTime(),
            };
            context.Bookings.Add(booking);
            await context.SaveChangesAsync();
            bookingCount ++;

        }

        var bookingcommissions = rootobject?.Washpass.BookingCommissions;
        var bc = await context.Bookings.ToListAsync();
        var firstBooking = bc.FirstOrDefault();
        var secondBooking = bc.Skip(1).FirstOrDefault();
        var thirdBooking = bc.Skip(2).FirstOrDefault();
        var bookingCountCommissions = 1;

        foreach (var c in bookingcommissions)
        {

            int bookingId;
            if (bookingCountCommissions == 1)
            {
                bookingId = firstBooking.Id;
            }
            else if (bookingCountCommissions == 2)
            {
                bookingId = secondBooking.Id;
            }
            else
            {
                bookingId = thirdBooking.Id;
            }

            var commission = new Models.BookingCommission
            {
                BookingId = bookingId,
                CommissionPercent = c.CommissionPercent,
                CommissionAmount = c.CommissionAmount,
                PaidToAdmin = c.PaidToAdmin,
                CreatedAt = DateTime.UtcNow.ToLocalTime(),
            };
            context.BookingCommissions.Add(commission);
            await context.SaveChangesAsync();
            bookingCountCommissions++;
        }
        var reviews = rootobject?.Washpass.Reviews;
        var reviewCount = 1;
        foreach (var r in reviews)
        {
            var bookingId = 0;
            if (reviewCount == 1)
            {
                bookingId = firstBooking.Id;
            }
            else if (reviewCount == 2)
            {
                bookingId = secondBooking.Id;
            }
            else
            {
                bookingId = thirdBooking.Id;
            }

            var review = new Models.Review
            {
                BookingId = bookingId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = DateTime.UtcNow.ToLocalTime(),
            };
            context.Reviews.Add(review);
            await context.SaveChangesAsync();
            reviewCount++;
        }
        var reviewPhotos = rootobject?.Washpass.ReviewPhotos;
        var reviewCountPhoto = 1;
        foreach (var p in reviewPhotos)
        {
            if (reviewCountPhoto == 1)
            {
                reviewCountPhoto = 1;
            }
            else if (reviewCountPhoto == 2)
            {
                reviewCountPhoto = 2;
            }
            else
            {
                reviewCountPhoto = 3;
            }
            var photo = new Models.ReviewPhoto
            {
                ReviewId = p.ReviewId,
                ImageUrl = p.ImageUrl,
            };
            context.ReviewPhotos.Add(photo);
            await context.SaveChangesAsync();
            reviewCountPhoto++;
        }
        var activityLogs = rootobject?.Washpass.ActivityLogs;
        var activityLogCounter = 1;
        foreach (var a in activityLogs)
        {
            int adminId = 0;
            if (activityLogCounter == 1 || activityLogCounter ==2)
            {
                adminId = firstAdmin.Id;
            }
            else if (activityLogCounter == 3)
            {
                adminId = secondAdmin.Id;
            }
            else
            {
                adminId = secondAdmin.Id;
            }
            var log = new Models.ActivityLog
            {
               AdminId = adminId,
                ActionType = a.ActionType,
                Message = a.Message,
                CreatedAt = DateTime.UtcNow.ToLocalTime(),
            };
            context.ActivityLogs.Add(log);
            await context.SaveChangesAsync();
            activityLogCounter++;
        }
    }
}
