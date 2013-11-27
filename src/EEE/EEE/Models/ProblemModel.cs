using System.Collections.Generic;

namespace EEE.Models
{
    public class ProblemModel : BaseModel
    {
        public string Why { get; set; }
        public string What { get; set; }
        public string When { get; set; }
        public string Where { get; set; }
        public string How { get; set; }
        public List<string> ImageUrls { get; set; }
        public List<string> YoutubeVideoIds { get; set; }

        public bool IsValid(ProblemModel model)
        {
            return !string.IsNullOrEmpty(model.Why)
                   && !string.IsNullOrEmpty(model.What)
                   && !string.IsNullOrEmpty(model.When)
                   && !string.IsNullOrEmpty(model.Where)
                   && !string.IsNullOrEmpty(model.How);
        }
    }
}