namespace e_commerceUserServiceBackend.DTO
{
    public class ApiResponseDTO
    {
        public bool Successs { get; set; } = true;
        public string? Data { get; set; }
        public string? Token { get; set; }
        public string? Msg { get; set; }
    }
}
