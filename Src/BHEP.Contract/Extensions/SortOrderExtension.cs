﻿using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Extensions;
public static class SortOrderExtension
{
    public static SortOrder ConvertStringToSortOrder(string? sortOrder)
        => !string.IsNullOrWhiteSpace(sortOrder)
        ? sortOrder.Trim().ToLower().Equals("asc")
        ? SortOrder.Ascending : SortOrder.Descending : SortOrder.Descending;
}
