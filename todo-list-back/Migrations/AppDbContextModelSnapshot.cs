﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using todo_list_back.Context;

#nullable disable

namespace todo_list_back.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("todo_list_back.Models.Permiso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permisos");
                });

            modelBuilder.Entity("todo_list_back.Models.PermisosRoles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Id_Permisos_FK")
                        .HasColumnType("int");

                    b.Property<int>("Id_Roles_FK")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id_Permisos_FK");

                    b.HasIndex("Id_Roles_FK");

                    b.ToTable("PermisosRoles");
                });

            modelBuilder.Entity("todo_list_back.Models.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("todo_list_back.Models.Tarea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id_Usuarios_FK")
                        .HasColumnType("int");

                    b.Property<string>("Prioridad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id_Usuarios_FK");

                    b.ToTable("Tareas");
                });

            modelBuilder.Entity("todo_list_back.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Id_Roles_FK")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id_Roles_FK");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("todo_list_back.Models.PermisosRoles", b =>
                {
                    b.HasOne("todo_list_back.Models.Permiso", "Permisos")
                        .WithMany("PermisosRoles")
                        .HasForeignKey("Id_Permisos_FK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("todo_list_back.Models.Rol", "Roles")
                        .WithMany("PermisosRoles")
                        .HasForeignKey("Id_Roles_FK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permisos");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("todo_list_back.Models.Tarea", b =>
                {
                    b.HasOne("todo_list_back.Models.Usuario", "Usuarios")
                        .WithMany()
                        .HasForeignKey("Id_Usuarios_FK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("todo_list_back.Models.Usuario", b =>
                {
                    b.HasOne("todo_list_back.Models.Rol", "Roles")
                        .WithMany("Usuarios")
                        .HasForeignKey("Id_Roles_FK")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("todo_list_back.Models.Permiso", b =>
                {
                    b.Navigation("PermisosRoles");
                });

            modelBuilder.Entity("todo_list_back.Models.Rol", b =>
                {
                    b.Navigation("PermisosRoles");

                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
