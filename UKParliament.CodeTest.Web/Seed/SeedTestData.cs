using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Web.Seed
{
    public static class SeedTestData
    {
        public static void AddData(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<PersonManagerContext>();

            db?.People.Add(new Person { Id = 1, Name = "Matt Smith", Address ="17 Avenue London" , DateOfBirth = new DateTime(2011, 10, 10), Email = "matt@gmail.com"});
            db?.People.Add(new Person { Id = 2, Name = "Jane Taylor",  Address = "16 Bedford London", DateOfBirth = new DateTime(2000, 10, 10), Email = "jane@gmail.com" } );
            db?.People.Add(new Person { Id = 3, Name = "Liam Francis", Address = "15 South London", DateOfBirth = new DateTime(2005, 10, 10), Email = "liam@gmail.com" });

            db?.SaveChanges();
        }
    }
}
