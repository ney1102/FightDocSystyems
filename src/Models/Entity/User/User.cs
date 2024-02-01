


namespace Models.Entity.User
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int Created_by { get; set; }
        public int? Updated_by { get; set; }
    }
}
