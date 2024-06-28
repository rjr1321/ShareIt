using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareIt.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IdProfileFore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coments_Profiles_ProfileIdUser",
                table: "Coments");

            migrationBuilder.DropIndex(
                name: "IX_Coments_ProfileIdUser",
                table: "Coments");

            migrationBuilder.DropColumn(
                name: "ProfileIdUser",
                table: "Coments");

            migrationBuilder.AlterColumn<string>(
                name: "IdProfile",
                table: "Coments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Coments_IdProfile",
                table: "Coments",
                column: "IdProfile");

            migrationBuilder.AddForeignKey(
                name: "FK_Coments_Profiles_IdProfile",
                table: "Coments",
                column: "IdProfile",
                principalTable: "Profiles",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coments_Profiles_IdProfile",
                table: "Coments");

            migrationBuilder.DropIndex(
                name: "IX_Coments_IdProfile",
                table: "Coments");

            migrationBuilder.AlterColumn<string>(
                name: "IdProfile",
                table: "Coments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ProfileIdUser",
                table: "Coments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Coments_ProfileIdUser",
                table: "Coments",
                column: "ProfileIdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Coments_Profiles_ProfileIdUser",
                table: "Coments",
                column: "ProfileIdUser",
                principalTable: "Profiles",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
