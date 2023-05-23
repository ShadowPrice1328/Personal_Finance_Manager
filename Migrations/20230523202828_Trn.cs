﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personal_Finance_Manager.Migrations
{
    /// <inheritdoc />
    public partial class Trn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Type",
                table: "Transactions",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "bool");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }
    }
}
