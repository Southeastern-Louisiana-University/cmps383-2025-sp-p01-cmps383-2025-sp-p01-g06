using System.ComponentModel.DataAnnotations;

namespace Selu383.SP25.Api.Entities
{
    public class Theater
    {
       public int Id { get; set; }
        [MaxLength(120)]
       public string Name { get; set; }
       public int SeatCount { get; set; }
       public required string Address { get; set; }
    }
}

public class CreateTheaterDto
{
    [MaxLength(120)]
    string Name { get; set; }
    int SeatCount { get; set; }
    string Address { get; set; }
}

public class GetTheaterDto
{
    int Id { get; set; }
    string Name { get; set; }
    int SeatCount { get; set; }
    string Address { get; set; }
}

public class GetTheaterByIdDto
{
    int Id { get; set; }
    string Name { get; set; }
    int SeatCount { get; set; }
    string Address { get; set; }
}

public class UpdateTheaterDto
{
    [MaxLength(120)]
    string Name { get; set; }
    int SeatCount { get; set; }
    string Address { get; set; }
}   

public class DeleteTheaterDto
{
    int Id { get; set; }
}


