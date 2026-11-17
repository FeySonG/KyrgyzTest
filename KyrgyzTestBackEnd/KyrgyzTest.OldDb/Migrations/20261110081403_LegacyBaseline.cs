using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KyrgyzTest.OldDb.Migrations
{
    /// <inheritdoc />
    public partial class LegacyBaseline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Baseline: существующая схема базы данных уже создана.
            // Здесь намеренно нет операций изменения схемы.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Откат baseline не должен удалять существующие таблицы.
        }
    }
}