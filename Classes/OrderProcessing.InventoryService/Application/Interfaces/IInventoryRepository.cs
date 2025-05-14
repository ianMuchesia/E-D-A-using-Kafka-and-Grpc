



namespace OrderProcessing.InventoryService.Application.Interfaces;


public interface IInventoryRepository
{
    Task<bool> ReserveItemsAsync(Guid reservationId, List<Domain.Entities.ReservationItem> items);
    Task<bool> ReleaseItemsAsync(Guid reservationId);
    Task<List<Domain.Entities.ReservationItem>> GetReservationItemsAsync(Guid reservationId);

    Task<Domain.Entities.Reservation?> GetReservationByIdAsync(Guid reservationId);

    Task<bool> AddReservationAsync(Domain.Entities.Reservation reservation);

    Task<bool> UpdateReservationAsync(Domain.Entities.Reservation reservation);


    Task<bool> DeleteReservationAsync(Guid reservationId);


    Task<bool> ItemExistsAsync(Guid itemId);

    Task<Domain.Entities.Reservation?> GetByOrderIdAsync(Guid orderId);


    Task<IEnumerable<Domain.Entities.ReservationItem>> GetAllItemsAsync();
    Task<Domain.Entities.ReservationItem?> GetItemByIdAsync(Guid itemId);


    Task<bool> AddItemAsync(Domain.Entities.ReservationItem item);


    Task<bool> UpdateItemAsync(Domain.Entities.ReservationItem item);

    Task<bool> DeleteItemAsync(Guid itemId);

    Task SaveChangesAsync();


}