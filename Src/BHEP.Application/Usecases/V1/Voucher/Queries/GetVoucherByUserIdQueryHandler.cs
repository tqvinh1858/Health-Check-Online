using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Services.V1.Voucher;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BHEP.Application.Usecases.V1.Voucher.Queries;
public sealed class GetVoucherByUserIdQueryHandler : IQueryHandler<Query.GetVoucherByUserIdQuery, PagedResult<Responses.VoucherResponse>>
{
    private readonly IVoucherRepository voucherRepository;
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    public GetVoucherByUserIdQueryHandler(
        IVoucherRepository voucherRepository,
        ApplicationDbContext context,
        IMapper mapper)
    {
        this.voucherRepository = voucherRepository;
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.VoucherResponse>>> Handle(Query.GetVoucherByUserIdQuery request, CancellationToken cancellationToken)
    {
        var Events = await context.UserVoucher
                   .Where(x => x.UserId == request.UserId)
                   .Select(x => x.Voucher)
                   .Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
                   .Take(request.PageSize.Value).ToListAsync();

        var responses = mapper.Map<List<Responses.VoucherResponse>>(Events);

        var result = PagedResult<Responses.VoucherResponse>.Create(
            responses,
            request.PageIndex.Value,
            request.PageSize.Value,
            Events.Count());

        return Result.Success(result);
    }
}
