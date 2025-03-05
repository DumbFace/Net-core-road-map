using Infrastucture.Domain.Enum;

namespace Common.Models.BaseModels
{
    public class ProjectModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ProjectStatus Status { get; set; }
    }
}
