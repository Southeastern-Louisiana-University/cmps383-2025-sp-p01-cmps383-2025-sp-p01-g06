using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Selu383.SP25.Api.Entities
{
    public class Theater
    {
        public Guid Id { get; set; }

        [MaxLength(120)]
        public required string Name { get; set; }
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
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public int SeatCount { get; set; }
    public required string Address { get; set; }
}

public class GetTheaterByIdDto
{
    public Guid Id { get; set; }
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
    public Guid Id { get; set; }
}
