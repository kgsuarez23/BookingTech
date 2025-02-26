namespace Api.Manager.Application.Entities
{
    /// <summary>
    /// Data transfer object representing the result of an authentication operation,
    /// including the authentication token issued upon a successful login.
    /// </summary>
    public class TokenLogin
    {
        /// <summary>
        /// Gets or sets a value indicating whether the login operation was successful.
        /// </summary>
        public bool Result { get; set; } = false;

        /// <summary>
        /// Gets or sets the authentication token generated upon successful login.
        /// </summary>
        public string Token { get; set; }
    }
}
