using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Data
{
    [Table("RefreshToken")]
    public class DTRefreshToken
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }

        public DTNguoiDung NguoiDung { get; set; }

        public string Token { get; set; }

        public string JwtId { get; set; }

        public bool IsUsed { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime ExpireAt { get; set; }

    }
}
