using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TTTN_Travel.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ADMIN",
                columns: table => new
                {
                    IDAD = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    USERNAME = table.Column<string>(maxLength: 50, nullable: true),
                    PASSWORK = table.Column<string>(maxLength: 50, nullable: true),
                    IMAGE = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADMIN", x => x.IDAD);
                });

            migrationBuilder.CreateTable(
                name: "DATTOUR",
                columns: table => new
                {
                    HOTEN = table.Column<string>(maxLength: 50, nullable: false),
                    TENTUOR = table.Column<string>(maxLength: 50, nullable: false),
                    SDT = table.Column<string>(maxLength: 15, nullable: false),
                    DC = table.Column<string>(maxLength: 50, nullable: false),
                    EMAIL = table.Column<string>(maxLength: 50, nullable: false),
                    DATE = table.Column<DateTime>(type: "datetime", nullable: false),
                    SONGUOI = table.Column<int>(nullable: false),
                    GHICHU = table.Column<string>(maxLength: 50, nullable: true),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DATTOUR", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KH",
                columns: table => new
                {
                    IDKH = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HOTEN = table.Column<string>(maxLength: 50, nullable: true),
                    SDT = table.Column<string>(maxLength: 15, nullable: true),
                    DC = table.Column<string>(maxLength: 50, nullable: true),
                    EMAIL = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KH", x => x.IDKH);
                });

            migrationBuilder.CreateTable(
                name: "TOUR",
                columns: table => new
                {
                    TENTUOR = table.Column<string>(maxLength: 150, nullable: true),
                    IMAGE = table.Column<string>(maxLength: 50, nullable: true),
                    NGAYBD = table.Column<DateTime>(type: "datetime", nullable: true),
                    NGAYKT = table.Column<DateTime>(type: "datetime", nullable: true),
                    MOTA = table.Column<string>(nullable: true),
                    GIA = table.Column<decimal>(type: "money", nullable: true),
                    QUOCGIA = table.Column<string>(maxLength: 50, nullable: true),
                    TRONGNUOC = table.Column<bool>(nullable: true),
                    IDTOUR = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LICHTRINH = table.Column<string>(nullable: true),
                    DANHGIA = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOUR", x => x.IDTOUR);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADMIN");

            migrationBuilder.DropTable(
                name: "DATTOUR");

            migrationBuilder.DropTable(
                name: "KH");

            migrationBuilder.DropTable(
                name: "TOUR");
        }
    }
}
