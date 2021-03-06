﻿using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseController.ModelsConfiguration
{
    internal sealed class Servidores_UsuariosConfig : IEntityTypeConfiguration<Servidores_Usuarios>
    {
        public void Configure(EntityTypeBuilder<Servidores_Usuarios> builder)
        {
            //Table
            builder.ToTable("Servidores_Usuarios");
            builder.HasKey(x => new { x.ServidorCod, x.UsuarioCod });
            //ServidorCod
            builder.Property(x => x.ServidorCod).HasColumnName("Servidores_codigo_servidor");
            //Servidor
            builder.HasOne(x => x.Servidor).WithMany(x => x.ServidoresUsuarios).HasForeignKey(x => x.ServidorCod).IsRequired();
            //UsuarioCod
            builder.Property(x => x.UsuarioCod).HasColumnName("Usuarios_codigo_usuario");
            //Usuario
            builder.HasOne(x => x.Usuario).WithMany(x => x.ServidoresUsuarios).HasForeignKey(x => x.UsuarioCod).IsRequired();
        }
    }
}
