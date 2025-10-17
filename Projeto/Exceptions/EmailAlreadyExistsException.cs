namespace Projeto.Exceptions
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException()
            : base("Account creation failed. Please try again.") { }
    }
}
