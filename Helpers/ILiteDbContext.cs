namespace ASPNETWebApplication.Helpers
{
    public interface ILiteDbContext
    {
        LiteDB.LiteDatabase Database { get; }
    }
}