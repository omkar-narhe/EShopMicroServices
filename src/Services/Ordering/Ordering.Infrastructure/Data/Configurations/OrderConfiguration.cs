using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasConversion(
                id => id.Value,
                value => OrderId.Of(value));

        // Many customers to one order
        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId);

        // One order to many OrderItems
        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(
            o => o.OrderName,
            orderNameBuilder =>
            {
                orderNameBuilder.Property(on => on.Value)
                    .HasColumnName("OrderName")
                    .HasMaxLength(100)
                    .IsRequired();
            });

        builder.ComplexProperty(
            o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50);

                addressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(100)
                .IsRequired();

                addressBuilder.Property(a => a.Country)
                .HasMaxLength(50);

                addressBuilder.Property(a => a.State)
                .HasMaxLength(50);

                addressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });

        builder.ComplexProperty(
            o => o.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(a => a.LastName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(a => a.EmailAddress)
                .HasMaxLength(50);

                addressBuilder.Property(a => a.AddressLine)
                .HasMaxLength(100)
                .IsRequired();

                addressBuilder.Property(a => a.Country)
                .HasMaxLength(50);

                addressBuilder.Property(a => a.State)
                .HasMaxLength(50);

                addressBuilder.Property(a => a.ZipCode)
                .HasMaxLength(5)
                .IsRequired();
            });

        builder.ComplexProperty(
            o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardName)
                    .HasMaxLength(50)
                    .IsRequired();

                paymentBuilder.Property(p => p.CardNumber)
                    .HasMaxLength(24)
                    .IsRequired();
                
                paymentBuilder.Property(p => p.Expiration)
                    .HasMaxLength(10)
                    .IsRequired();
                paymentBuilder.Property(p => p.CVV)
                    .HasMaxLength(3)
                    .IsRequired();
            });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Pending)
            .HasConversion(
                s => s.ToString(),
                value => (OrderStatus)Enum.Parse(typeof(OrderStatus), value));

        builder.Property(o => o.TotalPrice);
    }
}
