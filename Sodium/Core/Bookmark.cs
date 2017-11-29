namespace Sodium.Core
{
    public class Bookmark
    {
        public string Address { get; set; }
        public int Port { get; set; } = 22;
        public string WebUrl { get; set; }
        public string Username { get; set; }
        public string StartupPath { get; set; }
    }
}
