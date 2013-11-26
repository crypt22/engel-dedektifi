using System.Collections.Generic;

namespace EEE.Domain.Entities
{
    public class Problem : BaseEntity
    {
        public string Title { get; set; }
        public string WhoId { get; set; }
        public string WhoEmail { get; set; }
        public string What { get; set; }
        public string When { get; set; }
        public string Where { get; set; }
        public string How { get; set; }
        public List<string> ImageUrls { get; set; }
        public List<string> YoutubeVideoIds { get; set; }
    }
}