using Domain.EFCore.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastucture.EFCore.EntityConfig
{
    public class EntityLinkTypeConfig : IEntityTypeConfiguration<LinkType>
    {
        public void Configure(EntityTypeBuilder<LinkType> builder)
        {
            builder.ToTable("LinkTypes", linkType => linkType.ExcludeFromMigrations(true));
        }
    }
}
