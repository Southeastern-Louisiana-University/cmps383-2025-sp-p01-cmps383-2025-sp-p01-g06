using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Selu383.SP25.Api.Entities
{
    public class Theater
    {
       public int Id { get; set; }
        
       public required string Name { get; set; }
       [Range(1, int.MaxValue, ErrorMessage = "Seat count must be at least 1.")]
        public int SeatCount { get; set; }
       public required string Address { get; set; }
    }
}

public class CreateTheaterDto
{
    public required string Name { get; set; }
    public int SeatCount { get; set; }
    public required string Address { get; set; }
}

public class GetTheaterDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int SeatCount { get; set; }
    public required string Address { get; set; }
}

public class GetTheaterByIdDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int SeatCount { get; set; }
    public required string Address { get; set; }
}

public class UpdateTheaterDto
{
    public required string Name { get; set; }
    public int SeatCount { get; set; }
    public required string Address { get; set; }
}   

public class DeleteTheaterDto
{
    public int Id { get; set; }
}


