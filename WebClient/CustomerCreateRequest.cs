namespace WebClient
{
    public class CustomerCreateRequest
    {
        public CustomerCreateRequest()
        {
        }

        public CustomerCreateRequest(
           
            string firstName,
            string lastName)
        {
            Firstname = firstName;
            Lastname = lastName;
        }
        public long Id { get; set; }

        public string Firstname { get; init; }

        public string Lastname { get; init; }
    }
}