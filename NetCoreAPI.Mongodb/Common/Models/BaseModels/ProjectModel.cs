using Infrastucture.Domain.Enum;
using System.Text.Json.Serialization;

namespace Common.Models.BaseModels
{
    public class ProjectModel 
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ProjectStatus Status { get; set; }


        #region Datetime using UTC Timezone wont send via http
        [JsonIgnore]
        public DateTime StartDate { get; set; }

        [JsonIgnore]
        public DateTime? EndDate { get; set; }
        #endregion

        //Start date timezone vietnamese GMT+7
        public DateTime StartDateCurrentTimeZone { get => TimeZoneInfo.ConvertTimeFromUtc(StartDate, TimeZoneInfo.Local); }

        public DateTime? EndDateCurrentTimeZone { get => EndDate.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(EndDate.Value, TimeZoneInfo.Local) : null;  }
    }
}
