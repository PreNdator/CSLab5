
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    class Achievement
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Reward { get; set; }
        public ICollection<Player> PlayerAchievement { get; set; } = new List<Player>();
    }
}
