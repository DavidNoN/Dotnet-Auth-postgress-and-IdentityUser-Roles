using System;

namespace UserApi.Entities
{
    public record PhotoEntity
    {
        public Guid PhotoId { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
    }
}