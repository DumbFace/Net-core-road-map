using Newtonsoft.Json;

namespace Common.Models.BaseModels
{
    public class UserModel
    {
        public int Id { get; set; }

        public string AboutMe { get; set; }

        //[NotMapped]
        public IEnumerable<Badge> Badges { get => JsonConvert.DeserializeObject<IEnumerable<Badge>>(JsonBadges); }

        public string JsonBadges { get; set; }
    }
}
