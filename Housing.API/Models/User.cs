namespace Housing.API.Models
{
    public class User:BaseEntity
    {
       
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordKey { get; set; }
    }
}
