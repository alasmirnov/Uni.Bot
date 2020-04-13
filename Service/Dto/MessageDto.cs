using System.Runtime.Serialization;

namespace Service.Dto
{
    [DataContract]
    internal class MessageDto
    {
        [DataMember(Name = "message_id")]
        public int Id { get; set; }
        
        [DataMember(Name = "from")]
        public UserDto User { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }
    }
}