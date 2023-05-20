using System.ComponentModel.DataAnnotations;
namespace VGChat_demo.Models
{
    public class Message
    {
        [Key] public int Id { get; set; }
        [Required] public string Content { get; set; }
        [Required] public string userName { get; set; }
        
        public DateTime? MessageSentTime { get; set; }
    }
}
