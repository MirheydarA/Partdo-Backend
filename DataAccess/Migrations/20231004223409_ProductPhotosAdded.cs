﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ProductPhotosAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhoto_Products_ProductId",
                table: "ProductPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductPhoto",
                table: "ProductPhoto");

            migrationBuilder.RenameTable(
                name: "ProductPhoto",
                newName: "ProductPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPhoto_ProductId",
                table: "ProductPhotos",
                newName: "IX_ProductPhotos_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductPhotos",
                table: "ProductPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductPhotos",
                table: "ProductPhotos");

            migrationBuilder.RenameTable(
                name: "ProductPhotos",
                newName: "ProductPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPhotos_ProductId",
                table: "ProductPhoto",
                newName: "IX_ProductPhoto_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductPhoto",
                table: "ProductPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhoto_Products_ProductId",
                table: "ProductPhoto",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
