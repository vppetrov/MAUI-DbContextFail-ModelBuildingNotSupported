using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
	[Table("MyEntities")]
    public class MyEntity
    {
	    public DateTime CreatedTimestamp { get; set; } = DateTime.UtcNow;
	    [Key]
	    public Guid ID { get; set; } = Guid.NewGuid();
	}
}
