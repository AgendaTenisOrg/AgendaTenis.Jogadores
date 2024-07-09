﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AgendaTenis.Jogadores.Core.AcessoDados.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "jogadores");

            migrationBuilder.CreateTable(
                name: "Jogador",
                schema: "jogadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Sobrenome = table.Column<string>(type: "varchar(100)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: false),
                    IdCidade = table.Column<int>(type: "integer", nullable: false),
                    MaoDominante = table.Column<string>(type: "text", nullable: false),
                    Backhand = table.Column<string>(type: "text", nullable: false),
                    EstiloDeJogo = table.Column<string>(type: "text", nullable: false),
                    PontuacaoAtual = table.Column<double>(type: "double precision", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogador", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jogador",
                schema: "jogadores");
        }
    }
}