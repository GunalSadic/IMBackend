namespace BackendIM.DTO
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }   
        public DateTime expiration { get; set; }    
    }
}
