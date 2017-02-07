using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LCARS.Migrations
{
    public partial class AddedSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BuildServerPassword = table.Column<string>(nullable: true),
                    BuildServerUrl = table.Column<string>(nullable: true),
                    BuildServerUsername = table.Column<string>(nullable: true),
                    DeploymentsServerKey = table.Column<string>(nullable: true),
                    DeploymentsServerUrl = table.Column<string>(nullable: true),
                    GitHubPassword = table.Column<string>(nullable: true),
                    GitHubUsername = table.Column<string>(nullable: true),
                    IssuesPassword = table.Column<string>(nullable: true),
                    IssuesUrl = table.Column<string>(nullable: true),
                    IssuesUsername = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
