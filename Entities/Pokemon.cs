﻿
namespace AspNetPokeAPi.Entities
{
    public partial class Pokemon
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }

    }
}
