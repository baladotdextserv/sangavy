using Microsoft.AspNetCore.Mvc;
using Sangavy.API.Repository;
using System.Threading.Tasks;

namespace Sangavy.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SendMailController : ControllerBase
{
    private MailRepository _mailRepository;
    public SendMailController(MailRepository mailRepository) { 
        _mailRepository = mailRepository;
    }

    [HttpPost("/send-mail")]
    public IActionResult GetMessage([FromBody] RequestSendMail request)
    {
        //if (request == null || !ModelState.IsValid)
        //{
        //    return BadRequest("Invalid request data.");
        //}

        string content = $"Name: {request.Name}\nEmail: {request.Email}\nPhone No: {request.PhoneNo}\nMessage: {request.Content}";

        bool result = _mailRepository.SentMail(content);

        if (result)
        {
            return Ok(request);
        }

        return BadRequest("Failed to send mail.");
    }
}

public class RequestSendMail
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNo { get; set; }
    public string? Content { get; set; }
}