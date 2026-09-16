namespace TicketMaster.Core.Domain.Events;

public enum EventStatus
{
    Draft = 0,
    OnSale = 1,
    SoldOut = 2,
    Cancelled = 3,
    Finished = 4
}