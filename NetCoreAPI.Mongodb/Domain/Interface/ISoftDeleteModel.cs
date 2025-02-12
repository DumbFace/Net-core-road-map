namespace Domain.Interface
{
    public interface ISoftDeleteModel
    {
        bool IsDeleted { get; set; }
    }
}
