using Microsoft.AspNetCore.Mvc;
using Sangavy.API.Repository;

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
        string content = $"Name: {request.Name} \nEmail: {request.Email} \nPhone No: {request.PhoneNo} \nMessage: {request.Content}";
        _mailRepository.SentMail(content);
        return Ok(content);
    }
}

public class RequestSendMail
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNo { get; set; }
    public string Content { get; set; }
}