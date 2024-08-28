namespace BHEP.Domain.Abstractions.EntityBase;
public interface IUserTracking
{
    int CreatedBy { get; set; }
    int UpdatedBy { get; set; }
}
