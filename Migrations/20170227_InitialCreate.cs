using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoutingSample.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoredValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        //upgrading from Npgsql.EntityFrameworkCore.PostgreSQL 1.0.0
                        //caused error on following line
                        .Annotation("Npgsql:ValueGenerationStrategy", true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredValues", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoredValues");
        }
    }
}