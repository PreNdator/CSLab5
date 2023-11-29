
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Kingdom
    {
        public int Id { get; set; }
        public int Level { get; set; } = 1;
        [Required(ErrorMessage = "Имя королевства обязательно для заполнения")]
        [StringLength(30, ErrorMessage = "Имя должно содержать не более 30 символов")]
        public string Name { get; set; } = null!;
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();

    }
}
