using Microsoft.EntityFrameworkCore.Migrations;

namespace SULS.Data.Migrations
{
    public partial class RemovedPasswordLengthConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
