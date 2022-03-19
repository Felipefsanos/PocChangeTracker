namespace PocChangeTracker.Entities
{
    public class Client
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }

        internal void Update(Client modifiedClient)
        {
            Name = modifiedClient.Name;
            LastName = modifiedClient.LastName;
            Email = modifiedClient.Email;
            Birthday = modifiedClient.Birthday;
        }
    }
}
