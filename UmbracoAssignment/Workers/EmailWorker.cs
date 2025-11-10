using Azure.Communication.Email;
using Azure.Messaging.ServiceBus;
using UmbracoAssignment.Dto;

namespace UmbracoAssignment.Workers;

public class EmailWorker : BackgroundService
{
    private readonly ServiceBusProcessor _processor;
    private readonly EmailClient _email;
    private readonly string _from;

    public EmailWorker(ServiceBusClient sb, IConfiguration cfg)
    {
        var queueName = cfg["Azure:ServiceBus:QueueName"]
            ?? throw new ArgumentNullException("Azure:ServiceBus:QueueName");

        _processor = sb.CreateProcessor(queueName, new ServiceBusProcessorOptions
        {
            MaxConcurrentCalls = 2,
            AutoCompleteMessages = false
        });

        var acsConn = cfg["Azure:Communication:Email:ConnectionString"]
            ?? throw new ArgumentNullException("Azure:Communication:Email:ConnectionString");
        _email = new EmailClient(acsConn);

        _from = cfg["Azure:Communication:Email:FromAddress"]
            ?? throw new ArgumentNullException("Azure:Communication:Email:FromAddress");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _processor.ProcessMessageAsync += async args =>

        {
            var payload = args.Message.Body.ToObjectFromJson<FormSubmissionMessage>();
            if (payload is null || string.IsNullOrWhiteSpace(payload.RecipientEmail))
            {
                await args.CompleteMessageAsync(args.Message);
                return;
            }
            var content = new EmailContent("Sugoi!! We have received your message, senpai! 💖") //Extra cringe from chatCPT
            {
                PlainText = "UwU~ Your message has reached our secret ninja village! We'll reply faster than Naruto running to Ichiraku Ramen!! 💌💨",
                Html = @"<p style='font-family: Comic Sans MS, cursive; color: #ff66b2;'>
                        <b>~ Kon’nichiwa Senpai!! ~</b> 💫✨<br/><br/>
                        UwU!! We have received your super kawaii message!! It has been safely delivered to our hidden leaf mail village 🌸🐾<br/><br/>
                        Our team of certified anime protagonists are now channeling their <i>chakra of productivity</i> 💪🍜 to read it with 
                        <span style='color:#ff3399;'>1000% focus no jutsu!!</span><br/><br/>
                        Please wait patiently, like a true ninja waiting for the next filler arc to end... ⏳💔<br/><br/>
                        Until then, keep your spirit strong, your heart tsundere, and your Wi-Fi connection baka-proof! 💻💞<br/><br/>
                        <b>Arigatō gozaimasu for contacting us, senpai!!</b> 🙏😳💖<br/>
                        <i>~ The UwU Support Squad no Jutsu 💌✨</i>
                    </p>"
            };

            var msg = new EmailMessage(
                _from,
                new EmailRecipients(new[] { new EmailAddress(payload.RecipientEmail) }),
                content
            );

            await _email.SendAsync(Azure.WaitUntil.Started, msg, args.CancellationToken);
            await args.CompleteMessageAsync(args.Message);

        };


        _processor.ProcessErrorAsync += err =>
        {
            Console.Error.WriteLine(err.Exception);
            return Task.CompletedTask;
        };

        return _processor.StartProcessingAsync(stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _processor.StopProcessingAsync(cancellationToken);
        await base.StopAsync(cancellationToken);
    }
}
