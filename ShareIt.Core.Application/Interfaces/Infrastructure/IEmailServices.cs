using ShareIt.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Core.Application.Interfaces.Infrastructure
{
    public interface IEmailServices
    {

        public EmailSettings _mailSettings { get; }
        Task SendAsync(EmailRequest request);
    }
}
