namespace AntiFraud.API.Options
{
    public class SMTPOptions
    {
        public string Server { get; set; }

        public int Port { get; set; }

        public bool UseSSL { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string AdminMail { get; set; }
    }
}
