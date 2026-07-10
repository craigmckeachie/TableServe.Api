using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableServe.Api.Models;

public class Order
{
    public int Id { get; set; }
    public int TableNumber { get; set; }
    [StringLength(200)]
    public string? Notes { get; set; }
    [StringLength(10)]
    public string Status { get; set; } = OrderStatus.Placed;
    [StringLength(200)]
    public string? CancellationReason { get; set; }
    [Column(TypeName = "decimal(11,2)")]
    public decimal Total { get; set; }
    public DateTime OrderedAt { get; set; } = DateTime.Now;

    public int StaffId { get; set; }
    public Staff? Staff { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}