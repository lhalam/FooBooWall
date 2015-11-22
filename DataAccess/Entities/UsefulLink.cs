namespace DataAccess.Entities
{
    public class UsefulLink
    {
        public int Id { get; set; }
        public int OwnerUserID{ get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? ImageId{ get; set; }
        public string Comment { get; set; }
    }
}