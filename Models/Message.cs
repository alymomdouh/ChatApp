using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class Message
    {
        public Message()
        {
            When = DateTime.Now;
        }

        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime When { get; set; }= DateTime.Now;
        public string UserId { get; set; }
        public virtual AppUser Sender { get; set; }
    }
}
