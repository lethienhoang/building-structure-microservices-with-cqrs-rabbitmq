namespace App.Services.Identity.Contract.Requests
{
    public class SignInRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
