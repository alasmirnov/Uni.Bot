using System.Runtime.Serialization;

namespace Service.Dto
{
    [DataContract]
    internal class GetMeResponse : TelegramResponseBase
    {
        [DataMember(Name = "result")]
        public UserDto Me { get; set; }
    }
}