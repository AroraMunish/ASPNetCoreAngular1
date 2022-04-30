namespace GreatWorld.Services
{
  public interface IMailService
  {
    Guid Id { get; }
    bool SendMail(string to, string from, string subject, string body);
  }

}
