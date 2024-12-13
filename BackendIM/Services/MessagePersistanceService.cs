namespace BackendIM.Services
{
    public interface IMessagePersistanceService
    {

    }

    public class MessagePersistanceService
    {
        //This class should persist messages after they're being sent inside of a ChatHub, no matter whether the user is online or offline. 
        //The entrypoint should also be made from the SendMessage method withing the hub.
    }
}
