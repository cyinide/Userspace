using Microsoft.EntityFrameworkCore.Migrations;

namespace Userspace.Data.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
               .Sql("INSERT INTO Links (Name) Values ('www.example.com/')");
            migrationBuilder
                .Sql("INSERT INTO Links (Name) Values ('http://www.example.com/')");
            migrationBuilder
                .Sql("INSERT INTO Links (Name) Values ('http://www.example1.com/')");
            migrationBuilder
                .Sql("INSERT INTO Links (Name) Values ('https://www.example.com/')");

            migrationBuilder
                .Sql("INSERT INTO Tags (Name) Values ('foo')");
            migrationBuilder
                .Sql("INSERT INTO Tags (Name) Values ('hello')");
            migrationBuilder
               .Sql("INSERT INTO Tags (Name) Values ('helloworld')");
            migrationBuilder
               .Sql("INSERT INTO Tags (Name) Values ('foo3')");
            migrationBuilder
               .Sql("INSERT INTO Tags (Name) Values ('foo2')");
            migrationBuilder
               .Sql("INSERT INTO Tags (Name) Values ('foo4')");
            migrationBuilder
               .Sql("INSERT INTO Tags (Name) Values ('foo5')");
            migrationBuilder
            .Sql("INSERT INTO Tags (Name) Values ('greet')");

            migrationBuilder
               .Sql("INSERT INTO LinkTags (LinkId, TagId) Values ((SELECT Id FROM Links WHERE Name = 'http://www.example.com/'), (SELECT Id FROM Tags WHERE Name = 'foo'))");
            migrationBuilder
                .Sql("INSERT INTO LinkTags (LinkId, TagId) Values ((SELECT Id FROM Links WHERE Name = 'www.example.com/'), (SELECT Id FROM Tags WHERE Name = 'foo2'))");
            migrationBuilder
             .Sql("INSERT INTO LinkTags (LinkId, TagId) Values ((SELECT Id FROM Links WHERE Name = 'https://www.example.com/'), (SELECT Id FROM Tags WHERE Name = 'hello'))");
            migrationBuilder
                .Sql("INSERT INTO LinkTags (LinkId, TagId) Values ((SELECT Id FROM Links WHERE Name = 'www.example.com/'), (SELECT Id FROM Tags WHERE Name = 'foo3'))");
            migrationBuilder
             .Sql("INSERT INTO LinkTags (LinkId, TagId) Values ((SELECT Id FROM Links WHERE Name = 'http://www.example.com/'), (SELECT Id FROM Tags WHERE Name = 'helloworld'))");
            migrationBuilder
                .Sql("INSERT INTO LinkTags (LinkId, TagId) Values ((SELECT Id FROM Links WHERE Name = 'www.example.com/'), (SELECT Id FROM Tags WHERE Name = 'foo5'))");
            migrationBuilder
             .Sql("INSERT INTO LinkTags (LinkId, TagId) Values ((SELECT Id FROM Links WHERE Name = 'http://www.example.com/'), (SELECT Id FROM Tags WHERE Name = 'foo4'))");
            migrationBuilder
                .Sql("INSERT INTO LinkTags (LinkId, TagId) Values ((SELECT Id FROM Links WHERE Name = 'www.example.com/'), (SELECT Id FROM Tags WHERE Name = 'greet'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
               .Sql("DELETE FROM Links");
            migrationBuilder
               .Sql("DELETE FROM Tags");
            migrationBuilder
               .Sql("DELETE FROM LinkTags");
        }
    }
}
