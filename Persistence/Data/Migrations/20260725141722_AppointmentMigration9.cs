using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentMigration9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkingHours_ServiceProviderId_DayOfWeek",
                table: "WorkingHours");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_ServiceProviderId",
                table: "WorkingHours",
                column: "ServiceProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkingHours_ServiceProviderId",
                table: "WorkingHours");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_ServiceProviderId_DayOfWeek",
                table: "WorkingHours",
                columns: new[] { "ServiceProviderId", "DayOfWeek" },
                unique: true);
        }
    }
}
