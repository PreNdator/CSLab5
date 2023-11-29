

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Player
    {
        public Player()
        {
            Stats = new PlayerStats()
            {
                Player = this
            };
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [StringLength(30, ErrorMessage = "Имя должно содержать не более 30 символов")]
        public string Username { get; set; } = null!;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public virtual PlayerStats? Stats { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        [Range(typeof(decimal), "0", "99999999", ErrorMessage = "Баланс должен быть от 0 до 99999999")]
        public decimal Balance { get; set; } = 0;
        public int? KingdomId {  get; set; } 
        public virtual Kingdom? Kingdom { get; set; }
        public virtual ICollection<Achievement> Achievement { get; set; } = new List<Achievement>();

    }
}
