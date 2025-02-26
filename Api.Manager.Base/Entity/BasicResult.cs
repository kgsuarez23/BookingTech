namespace Api.Manager.Base.Entity
{
    /// <summary>
    /// Represents the basic outcome of an operation, including a result flag and a descriptive message.
    /// </summary>
    public class BasicResult
    {
        /// <summary>
        /// Gets or sets a message that describes the outcome of the operation.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool Result { get; set; }
    }

}
