using Microsoft.EntityFrameworkCore.Migrations;

namespace Userspace.Data.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO AspNetUsers (Id, Username, Email, FirstName, LastName, Password, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, AccessFailedCount, LockoutEnd, LockoutEnabled) Values ('BCF35950-66FB-4EAC-BE9E-08D8F2E0DD6C'," +
                " 'usertester', 'usertester@email.com', 'user','tester','Qwer1234!', 0, 0, 0, 0, NULL, 0)");
            migrationBuilder
                .Sql("INSERT INTO AspNetUsers (Id, Username, Email, FirstName, LastName, Password, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, AccessFailedCount, LockoutEnd, LockoutEnabled) Values ('B7C9A318-BBD9-414A-BE9F-08D8F2E0DD6C', 'testeruser','testeruser@email.com','tester','user','Qwer1234!', 0, 0, 0, 0, NULL, 0)");

            migrationBuilder
                .Sql("INSERT INTO Links (Name) Values ('www.example.com/?foo=bar&hello=world')");
            migrationBuilder
                .Sql("INSERT INTO Links (Name) Values ('http://www.example.com/?foo=bar&hello=world')");
            migrationBuilder
                .Sql("INSERT INTO Links (Name) Values ('http://www.example.com/?hello=world&foo=bar')");
            migrationBuilder
                .Sql("INSERT INTO Links (Name) Values ('http://www.example.com/?hello=world&foo1=bar2')");
            migrationBuilder
                .Sql("INSERT INTO Links (Name) Values ('http://www.example.com/?hello=world&foo2=bar1')");

            migrationBuilder
               .Sql("INSERT INTO Tags (Name, LinkId) Values ('news', (SELECT Id FROM Links WHERE Name = 'www.example.com/?foo=bar&hello=world'))");
            migrationBuilder
               .Sql("INSERT INTO Tags (Name, LinkId) Values ('business', (SELECT Id FROM Links WHERE Name = 'www.example.com/?foo=bar&hello=world'))");
            migrationBuilder
               .Sql("INSERT INTO Tags (Name, LinkId) Values ('sport', (SELECT Id FROM Links WHERE Name = 'http://www.example.com/?foo=bar&hello=world'))");
            migrationBuilder
               .Sql("INSERT INTO Tags (Name, LinkId) Values ('culture', (SELECT Id FROM Links WHERE Name = 'http://www.example.com/?foo=bar&hello=world'))");
            migrationBuilder
               .Sql("INSERT INTO Tags (Name, LinkId) Values ('lifestyle', (SELECT Id FROM Links WHERE Name = 'http://www.example.com/?hello=world&foo=bar'))");
            migrationBuilder
               .Sql("INSERT INTO Tags (Name, LinkId) Values ('tech', (SELECT Id FROM Links WHERE Name = 'http://www.example.com/?hello=world&foo=bar'))");
            migrationBuilder
               .Sql("INSERT INTO Tags (Name, LinkId) Values ('riddles', (SELECT Id FROM Links WHERE Name = 'http://www.example.com/?hello=world&foo1=bar2'))");
            migrationBuilder
               .Sql("INSERT INTO Tags (Name, LinkId) Values ('riddles', (SELECT Id FROM Links WHERE Name = 'http://www.example.com/?hello=world&foo2=bar1'))");
            migrationBuilder
               .Sql("INSERT INTO Tags (Name, LinkId) Values ('riddles', (SELECT Id FROM Links WHERE Name = 'http://www.example.com/?hello=world&foo1=bar2'))");
            migrationBuilder
               .Sql("INSERT INTO Tags (Name, LinkId) Values ('riddles', (SELECT Id FROM Links WHERE Name = 'http://www.example.com/?hello=world&foo2=bar1'))");

            migrationBuilder
                .Sql("INSERT INTO UserLinks (LinkId, UserId) Values ((SELECT Id FROM Links WHERE Name = 'www.example.com/?foo=bar&hello=world'), (SELECT Id FROM AspNetUsers WHERE Id = 'BCF35950-66FB-4EAC-BE9E-08D8F2E0DD6C'))");
            migrationBuilder
                .Sql("INSERT INTO UserLinks (LinkId, UserId) Values ((SELECT Id FROM Links WHERE Name = 'www.example.com/?foo=bar&hello=world'), (SELECT Id FROM AspNetUsers WHERE Id = 'B7C9A318-BBD9-414A-BE9F-08D8F2E0DD6C'))");
            migrationBuilder
                .Sql("INSERT INTO UserLinks (LinkId, UserId) Values ((SELECT Id FROM Links WHERE Name = 'http://www.example.com/?hello=world&foo=bar'), (SELECT Id FROM AspNetUsers WHERE Id = 'BCF35950-66FB-4EAC-BE9E-08D8F2E0DD6C'))");
            migrationBuilder
                .Sql("INSERT INTO UserLinks (LinkId, UserId) Values ((SELECT Id FROM Links WHERE Name = 'http://www.example.com/?foo=bar&hello=world'), (SELECT Id FROM AspNetUsers WHERE Id = 'BCF35950-66FB-4EAC-BE9E-08D8F2E0DD6C'))");
            migrationBuilder
                .Sql("INSERT INTO UserLinks (LinkId, UserId) Values ((SELECT Id FROM Links WHERE Name = 'http://www.example.com/?foo=bar&hello=world'), (SELECT Id FROM AspNetUsers WHERE Id = 'B7C9A318-BBD9-414A-BE9F-08D8F2E0DD6C'))");
            migrationBuilder
                .Sql("INSERT INTO UserLinks (LinkId, UserId) Values ((SELECT Id FROM Links WHERE Name = 'http://www.example.com/?hello=world&foo=bar'), (SELECT Id FROM AspNetUsers WHERE Id = 'B7C9A318-BBD9-414A-BE9F-08D8F2E0DD6C'))");
            migrationBuilder
                .Sql("INSERT INTO UserLinks (LinkId, UserId) Values ((SELECT Id FROM Links WHERE Name = 'http://www.example.com/?hello=world&foo1=bar2'), (SELECT Id FROM AspNetUsers WHERE Id = 'BCF35950-66FB-4EAC-BE9E-08D8F2E0DD6C'))");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
              .Sql("DELETE FROM Links");
            migrationBuilder
               .Sql("DELETE FROM Tags");
            migrationBuilder
               .Sql("DELETE FROM UserTags");
        }
    }
}
