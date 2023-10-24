namespace Games.Entities
{
    public class Game
    {
        #region props
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Genre { get; set; }
        public required decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public required string ImageUri { get; set; }
        #endregion


    }
}
