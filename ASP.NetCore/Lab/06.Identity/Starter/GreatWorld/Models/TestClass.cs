using GreatWorld.Services;
using System.Diagnostics;

namespace GreatWorld.Models
{
  public class TestClass
  {
    private IMailService _mailService;

    public TestClass(IMailService mailService)
    {
      _mailService = mailService;
      Debug.WriteLine("TestController: Using mailService class id::" + _mailService.Id);
    }
    public string TestMsg()
    {
      Debug.WriteLine("TestController:TestMsg(): Using mailService class id::" + _mailService.Id);

      return "Test Succeeded";
    }

  }
}
