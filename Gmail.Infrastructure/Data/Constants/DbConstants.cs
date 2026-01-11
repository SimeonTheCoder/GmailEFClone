namespace Gmail.Infrastructure.Data.Constants
{
    public static class DbConstants
    {
        public static class MailConstants
        {
            public const int MaxSubjectLength = 200;
            public const int MaxContentLength = 2000;
        }

        public static class UserConstants
        {
            public const int MaxNameLength = 250;
        }

        public static class EmailAddressConstants
        {
            public const int MaxEmailLength = 255;
        }
    }
}
