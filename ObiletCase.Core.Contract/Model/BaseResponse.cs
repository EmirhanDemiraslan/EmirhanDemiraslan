using System;
namespace ObiletCase.Core.Contract.Model
{
	public class BaseResponse
	{
        public bool IsError { get; set; }
        public string ResponseJson { get; set; }
    }
}

