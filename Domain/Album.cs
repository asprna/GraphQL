namespace Domain
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
		public int ArtistId { get; set; }
		public Artist Artist { get; set; }
	}
}