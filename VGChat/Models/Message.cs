using System.ComponentModel.DataAnnotations;

namespace VGChat.Models
{
    public class Message
    {
        [Key] public int Id { get; set; }
        [Required] public string Content { get; set; }
        [Required] public string UserName { get; set; }
        [Required] public int GroupId { get; set; }
        public DateTime? MessageSentTime { get; set; } = DateTime.Now;
    }
}
