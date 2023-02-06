using System.Net.Mail;

namespace MyCore.Common.Extensions
{
    public static class CommonExtensions
    {
        /// <summary>
        /// mail adress format control 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}
