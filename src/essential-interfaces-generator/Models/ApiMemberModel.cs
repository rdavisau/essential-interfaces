using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;

namespace EssentialInterfaces.Models
{
    public class ApiMemberModel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public MemberKind Kind { get; set; }
        public string ReturnType { get; set; }
        public string Identifier { get; set; }
        public string ArgsString { get; set; }
        internal string ArgsArgs => // hehe xd pls dont @ me
            String.Join(", ",
                ArgsString
                    .Split(',')
                    .Select(a => a.Replace("(", "").Replace(")", "").Replace("params", "").Trim())
                    .Select(a => a == "" ? "" : a.Split(' ')[1]));
    }
}