using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Multilang.Models.Db;

namespace Multilang.Services.MessagingServices {
    
    // todo: single is special cas of many. Remove single methods
    public interface IMessagingService
    {
        Task<Boolean> SendMessage(string senderId, string senderLangCode, User recipient, 
            string message);
         Task<Boolean> SendMessages(string senderId, string senderLangCode, List<User> recipients, 
            string message);
        Task<Boolean> SendBinary(string senderId, string senderLangCode, User recipient, 
            string filename, string base64Data);
        Task<Boolean> SendBinaries(string senderId, string senderLangCode, List<User> recipients, 
            string filename, string base64Data);
        Task<Boolean> NotifyPoorTranslation(string senderDislayName, string senderLanguage,
            User recipient, string message);
    }
}