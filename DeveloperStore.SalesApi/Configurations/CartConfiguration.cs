﻿using DeveloperStore.SalesApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.SalesApi.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasMany(c => c.Products).WithOne(ci => ci.Cart).HasForeignKey(ci => ci.CartId);
    }
}
