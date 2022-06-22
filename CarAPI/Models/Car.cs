using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarAPI.Models;

public class Car
{
    public Guid Id { get; set; }
    public string Model { get; set; }
    public string Colour { get; set; }
}