namespace App.Services.Identity.Infrastructure.Constants
{
    public static class MessagesCode
    {
        public const string InvalidPassword = "Password can not be empty.";
        public const string InvalidEmail = "Invalid email: {0}";
        public const string EmailInUse = "Email: {0} is already in use.";
        public const string UserNotFound = "User with id: {0} was not found.";
        public const string InvalidCurrentPassword = "Invalid current password.";
        public const string InvalidCredentials = "Invalid credentials";
        public const string RefreshTokenAlreadyRevoked = "Refresh token: {0} was already revoked at {1}.";
        public const string RefreshTokenNotFound = "Refresh token was not found.";
    }
}
