using Infrastucture.Domain.EFCore.Entites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Domain.EFCore.Entites
{
    [Table("Country")]
    public class Country : BaseEntity
    {
        [MaxLength(50)]
        [AllowNull]
        public string City { get; set; }

        [MaxLength(50)]
        [AllowNull]
        public string CityName { get; set; }

        [MaxLength(50)]
        [AllowNull]
        public string State { get; set; }

        [MaxLength(10)]
        [AllowNull]
        public string CountryCode { get; set; }
    }
}
