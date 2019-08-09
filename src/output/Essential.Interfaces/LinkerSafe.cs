using System;
using System.ComponentModel;
using Essential.Interfaces;

[assembly: LinkerSafe]
namespace Essential.Interfaces
{
    class LinkerSafeAttribute : System.Attribute { }

    [AttributeUsage(AttributeTargets.All)]
    internal sealed class PreserveAttribute : Attribute
    {
        public bool AllMembers;
		public bool Conditional;

		public PreserveAttribute(bool allMembers, bool conditional)
		{
			AllMembers = allMembers;
			Conditional = conditional;
		}

		public PreserveAttribute()
		{
		}
    }
}
