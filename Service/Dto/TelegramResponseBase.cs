using System;
using System.Runtime.Serialization;

namespace Service.Dto
{
    [DataContract]
    internal class TelegramResponseBase
    {
        [DataMember(Name = "ok")]
        public bool Success { get; set; }

        [DataMember(Name = "description", IsRequired = false)]
        public string Error { get; set; }

        public void CheckException()
        {
            if (!Success)
                throw new Exception(Error);
        }
    }
}