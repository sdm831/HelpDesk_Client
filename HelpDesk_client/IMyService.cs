using System.ServiceModel;

namespace HelpDesk_client
{
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        bool AddSubscriber(string fio, string phone);

        [OperationContract]
        string[] TryGetSubscribers(string data);

        [OperationContract]
        bool DeleteSubscriber(string fio);




        [OperationContract]
        bool AddRequest(string type, string text, string subscriber, string source);


        [OperationContract]
        string[] TryGetRequests(string input);


        [OperationContract]
        bool DeleteRequest(string id);


    }
}