using Gmail.Core.Exceptions;
using Gmail.Core.Models.Mail;
using Gmail.Core.Models.User;
using Gmail.Core.Services;
using Gmail.Infrastructure.Data.Models;

namespace Gmail.Runner
{
    public class Engine
    {
        private UserService userService;
        private MailService mailService;

        public Engine(UserService userService, MailService mailService)
        {
            this.userService = userService;
            this.mailService = mailService;
        }

        public bool RegisterUser(UserCredentials credentials)
        {
            userService.Add(credentials);
            return true;
        }

        public bool ValidateUserCredentials(UserCredentials credentials)
        {
            return userService.ValidateCredentials(credentials);
        }

        public void SendMail(MailDTO mailDto)
        {
            mailService.Add(mailDto);
        }

        public User GetUser(UserCredentials user)
        {
            if (!ValidateUserCredentials(user))
                throw new NotFoundException("User not found!");

            return userService.GetAllAsNoTracking().FirstOrDefault(u => u.Email.Address == user.Email);
        }
    }
}
