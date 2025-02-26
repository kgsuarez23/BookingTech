namespace Api.Manager.Application.Wrappers
{
    /// <summary>
    /// Generic response wrapper used to encapsulate the results of operations,
    /// including a success flag, data collection, count of data items, and a message.
    /// </summary>
    /// <typeparam name="T">The type of data contained in the response.</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Response{T}"/> class.
        /// </summary>
        public Response()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response{T}"/> class with the specified count and data.
        /// The response is marked as successful.
        /// </summary>
        /// <param name="count">The count of data items.</param>
        /// <param name="data">The collection of data items (optional).</param>
        public Response(int count, IEnumerable<T> data = null)
        {
            Data = data;
            Success = true;
            Count = count;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response{T}"/> class with the specified error message.
        /// The response is marked as unsuccessful.
        /// </summary>
        /// <param name="message">The error message.</param>
        public Response(string message)
        {
            Success = false;
            Count = 0;
            Message = message;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the collection of data items.
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Gets or sets the count of data items.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets a message associated with the response. The default message is "Ok".
        /// </summary>
        public string Message { get; set; } = "Ok";
    }
}
