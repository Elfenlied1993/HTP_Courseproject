using Microsoft.EntityFrameworkCore.Migrations;

namespace Course.ITnews.Data.EntityFramework.Migrations.ApplicationIdentityDb
{
    public partial class InitialIdentity_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentary_AspNetUsers_AuthorId",
                table: "Commentary");

            migrationBuilder.DropForeignKey(
                name: "FK_Commentary_News_NewsId",
                table: "Commentary");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Category_CategoryId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsTag_News_NewsId",
                table: "NewsTag");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsTag_Tag_TagId",
                table: "NewsTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsTag",
                table: "NewsTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Commentary",
                table: "Commentary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "NewsTag",
                newName: "NewsTags");

            migrationBuilder.RenameTable(
                name: "Commentary",
                newName: "Commentaries");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_NewsTag_TagId",
                table: "NewsTags",
                newName: "IX_NewsTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsTag_NewsId",
                table: "NewsTags",
                newName: "IX_NewsTags_NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_Commentary_NewsId",
                table: "Commentaries",
                newName: "IX_Commentaries_NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_Commentary_AuthorId",
                table: "Commentaries",
                newName: "IX_Commentaries_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsTags",
                table: "NewsTags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Commentaries",
                table: "Commentaries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_AspNetUsers_AuthorId",
                table: "Commentaries",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_News_NewsId",
                table: "Commentaries",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTags_News_NewsId",
                table: "NewsTags",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTags_Tags_TagId",
                table: "NewsTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_AspNetUsers_AuthorId",
                table: "Commentaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_News_NewsId",
                table: "Commentaries");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsTags_News_NewsId",
                table: "NewsTags");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsTags_Tags_TagId",
                table: "NewsTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsTags",
                table: "NewsTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Commentaries",
                table: "Commentaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "NewsTags",
                newName: "NewsTag");

            migrationBuilder.RenameTable(
                name: "Commentaries",
                newName: "Commentary");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_NewsTags_TagId",
                table: "NewsTag",
                newName: "IX_NewsTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_NewsTags_NewsId",
                table: "NewsTag",
                newName: "IX_NewsTag_NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_Commentaries_NewsId",
                table: "Commentary",
                newName: "IX_Commentary_NewsId");

            migrationBuilder.RenameIndex(
                name: "IX_Commentaries_AuthorId",
                table: "Commentary",
                newName: "IX_Commentary_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsTag",
                table: "NewsTag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Commentary",
                table: "Commentary",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentary_AspNetUsers_AuthorId",
                table: "Commentary",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Commentary_News_NewsId",
                table: "Commentary",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Category_CategoryId",
                table: "News",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTag_News_NewsId",
                table: "NewsTag",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTag_Tag_TagId",
                table: "NewsTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
