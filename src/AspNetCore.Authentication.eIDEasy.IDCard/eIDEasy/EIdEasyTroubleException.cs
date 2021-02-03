using System;

namespace AspNetCore.Authentication.eIDEasy.IDCard.eIDEasy
{
    public class EIdEasyTroubleException : Exception
    {
        public EIdEasyTroubleException(string message) : base(message)
        {
        }
    }
}