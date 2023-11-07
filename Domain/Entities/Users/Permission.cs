namespace Domain.Entities.Users
{
    public class Permission
    {
        public bool Read { get; }
        public bool Write { get; }
        public bool Edit { get; }
        public bool Delete { get; }

        public Permission(bool read, bool write, bool edit, bool delete)
        {
            Read = read;
            Write = write;
            Edit = edit;
            Delete = delete;
        }
    }
}
