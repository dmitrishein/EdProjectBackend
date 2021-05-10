namespace EdProject.BLL.Models.User
{
    public class UserUpdateModel:BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
    }
}
