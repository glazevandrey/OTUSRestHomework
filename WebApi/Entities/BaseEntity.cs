using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
