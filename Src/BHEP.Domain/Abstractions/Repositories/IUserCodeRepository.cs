﻿using BHEP.Domain.Entities.SaleEntities;

namespace BHEP.Domain.Abstractions.Repositories;
public interface IUserCodeRepository
{
    Task Add(UserCode userCode);
}
