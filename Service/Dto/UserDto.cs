using System.Runtime.Serialization;

namespace Service.Dto
{
    [DataContract]
    internal class UserDto
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "username")]
        public string Name { get; set; }
    }
}