using System;
using System.Text.Json.Serialization;

namespace AspNetCore.Authentication.eIDEasy.IDCard.eIDEasy
{
    public class UserData
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("idcode")]
        public string Idcode { get; set; }

        [JsonPropertyName("lastname")]
        public string Lastname { get; set; }

        [JsonPropertyName("firstname")]
        public string Firstname { get; set; }

        [JsonPropertyName("current_login_method")]
        public string CurrentLoginMethod { get; set; }

        [JsonPropertyName("birth_date")]
        public string BirthDate { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("current_login_info")]
        public LoginInfo CurrentLoginInfo { get; set; }
    }

    public class LoginInfo
    {
        [JsonPropertyName("valid_from")]
        public DateTime ValidFrom { get; set; }

        [JsonPropertyName("valid_to")]
        public DateTime ValidTo { get; set; }
    }
}