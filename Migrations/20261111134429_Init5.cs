using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimuladoIronFit.Api.Migrations
{
    /// <inheritdoc />
    public partial class Init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GymMembers_Personals_PersonalId1",
                table: "GymMembers");

            migrationBuilder.DropIndex(
                name: "IX_GymMembers_PersonalId1",
                table: "GymMembers");

            migrationBuilder.DropColumn(
                name: "PersonalId1",
                table: "GymMembers");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Personals",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalId",
                table: "GymMembers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_GymMembers_PersonalId",
                table: "GymMembers",
                column: "PersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_GymMembers_Personals_PersonalId",
                table: "GymMembers",
                column: "PersonalId",
                principalTable: "Personals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GymMembers_Personals_PersonalId",
                table: "GymMembers");

            migrationBuilder.DropIndex(
                name: "IX_GymMembers_PersonalId",
                table: "GymMembers");

            migrationBuilder.AlterColumn<int>(
                name: "CPF",
                table: "Personals",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalId",
                table: "GymMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonalId1",
                table: "GymMembers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GymMembers_PersonalId1",
                table: "GymMembers",
                column: "PersonalId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GymMembers_Personals_PersonalId1",
                table: "GymMembers",
                column: "PersonalId1",
                principalTable: "Personals",
                principalColumn: "Id");
        }
    }
}
