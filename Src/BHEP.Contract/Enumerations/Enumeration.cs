namespace BHEP.Contract.Enumerations;

public enum Gender
{
    Male = 1,
    Female = 2,
    Others = 3
}

public enum ApplicationStatus
{
    Processing = 1,
    Approve = 2,
    Cancel = -1,
}

public enum AppointmentStatus
{
    Cancel = -1,
    Waiting = 0,
    Received = 1,
    Completed = 2,
    Refused = 3
}

public enum UserRole
{
    Admin = 1,
    Customer = 2,
    Employee = 3
}

public enum ServiceType
{
    Individual = 1,
    Family = 2,
}

public enum TimeExpired
{
    ThreeMonth = 3,
    SixMonth = 6,
    TwelveMoth = 12,
}

public enum PaymentStatus
{
    Pending = 1,
    Completed = 2,
    Canceled = -1
}

public enum BlogStatus
{
    Draft = 1,
    Pending = 2,
    Published = 3,
    Canceled = -1,
}

public enum PostStatus
{
    Pending = 1,
    Published = 2,
    Canceled = -1,
}

public enum DeletionRequestStatus
{
    Pending = 1,
    Approved = 2,
    Rejected = -1
}

public enum CoinTransactionType
{
    Appointment = 1,
    Order = 2,
    Deposit = 3
}
