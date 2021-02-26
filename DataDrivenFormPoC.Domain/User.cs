using System.Collections.Generic;

namespace DataDrivenFormPoC.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public IList<Form> Forms { get; set; }

        public User()
        {
            this.Forms = new List<Form>();
        }
    }
}
