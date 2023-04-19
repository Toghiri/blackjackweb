using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blackjack.Database.Entities
{
    [Table("stats")]
    public class Stats
    {
        [Column("id"), Key]
        public int Id { get; set; }
        [Column("player_score")]
        public int PlayerScore { get; set; }
        [Column("dealer_score")]
        public int DealerScore { get; set; }
        [Column("player_hand", TypeName = "jsonb")]
        public string PlayerHand { get; set; }
        [Column("dealer_hand", TypeName = "jsonb")]
        public string DealerHand { get; set; }
    }
}
