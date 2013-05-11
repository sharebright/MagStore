namespace MagStore.Web.Models
{
    public class EditUserModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
        public string[] UserRoles { get; set; }

        public EditUserModel()
        { }

        public EditUserModel(string username, string email, string[] roles, string[] userRoles)
        {
            Username = username;
            Email = email;
            Roles = roles;
            UserRoles = userRoles;
        }
    }
}