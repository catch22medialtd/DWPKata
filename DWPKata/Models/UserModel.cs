namespace DWPKata.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IPAddress { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}