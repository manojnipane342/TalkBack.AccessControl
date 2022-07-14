namespace Game.Data.Models
{
    public partial class Game2
    {
        public Guid Id { get; set; }

        public string? InitiatingPlayer { get; set; }

        public string? Oponent { get; set; }

        public bool? Status { get; set; }

    }
}