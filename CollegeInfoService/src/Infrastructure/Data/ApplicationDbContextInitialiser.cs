using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
    public async Task TrySeedAsync()
    {
        if (_context.Campus.Any()) return;

        var hcmCampus = new Campus { Id = Guid.NewGuid(), Name = "Hồ Chí Minh Campus", Address = "Hồ Chí Minh", Description = "FPT University Hồ Chí Minh", ContactPhone = "028-1234-5678", ContactEmail = "hcm@fpt.edu.vn" };
        var hnCampus = new Campus { Id = Guid.NewGuid(), Name = "Hà Nội Campus", Address = "Hà Nội", Description = "FPT University Hà Nội", ContactPhone = "024-1234-5678", ContactEmail = "hn@fpt.edu.vn" };
        var dnCampus = new Campus { Id = Guid.NewGuid(), Name = "Đà Nẵng Campus", Address = "Đà Nẵng", Description = "FPT University Đà Nẵng", ContactPhone = "0236-1234-5678", ContactEmail = "dn@fpt.edu.vn" };
        var qnCampus = new Campus { Id = Guid.NewGuid(), Name = "Quy Nhơn Campus", Address = "Quy Nhơn", Description = "FPT University Quy Nhơn", ContactPhone = "0256-1234-5678", ContactEmail = "qn@fpt.edu.vn" };
        var ctCampus = new Campus { Id = Guid.NewGuid(), Name = "Cần Thơ Campus", Address = "Cần Thơ", Description = "FPT University Cần Thơ", ContactPhone = "0292-1234-5678", ContactEmail = "ct@fpt.edu.vn" };

        var campuses = new[] { hcmCampus, hnCampus, dnCampus, qnCampus, ctCampus };

        var fieldMap = new Dictionary<string, AcademicField>
        {
            ["Công nghệ thông tin"] = new AcademicField { Id = Guid.NewGuid(), Name = "Công nghệ thông tin" },
            ["Quản trị kinh doanh"] = new AcademicField { Id = Guid.NewGuid(), Name = "Quản trị kinh doanh" },
            ["Công nghệ truyền thông"] = new AcademicField { Id = Guid.NewGuid(), Name = "Công nghệ truyền thông" },
            ["Luật"] = new AcademicField { Id = Guid.NewGuid(), Name = "Luật" },
            ["Ngôn ngữ Anh"] = new AcademicField { Id = Guid.NewGuid(), Name = "Ngôn ngữ Anh" },
            ["Ngôn ngữ Nhật"] = new AcademicField { Id = Guid.NewGuid(), Name = "Ngôn ngữ Nhật" },
            ["Ngôn ngữ Trung Quốc"] = new AcademicField { Id = Guid.NewGuid(), Name = "Ngôn ngữ Trung Quốc" },
            ["Ngôn ngữ Hàn Quốc"] = new AcademicField { Id = Guid.NewGuid(), Name = "Ngôn ngữ Hàn Quốc" }
        };

        var campusAcademicFields = campuses
            .SelectMany(campus => fieldMap.Values.Select(field => new CampusAcademicField
            {
                CampusId = campus.Id,
                AcademicFieldId = field.Id
            }))
            .ToList();

        var specializationData = new Dictionary<string, string[]>
        {
            ["Công nghệ thông tin"] = new[]
            {
            "Kỹ thuật phần mềm", "Hệ thống thông tin", "Trí tuệ nhân tạo", "An toàn thông tin",
            "Công nghệ ô tô số", "Thiết kế vi mạch bán dẫn", "Thiết kế mỹ thuật số"
        },
            ["Quản trị kinh doanh"] = new[]
            {
            "Digital Marketing", "Kinh doanh quốc tế", "Quản trị khách sạn",
            "Quản trị dịch vụ du lịch & lữ hành", "Tài chính doanh nghiệp",
            "Ngân hàng số - Tài chính", "Công nghệ tài chính (Fintech)",
            "Tài chính đầu tư", "Logistics & quản lý chuỗi cung ứng toàn cầu"
        },
            ["Công nghệ truyền thông"] = new[]
            {
            "Truyền thông đa phương tiện", "Quan hệ công chúng"
        },
            ["Luật"] = new[]
            {
            "Luật kinh tế", "Luật thương mại quốc tế"
        },
            ["Ngôn ngữ Anh"] = new[] { "Ngôn ngữ Anh" },
            ["Ngôn ngữ Nhật"] = new[] { "Song ngữ Nhật – Anh" },
            ["Ngôn ngữ Trung Quốc"] = new[] { "Song ngữ Trung – Anh" },
            ["Ngôn ngữ Hàn Quốc"] = new[] { "Song ngữ Hàn – Anh" }
        };

        var specializations = new List<Specialization>();

        foreach (var caf in campusAcademicFields)
        {
            var fieldName = fieldMap.First(f => f.Value.Id == caf.AcademicFieldId).Key;

            if (!specializationData.ContainsKey(fieldName)) continue;

            foreach (var specName in specializationData[fieldName])
            {
                specializations.Add(new Specialization
                {
                    Id = Guid.NewGuid(),
                    Name = specName,
                    Description = $"{specName} specialization",
                    CampusAcademicField = caf
                });
            }
        }

        _context.Campus.AddRange(campuses);
        _context.AcademicFields.AddRange(fieldMap.Values);
        _context.CampusAcademicFields.AddRange(campusAcademicFields);
        _context.Specializations.AddRange(specializations);

        await _context.SaveChangesAsync();
    }

}
