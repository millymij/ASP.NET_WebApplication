using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using WebApplication3;
using WebApplication3.Data;

{

// This class sets up and start the ASP.NET core application. It creates the web host, configures services and HTTP requests.

    static void Main(string[] args)
    {
        // open connection with database
        using (var db = new WebApplication3Context())
        {
            // user input
            Console.WriteLine("Enter a country name:");
            var countryName = Console.ReadLine();

            var country = new Country { Name = countryName };
            db.Countries.Add(country);
            db.SaveChanges();

            // user input
            Console.WriteLine("Enter a street address (1):");
            var streetAddress1 = Console.ReadLine();
            Console.WriteLine("Enter a street address (2):");
            var streetAddress2 = Console.ReadLine();
            Console.WriteLine("Enter a region:");
            var region = Console.ReadLine();
            Console.WriteLine("Enter a post code:");
            var postCode = Console.ReadLine();

            var address = new Address
            {
                StreetAddress1 = streetAddress1,
                StreetAddress2 = streetAddress2,
                Region = region,
                PostCode = postCode,
            };

            db.Addresses.Add(address);
            db.SaveChanges();

            // display all addresses in the database
            var query = from a in db.Addresses
                        orderby a.PostCode
                        select a;
            Console.WriteLine("All addresses in the database:");
            foreach (var item in query)
            {
                Console.WriteLine(item.StreetAddress1 + " " + item.StreetAddress2 + " " + item.Region + " " + item.PostCode);
            }

            // exit prompt
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}


// ASP.NET Core web application with Entity Framework Core, Newtonsoft.Json for JSON serialization, and CORS configuration. 

// Create and configure builder web host
var builder = WebApplication.CreateBuilder(args);

// Add services (Razor pages) to the container. Configure JSON serialisation settings
builder.Services.AddRazorPages().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});

// service to add data context with SQL server
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

// set up CORS to allow requests and responses from URL
builder.Services.AddCors(policyBuilder =>
policyBuilder.AddDefaultPolicy(policy =>
policy.WithOrigins("http://localhost:7148/").AllowAnyHeader()));

//build WebApplication instance
var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days.
    app.UseHsts();
}

app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();