using MessageRequest.Data.Models;

namespace MessageRequest.Data.Services
{
    public interface IMessageService
    {
        Message PostMessage (Message message);
        
        //NotifyUser,SaveChat
    }
}
