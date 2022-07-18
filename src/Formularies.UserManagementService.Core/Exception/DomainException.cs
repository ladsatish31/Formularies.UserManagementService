using System;
using System.Runtime.Serialization;

namespace Formularies.UserManagementService.Core.Exception
{
    [Serializable]
    public class DomainException:System.Exception
    {
        public DomainException()
        {
        }
        public DomainException(string message):base(message)
        {
        }
        public DomainException(string message,System.Exception inner) : base(message,inner)
        {
        }
        public DomainException(SerializationInfo info, StreamingContext context): base(info,context)
        {
        }

    }
}
