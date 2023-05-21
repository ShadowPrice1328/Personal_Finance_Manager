using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Personal_Finance_Manager.Migrations
{
    /// <inheritdoc />
    public partial class pfm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Transactions MODIFY COLUMN Type ENUM('Expenses', 'Revenues') NOT NULL DEFAULT 'Expenses';");
        }
    }
}
