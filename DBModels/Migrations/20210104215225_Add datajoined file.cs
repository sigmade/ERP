using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DBModels.Migrations
{
    public partial class Adddatajoinedfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_File_Person_PersonId",
                table: "File");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateJoined",
                table: "File",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "entity.Property(e => e.Name);",
                table: "File",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "entity.Property(e => e.Name);",
                table: "File");

            migrationBuilder.DropColumn(
                name: "DateJoined",
                table: "File");

            migrationBuilder.AddForeignKey(
                name: "FK_File_Person_PersonId",
                table: "File",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
