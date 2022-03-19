

namespace AspNet.OAuth.Providers.Models
{
    public class SocialUserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public object Payload { get; set; }
    }
}
