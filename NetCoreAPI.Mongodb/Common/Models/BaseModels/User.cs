using AutoMapper.Configuration.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Common.Models.BaseModels
{
    public class User
    {
        public int Id { get; set; }

        public string AboutMe { get; set; }

        [NotMapped]
        public IEnumerable<Badge> Badges { get => JsonConvert.DeserializeObject<IEnumerable<Badge>>(JsonBadges); }

        public string JsonBadges { get; set; }
    }
}
