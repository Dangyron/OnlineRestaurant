using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineRestaurant.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToDishImagesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "DishImages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<List<DateTime>>(
                name: "UpdateDates",
                table: "DishImages",
                type: "timestamp with time zone[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "DishImages");

            migrationBuilder.DropColumn(
                name: "UpdateDates",
                table: "DishImages");
        }
    }
}
