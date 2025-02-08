using System.ComponentModel.DataAnnotations;

namespace Selu383.SP25.Api.Entities
{
    public class Theater
    {
       public int Id { get; set; }
        [MaxLength(120)]
       public required string Name { get; set; }
       public int SeatCount { get; set; }
       public required string Address { get; set; }
    }
}

public class CreateTheaterDto
{
    [MaxLength(120)]
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
    [MaxLength(120)]
    public required string Name { get; set; }
    public int SeatCount { get; set; }
    public required string Address { get; set; }
}   

public class DeleteTheaterDto
{
    public int Id { get; set; }
}


