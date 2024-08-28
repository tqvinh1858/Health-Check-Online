using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BHEP.Contract.Constants;
using BHEP.Contract.Enumerations;
using BHEP.Domain.Abstractions.EntityBase;
using static System.Net.Mime.MediaTypeNames;

namespace BHEP.Domain.Entities.UserEntities;
public class DeletionRequest : EntityBase<int>
{
    public int UserId { get; set; }
    public string? Reason { get; set; }
    public DeletionRequestStatus Status { get; set; } = DeletionRequestStatus.Pending;
    public DateTime CreatedDate { get; set; } = TimeZones.SoutheastAsia;
    public DateTime? ProccessedDate { get; set; }

    public virtual User User { get; set; } = null!;


    public void Update(int userId, string? reason, DeletionRequestStatus status, DateTime? proccessedDate)
    {
        UserId = userId;
        Reason = reason;
        Status = status;
        ProccessedDate = proccessedDate;
    }

}
