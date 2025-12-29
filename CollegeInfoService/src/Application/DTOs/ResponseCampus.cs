namespace Application.DTOs
{
    public record ResponseCampus
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
        public string Address { get; init; } = default!;
        public string? ContactPhone { get; init; }
        public string? ContactEmail { get; init; }
        // Không cần ánh xạ CampusAcademicFields nếu không muốn trả về
    }
}