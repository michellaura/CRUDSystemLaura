namespace CRUDSystem.Migrations
{
    using CRUDSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CRUDSystem.Models.MyDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CRUDSystem.Models.MyDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            IList<Detail> defaultDetail = new List<Detail>();

            defaultDetail.Add(new Detail() { Fname = "Laura", Lname = "Michel",Address="Secret Address1",Age=20,DOB = DateTime.Now });
            defaultDetail.Add(new Detail() { Fname = "Adilene", Lname = "Fernandez", Address = "Secret Address2", Age = 27, DOB = DateTime.Now });
            defaultDetail.Add(new Detail() { Fname = "Victor", Lname = "Orozco", Address = "Secret Address3", Age = 24, DOB = DateTime.Now });


            foreach (Detail std in defaultDetail)
                context.Details.Add(std);

            base.Seed(context);

        }
    }
}
