namespace AppWeb.Application
{
    public class CurrentUser
    {

        public CurrentUser(string id, string name, string email, string phone)
        {
            UseId = id;
            UserName = name;
            UserEmail = email;
            UserPhoneNumber = phone;
        }


        public string UseId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public string UserPhoneNumber { get; set; }

    }


}
