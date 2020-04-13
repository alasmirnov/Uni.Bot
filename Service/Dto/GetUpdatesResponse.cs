using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Service.Dto
{
    [DataContract]
    internal class GetUpdatesResponse : TelegramResponseBase
    {
        [DataMember(Name = "result")]
        public IReadOnlyList<UpdateDto> Updates { get; set; }
    }
}