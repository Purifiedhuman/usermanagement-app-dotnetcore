using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Entities;

namespace UserService.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("aspnet_users");
        builder.Property(p => p.Meta).HasColumnType("jsonb");
        builder.Property(p => p.isDeleted).HasDefaultValue(false);

        builder.HasData(
            DataGenerator()
        );
    }

    private IEnumerable<ApplicationUser> DataGenerator()
    {
        Randomizer.Seed = new Random(8675309);

        var users = new Faker<ApplicationUser>()
            .RuleFor(u => u.UserName, f => f.Internet.UserName())
            .RuleFor(u => u.NormalizedUserName, (f, u) => u.UserName.ToUpper())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.UserName))
            .RuleFor(u => u.NormalizedEmail, (f, u) => u.Email.ToUpper())
            .RuleFor(u => u.EmailConfirmed, f => f.Random.Bool())
            .RuleFor(u => u.PasswordHash, f => f.Random.Hash())
            .RuleFor(u => u.SecurityStamp, f => f.Random.Hash())
            .RuleFor(u => u.ConcurrencyStamp, f => f.Random.Hash())
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(u => u.PhoneNumberConfirmed, f => f.Random.Bool())
            .Generate(100);

        return users;

    }
}
