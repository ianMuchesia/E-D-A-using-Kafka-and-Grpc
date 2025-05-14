using OrderProcessing.InventoryService.AppDbContext;
using OrderProcessing.InventoryService.Application.Interfaces;
using OrderProcessing.InventoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace OrderProcessing.InventoryService.Infrastructure;


public class InventoryRepository : IInventoryRepository
{

    private readonly InventoryDbContext _context;

    public InventoryRepository(InventoryDbContext context)
    {
        _context = context;
    }


    public async Task<bool> AddItemAsync(ReservationItem item)
    {
        await _context.ReservationItems.AddAsync(item);
        return true;
    }

    public async Task<bool> AddReservationAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
        return true;
    }

    public async Task<bool> DeleteItemAsync(Guid itemId)
    {
        var item = await _context.ReservationItems.FindAsync(itemId);
        if (item == null)
        {
            return false;
        }

        _context.ReservationItems.Remove(item);
        return true;
    }

    public async Task<bool> DeleteReservationAsync(Guid reservationId)
    {
        var reservation = await _context.Reservations.FindAsync(reservationId);
        if (reservation == null)
        {
            return false;
        }

        _context.Reservations.Remove(reservation);
        return true;
    }

    public async Task<IEnumerable<ReservationItem>> GetAllItemsAsync()
    {
        return await _context.ReservationItems.ToListAsync();
    }

    public async Task<Reservation?> GetByOrderIdAsync(Guid orderId)
    {
        return await _context.Reservations.FirstOrDefaultAsync(r => r.OrderId == orderId);
    }

    public async Task<ReservationItem?> GetItemByIdAsync(Guid itemId)
    {
        return await _context.ReservationItems.FindAsync(itemId);
    }

    public async Task<Reservation?> GetReservationByIdAsync(Guid reservationId)
    {
        return await _context.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
            
            
    }

    public async Task<List<ReservationItem>> GetReservationItemsAsync(Guid reservationId)
    {
        return await _context.ReservationItems
            .Where(i => i.ReservationId == reservationId)
            .ToListAsync();
    }

    public async Task<bool> ItemExistsAsync(Guid itemId)
    {
        return await _context.ReservationItems.AnyAsync(i => i.Id == itemId);
    }

    public async Task<bool> ReleaseItemsAsync(Guid reservationId)
    {
        var items = await _context.ReservationItems
            .Where(i => i.ReservationId == reservationId)
            .ToListAsync();

        if (items.Count == 0)
        {
            return false;
        }

        _context.ReservationItems.RemoveRange(items);
        return true;
    }

    public  Task<bool> ReserveItemsAsync(Guid reservationId, List<ReservationItem> items)
    {

        throw new NotImplementedException();
       
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateItemAsync(ReservationItem item)
    {
         _context.ReservationItems.Update(item);
         await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateReservationAsync(Reservation reservation)
    {
        _context.Reservations.Update(reservation);
        await _context.SaveChangesAsync();

        return true;
    }
}