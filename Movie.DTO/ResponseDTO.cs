using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Movie_store.DTO
{
    public class BaseResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
