using FCRA.Repository.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository
{
    internal static class ExecuteSqlObjects
    {
        public static void UpdateSQLObjects(this MigrationBuilder migrationBuilder, string? folderName=null)
        {
            var basePath = System.IO.Directory.GetCurrentDirectory();
            string path = Path.Combine(basePath.Replace("FCRA.Web", "FCRA.Repository"), "SqlObjects");
            if (!string.IsNullOrWhiteSpace(folderName))
                path = Path.Combine(path, folderName);

            foreach (var file in Directory.GetFiles(path, "*.sql"))
            {
                string fileText = File.ReadAllText(file);
                fileText = fileText.Replace("{", "{{").Replace("}", "}}");
                migrationBuilder.Sql(fileText);
            }

           migrationBuilder.Sql($"UPDATE UserMaster SET RoleId=1 WHERE Id={Constants.UserId}");
        }
    }
}
