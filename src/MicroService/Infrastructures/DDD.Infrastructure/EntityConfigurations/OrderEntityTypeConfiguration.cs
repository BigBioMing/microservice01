using DDD.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(n => n.UserId).HasMaxLength(20).IsRequired();
            builder.Property(n => n.UserName).HasMaxLength(30).IsRequired();
            builder.OwnsOne(n => n.Address, a =>
            {
                //a.WithOwner();//加上这行，address的字段与存储在order表中，名称为Address_{字段名}
                a.ToTable("address");//不加a.WithOwner();,则存储在address表中，用外键与order表关联，外键为OrderId
                a.Property(n => n.Street).HasMaxLength(30);
                a.Property(n => n.City).HasMaxLength(30);
                a.Property(n => n.ZipCode).HasMaxLength(30);
            });
        }
    }
}
