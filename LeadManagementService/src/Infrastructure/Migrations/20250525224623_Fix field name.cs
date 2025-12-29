using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fixfieldname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignment_Lead_LeadId",
                table: "LeadAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadInteraction_Lead_LeadId",
                table: "LeadInteraction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadInteraction",
                table: "LeadInteraction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadAssignment",
                table: "LeadAssignment");

            migrationBuilder.DropIndex(
                name: "IX_LeadAssignment_LeadId",
                table: "LeadAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lead",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "LeadInteraction");

            migrationBuilder.RenameTable(
                name: "LeadInteraction",
                newName: "LeadInteractions");

            migrationBuilder.RenameTable(
                name: "LeadAssignment",
                newName: "LeadAssignments");

            migrationBuilder.RenameTable(
                name: "Lead",
                newName: "Leads");

            migrationBuilder.RenameIndex(
                name: "IX_LeadInteraction_LeadId",
                table: "LeadInteractions",
                newName: "IX_LeadInteractions_LeadId");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "LeadAssignments",
                newName: "ConsultantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadInteractions",
                table: "LeadInteractions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadAssignments",
                table: "LeadAssignments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leads",
                table: "Leads",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssignments_LeadId",
                table: "LeadAssignments",
                column: "LeadId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignments_Leads_LeadId",
                table: "LeadAssignments",
                column: "LeadId",
                principalTable: "Leads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadInteractions_Leads_LeadId",
                table: "LeadInteractions",
                column: "LeadId",
                principalTable: "Leads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignments_Leads_LeadId",
                table: "LeadAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadInteractions_Leads_LeadId",
                table: "LeadInteractions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leads",
                table: "Leads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadInteractions",
                table: "LeadInteractions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadAssignments",
                table: "LeadAssignments");

            migrationBuilder.DropIndex(
                name: "IX_LeadAssignments_LeadId",
                table: "LeadAssignments");

            migrationBuilder.RenameTable(
                name: "Leads",
                newName: "Lead");

            migrationBuilder.RenameTable(
                name: "LeadInteractions",
                newName: "LeadInteraction");

            migrationBuilder.RenameTable(
                name: "LeadAssignments",
                newName: "LeadAssignment");

            migrationBuilder.RenameIndex(
                name: "IX_LeadInteractions_LeadId",
                table: "LeadInteraction",
                newName: "IX_LeadInteraction_LeadId");

            migrationBuilder.RenameColumn(
                name: "ConsultantId",
                table: "LeadAssignment",
                newName: "ApplicantId");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId",
                table: "LeadInteraction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lead",
                table: "Lead",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadInteraction",
                table: "LeadInteraction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadAssignment",
                table: "LeadAssignment",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssignment_LeadId",
                table: "LeadAssignment",
                column: "LeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignment_Lead_LeadId",
                table: "LeadAssignment",
                column: "LeadId",
                principalTable: "Lead",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadInteraction_Lead_LeadId",
                table: "LeadInteraction",
                column: "LeadId",
                principalTable: "Lead",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
