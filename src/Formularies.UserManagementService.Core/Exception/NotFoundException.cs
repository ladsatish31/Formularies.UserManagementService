using System;
using System.Runtime.Serialization;

namespace Formularies.UserManagementService.Core.Exception
{
    [Serializable]
    public class NotFoundException:DomainException
    {
        public NotFoundException()
        {
        }
        public NotFoundException(string message) : base(message)
        {
        }
        public NotFoundException(string message, System.Exception inner) : base(message, inner)
        {
        }
        public NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
