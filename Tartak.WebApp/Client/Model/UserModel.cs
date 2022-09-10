namespace Tartak.WebApp.Client.Model
{
    public class UserModel
    {
        public event EventHandler<EventArgs> UserStatusChanged;
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsLoggedIn { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }
        public void UserLoggedIn()
        {
            UserStatusChanged?.Invoke(this, new EventArgs());
        }
        public void UserLoggedOut()
        {
            Id = "";
            Name = "";
            Token = "";
            IsManager = false;
            IsAdmin = false;
            IsLoggedIn = false;
            UserStatusChanged?.Invoke(this, new EventArgs());
        }
    }
}
