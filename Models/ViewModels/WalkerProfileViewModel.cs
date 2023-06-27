namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Walker Walker{ get; set; }
        public List<Walk> Walks { get; set; }
    }
}
