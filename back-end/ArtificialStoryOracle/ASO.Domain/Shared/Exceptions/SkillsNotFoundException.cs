using System.Net;
using ASO.Domain.Shared.Messages;

namespace ASO.Domain.Shared.Exceptions;

public class SkillsNotFoundException : DomainException
{
    public SkillsNotFoundException()
        : base(HttpStatusCode.NotFound, ErrorMessages.SkillNotFound)
    {
    }
}