using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Symptom;
public static class Query
{
    public record GetSymptomQuery()
        : IQuery<List<Responses.SymptomResponse>>;

    public record GetSymptomByIdQuery(int Id)
    : IQuery<Responses.SymptomResponse>;
}
