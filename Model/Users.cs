using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointSystem.Model
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name {  get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        // 네비게이션 프로퍼티
        public Points Points { get; set; } 
    }

    public class Points
    {
        [Key]
        public int WalletId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        [Required]
        public int Balance { get; set; } = 0;

        // 네비게이션 프로퍼티
        public Users Users { get; set; }

    }
}
