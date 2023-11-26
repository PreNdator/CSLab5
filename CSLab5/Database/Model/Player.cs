

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    class Player
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public PlayerStats? Stats { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Balance { get; set; } = 0;
        public int? KingdomId {  get; set; } 
        public  Kingdom? Kingdom { get; set; }
        public ICollection<Achievement> Achievement { get; set; } = new List<Achievement>();

    }
}
