using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class changeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaTa",
                table: "HangHoa");

            migrationBuilder.AlterColumn<byte>(
                name: "GiamGia",
                table: "HangHoa",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "GiamGia",
                table: "HangHoa",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaTa",
                table: "HangHoa",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
