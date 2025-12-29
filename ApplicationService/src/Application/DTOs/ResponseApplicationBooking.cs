namespace Application.DTOs
{
    public class ResponseApplicationBooking
    {
        public string Id { get; set; } = default!;
        public string UserFullName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string UserPhoneNumber { get; set; } = default!;
        public string UserBirthDate { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public string Province { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string School { get; set; } = default!;
        public int GraduationYear { get; set; } = default!;
        public string Campus { get; set; } = default!;
        public string InterestedAcademicField { get; set; } = default!;
        public int MathScore { get; set; } = default!;
        public int EnglishScore { get; set; } = default!;
        public int LiteratureScore { get; set; } = default!;
        public string Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;
    }
}
