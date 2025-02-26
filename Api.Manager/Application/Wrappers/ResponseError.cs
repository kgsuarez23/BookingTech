namespace Api.Manager.Application.Wrappers
{
    /// <summary>
    /// Generic response wrapper for error scenarios, encapsulating error details and a status message.
    /// </summary>
    /// <typeparam name="T">A generic parameter placeholder (not currently used in this class).</typeparam>
    public class ResponseError<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseError{T}"/> class.
        /// </summary>
        public ResponseError() { }

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// In error scenarios, this is typically set to false.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the list of error messages describing the issues encountered during the operation.
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Gets or sets a message associated with the error response. The default message is "Ko".
        /// </summary>
        public string Message { get; set; } = "Ko";
    }

}
