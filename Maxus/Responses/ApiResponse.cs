﻿namespace AccountingAPI.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public ApiResponse(T data, string message = "", bool success = true)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = new List<string>();
        }

        public ApiResponse(IEnumerable<string> errors, string message = "")
        {
            Success = false;
            Message = message;
            Errors = errors ?? new List<string>();
            Data = default;
        }
    }
}
