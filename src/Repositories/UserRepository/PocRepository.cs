using Multilang.Models.Accounts;
using System;

namespace Multilang.Repositories.UserRepository {

    public class UserRepository : IUserRepository
    {
        private readonly User arabic = new User {
            id = "0",
            name = "Arabic",
            passwordHash = "Arabic",
            language = "Arabic",
            langCode = "ar",
            firebaseToken = "d1HBOGgSALI:APA91bFet-Isi1BqEXbFolj88EjP53GsvbOcqBsdgyiMhhwyGi7gYOR1eJ6SqV2dC0G_oGzyE4RP357lFkgEiZIweYhUJR6TmiORLXc_u1LJa6JGS_1AWAFp7anlrgzaKtgB6CCvHPYY"
        };

        private readonly User english = new User {
            id = "1",
            name = "English",
            passwordHash = "English",
            language = "English",
            langCode = "en",
            firebaseToken = "cVSVtEh1_5E:APA91bEyyshwzlYdqmQUX6hH8b80QPMEq8-UkOCitlhwjvr2qMh1zKEjYQTKn17Ht6YwemQSBu7esVQFrmwui4phY84-kShAkHXMuhsnytzzpr-9k04rABLdYL-MdyQBgj7Zh-K_gdwV"

        };

        private readonly User french = new User {
            id = "2",
            name = "French",
            passwordHash = "French",
            language = "French",
            langCode = "fr",
            firebaseToken = "d5-lcwlWkWs:APA91bHu1zKHxAYSmU-GJFiUzTBibUNWQ3oSL7wQyV6FX4zIhiX0Fhz70Dy4d5s-OEwoP5OGHkNLUZ4a4vdXNDOwNFJXa2-J4g2S-3gRWdEJk6ZTINnbg9Vdr1HxaOBGigNH3R5FvQd5"
        };

        User IUserRepository.GetUserById(string id)
        {
            switch(id) {
                case("0"):
                    return arabic;
                case("1"):
                    return english;
                case("2"):
                    return french;
                default:
                    throw new System.Exception("User does not exist.");
            }
        }

        void IUserRepository.UpdateFirebaseToken(string id, string token)
        {
            switch(id) {
                case("0"):
                    arabic.firebaseToken = token;
                    return;
                case("1"):
                    english.firebaseToken = token;
                    return;
                case("2"):
                    french.firebaseToken = token;
                    return;
                default:
                    throw new System.Exception("User does not exist.");
            }
        }
    }
}