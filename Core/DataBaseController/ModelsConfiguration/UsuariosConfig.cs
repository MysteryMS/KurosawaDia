﻿using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseController.ModelsConfiguration
{
    internal sealed class UsuariosConfig : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            //Table
            builder.ToTable("Usuarios");
            //Cod
            builder.HasKey(x => x.Cod);
            builder.Property(x => x.Cod).HasColumnName("codigo_usuario").HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
            //ID
            builder.Property(x => x.ID).HasColumnName("id_usuario").IsRequired();
            builder.HasIndex(x => x.ID).IsUnique();
            //Nome
            builder.Property(x => x.Nome).HasColumnName("nome_usuario").HasColumnType("varchar(255)").HasCharSet("utf8mb4").IsRequired();
        }
    }
}
