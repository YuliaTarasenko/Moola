using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Moola.Models
{
    public record EntityWithId
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
    }
}
