﻿using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseController.ModelsConfiguration
{
    internal sealed class AdmsBotConfig : IEntityTypeConfiguration<AdmsBot>
    {
        public void Configure(EntityTypeBuilder<AdmsBot> builder)
        {
            //Table
            builder.ToTable("AdmsBot");
            //Cod
            builder.HasKey(x => x.Cod);
            builder.Property(x => x.Cod).HasColumnName("cod").HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
            //Usuario
            builder.HasOne(x => x.Usuario).WithMany(x => x.AdmsBots).HasForeignKey(x => x.CodUsuario).IsRequired();
            //CodUsuario
            builder.Property(x => x.CodUsuario).HasColumnName("usuario");
            //Permissao
            builder.Property(x => x.Permissao).HasColumnName("permissao").HasColumnType("tinyint").HasConversion<byte>().IsRequired();
        }
    }
}
