using BHEP.Contract.Enumerations;
using BHEP.Domain.Abstractions.EntityBase;

namespace BHEP.Domain.Entities.UserEntities;
public class Schedule : EntityBase<int>
{
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }

    public virtual User Employee { get; set; }


    public void Update(DateTime date, string time)
    {
        Date = date;
        Time = time;
    }



}
