using System.ComponentModel.DataAnnotations.Schema;

namespace API_WITH_EF
{
    [Table("Worker")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
