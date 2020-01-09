using System.Collections.Generic;

namespace EssentialInterfaces.Models
{
    public class ApiModel
    {
        public string Namespace { get; set; }
        public string Api { get; set; }
        public string Interface => $"I{Api}";
        public List<ApiMemberModel> Declarations { get; set; } = new List<ApiMemberModel>();
        public List<string> OtherTypes { get; set; }
    }
}