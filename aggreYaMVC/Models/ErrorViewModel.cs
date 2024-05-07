namespace aggreYaMVC.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    [Serializable]
    public class TransportException : Exception
    {
        public int StatusCode { get; set; }
        public string Content { get; set; }
    }
}
