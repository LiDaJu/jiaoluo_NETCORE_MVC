using Microsoft.EntityFrameworkCore.Migrations;

namespace jiaoluo.Migrations
{
    public partial class sendStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "ClassName", "Email", "Name" },
                values: new object[] { 1, 1, "111@123.com", "张三" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "ClassName", "Email", "Name" },
                values: new object[] { 2, 2, "221@123.com", "张四" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "ClassName", "Email", "Name" },
                values: new object[] { 3, 3, "233@123.com", "张五" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
