namespace BHEP.Contract.Constants;
public static class TableName
{
    #region AppointmentTable
    public const string Appointment = nameof(Appointment);
    public const string AppointmentSymptom = nameof(AppointmentSymptom);
    public const string Specialist = nameof(Specialist);
    public const string SpecialistSymptom = nameof(SpecialistSymptom);
    public const string Symptom = nameof(Symptom);
    #endregion

    #region BlogTable
    public const string Blog = nameof(Blog);
    public const string BlogPhoto = nameof(BlogPhoto);
    public const string BlogRate = nameof(BlogRate);
    public const string BlogTopic = nameof(BlogTopic);
    public const string Topic = nameof(Topic);
    #endregion

    #region PostTable
    public const string Post = nameof(Post);
    public const string PostSpecialist = nameof(PostSpecialist);
    public const string Comment = nameof(Comment);
    public const string CommentLike = nameof(CommentLike);
    public const string PostLike = nameof(PostLike);
    public const string Reply = nameof(Reply);
    public const string ReplyLike = nameof(ReplyLike);
    #endregion

    #region SaleTable
    public const string Code = nameof(Code);
    public const string Duration = nameof(Duration);
    public const string Device = nameof(Device);
    public const string Order = nameof(Order);
    public const string ProductTransaction = nameof(ProductTransaction);
    public const string ServiceTransaction = nameof(ServiceTransaction);
    public const string Payment = nameof(Payment);
    public const string Product = nameof(Product);
    public const string ProductRate = nameof(ProductRate);
    public const string Service = nameof(Service);
    public const string ServiceRate = nameof(ServiceRate);
    public const string Voucher = nameof(Voucher);
    public const string VoucherTransaction = nameof(VoucherTransaction);
    #endregion

    #region UserEntities
    public const string GeoLocation = nameof(GeoLocation);
    public const string JobApplication = nameof(JobApplication);
    public const string Role = nameof(Role);
    public const string Schedule = nameof(Schedule);
    public const string User = nameof(User);
    public const string UserCode = nameof(UserCode);
    public const string UserRate = nameof(UserRate);
    public const string UserVoucher = nameof(UserVoucher);
    public const string WorkProfile = nameof(WorkProfile);
    public const string HealthRecord = nameof(HealthRecord);
    public const string DeletionRequest = nameof(DeletionRequest);
    #endregion

    public const string CoinTransaction = nameof(CoinTransaction);
    public const string Major = nameof(Major);
}
