namespace TicketMaster.Core.BuildingBlocks.Queries;

public record QueryBase
{
    public virtual int PageNumber { get; set; } = 1;

    public virtual int PageSize { get; set; } = 10;

    public virtual int SkipCount => (PageNumber - 1) * PageSize;

    public virtual bool NeedTotalCount { get; set; }

    public virtual string SortBy { get; set; } = "Id";

    public virtual bool SortAscending { get; set; }
}