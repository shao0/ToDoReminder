namespace ToDoReminder.Share
{
    public class ApiResponse
    {
        public ApiResponse()
        {

        }
        public ApiResponse(string message) : this()
        {
            Message = message;
        }
        public ApiResponse(object o) : this("请求成功")
        {
            Status = true;
            Result = o;
        }
        public string Message { get; set; }
        public bool Status { get; set; }
        public object Result { get; set; }

    }
    public class ApiResponse<T>: ApiResponse
    {
        public ApiResponse(string message) : base()
        {
            Message = message;
        }
        public new T Result { get; set; }
    }
}
