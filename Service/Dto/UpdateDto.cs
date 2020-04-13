using System.Runtime.Serialization;

namespace Service.Dto
{
    [DataContract]
    internal class UpdateDto
    {
        [DataMember(Name = "update_id")]
        public int Id { get; set; }
        
        [DataMember(Name = "message")]
        public MessageDto Message { get; set; }
    }
}