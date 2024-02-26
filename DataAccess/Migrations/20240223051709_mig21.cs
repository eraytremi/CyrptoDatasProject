using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Volumeto",
                table: "PastDatas",
                newName: "VolumeByParity");

            migrationBuilder.RenameColumn(
                name: "VolumeFrom",
                table: "PastDatas",
                newName: "VolumeByCurrencyCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VolumeByParity",
                table: "PastDatas",
                newName: "Volumeto");

            migrationBuilder.RenameColumn(
                name: "VolumeByCurrencyCount",
                table: "PastDatas",
                newName: "VolumeFrom");
        }
    }
}
