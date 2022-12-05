namespace BookStoreVer2.Lib.Models
{
    public partial class Authorization
    {
        public int Id { get; set; }
        public int IdEmployee { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public virtual Employee IdEmployeeNavigation { get; set; } = null!;
    }
}
