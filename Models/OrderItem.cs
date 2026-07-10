using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TableServe.Api.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; } = 1;
    [StringLength(200)]
    public string? Notes { get; set; }

    public int OrderId { get; set; }
    [JsonIgnore]
    public Order? Order { get; set; }

    public int MenuItemId { get; set; }
    public MenuItem? MenuItem { get; set; }
}