namespace UserAPI.Interfaces
{
    public interface ISendVerificationMail
    {
        void SmtpServer(string email, string userName);
    }
}