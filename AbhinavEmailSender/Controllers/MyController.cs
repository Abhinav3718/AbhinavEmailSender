using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AbhinavEmailSender.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MyController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("api/my/sendmail")]
        public IActionResult SendMail()
        {
            MailSender mailSender = new MailSender("test subject", "test body", _configuration);
            mailSender.SendMail();
            return Ok();
        } 
    }
}
