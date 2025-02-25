using Newtonsoft.Json;

namespace Common.Models.BaseModels
{
    public class User
    {
        public int Id { get; set; }

        public string AboutMe { get; set; }

        //[NotMapped]
        public IEnumerable<Badge> Badges { get => JsonConvert.DeserializeObject<IEnumerable<Badge>>(JsonBadges); }

        //public IEnumerable<Badge> Badges { get; set; }


        public string JsonBadges { get; set; }
    }
}
