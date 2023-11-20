using System;

namespace DutchTreat.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string ArtDescription { get; set; }
        public string ArtDating { get; set; }
        public string ArtID { get; set; }
        public string Artist { get; set; }
        public DateTime ArtistBirthDate { get; set;}
        public DateTime AtristDeathDate { get; set;}
        public string AtristNationality { get; set; }
    }
}
