
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    class Kingdom
    {
        public int Id { get; set; }
        public int Level { get; set; } = 1;
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        public ICollection<Player> Players { get; set; } = new List<Player>();

    }
}
