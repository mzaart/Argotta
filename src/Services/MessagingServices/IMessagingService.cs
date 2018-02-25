using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Multilang.Models.Db;

namespace Multilang.Services.MessagingServices {
    
    // todo: single is special cas of many. Remove single methods
    public interface IMessagingService
    {
        Task<Boolean> SendMessage(User sender, User recipient, string message);
        Task<Boolean> SendMessages(User sender, List<User> recipients, string message);
        Task<Boolean> SendBinary(User sender, User recipient, string filename, string base64Data);
        Task<Boolean> SendBinaries(User sender, List<User> recipients, string filename, 
            string base64Data);
        Task<Boolean> NotifyPoorTranslation(User sender,User recipient, string message);
        Task<Boolean> SendInvitation(Invitation invitation, User sender, User recipient);
    }
}