namespace BHEP.Domain.Abstractions.EntityBase;
public interface ISoftDelete
{
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; }

    public void Undo()
    {
        DeletedDate = null;
        IsDeleted = false;
    }
}
