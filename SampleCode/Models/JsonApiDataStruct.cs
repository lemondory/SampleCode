namespace Manager.Models
{
    public class CharacterInfo
    {
        public long AUID { get; set; }
        public string UserName { get; set; }
        public long CUID { get; set; }

        public CharacterInfo(long auid, long cuid, byte[] username)
        {
            this.AUID = auid;
            this.UserName = (username == null && username.Length == 0) ? string.Empty : System.Text.Encoding.UTF8.GetString(username);
            this.CUID = cuid;
        }
    }
}
