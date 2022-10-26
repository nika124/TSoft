using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TSoft.Models;

namespace TSoft.Data
{
    public class DataBaseHelper
    {
        public string connectionString = @"Data Source=localhost;Initial Catalog=InsData;Integrated Security=True";

        //PersonDataByUserNameAndPasword
        public Person GetPersonData(string userName, string password)
        {
            var result = new Person();
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"SELECT [UserName], [Password], [Status], [Id] FROM [InsData].[dbo].[Person]";
            if (userName != null)
            {
                sql = @"SELECT [UserName], [Password], [Status], [Id] FROM [InsData].[dbo].[Person] where [userName] = @UserName AND [password] = @Password";
            }

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (userName != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@UserName", userName));
                    cmd.Parameters.Add(new SqlParameter("@Password", password));
                }

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var readerResult = new Person();
                    readerResult.Id = (int)reader["Id"];
                    readerResult.Status = (bool)reader["Status"];

                    result = readerResult;
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        //GetAppObjectsDataByPersonId
        public List<AppObjects> GetAppObjectsData(int PersonId)
        {
            var result = new List<AppObjects>();
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"select DISTINCT ao.*
                 FROM [InsData].[dbo].[AppObjects] ao inner join AppObjectsGroup app ON app.AppObjectId = ao.Id
                 inner join Roles rl ON rl.Id = app.RoleId
				 inner join Permissions pm ON pm.RoleId = rl.Id
				 inner join Person pr ON pr.Id=pm.PersonId
                 where pr.Id = @Id";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@Id", PersonId));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var readerResult = new AppObjects();

                    readerResult.Id = (int)reader["Id"];
                    readerResult.Name = (string)reader["Name"];
                    readerResult.Type = (string)reader["Type"];
                    readerResult.URL = (string)reader["URL"];
                    readerResult.Status = (bool)reader["Status"];

                    result.Add(readerResult);
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        //GetWebPagePermissionDataByPersonIdAndURL
        public bool GetWebPagePermissionData(int PersonId, string URL)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"select DISTINCT ao.[URL], ao.[Status]
                 FROM[InsData].[dbo].[AppObjects] ao inner join AppObjectsGroup app ON app.AppObjectId = ao.Id
                 inner join Roles rl ON rl.Id = app.RoleId
                 inner join Permissions pm ON pm.RoleId = rl.Id
                 inner join Person pr ON pr.Id=pm.PersonId
                 where pr.Id = @PersonId AND [URL] = @URL";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PersonId", PersonId));
                cmd.Parameters.Add(new SqlParameter("@URL", URL));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ((bool)reader["Status"] == true)
                    {
                        return true;
                    }
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return false;
        }
        //CheckUserNameExitOrNot
        public bool UserNameChecker(string UserName)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"SELECT [UserName], [Status] FROM [InsData].[dbo].[Person]  WHERE [UserName] = @UserName";
            var result = new List<Permissions>();
            sql = @"SELECT [UserName], [Status] FROM [InsData].[dbo].[Person]  WHERE [UserName] = @UserName";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@UserName", UserName));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    if ((bool)reader["Status"] == true)
                    {
                        return true;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return false;
        }
        //SetPersonData
        public void SetPersonInfo(string Name, string LastName, string UserName, string Password, string Email, string PhoneNumber, string IdCardNumber, bool Gender)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"INSERT INTO [InsData].[dbo].[Person]  ([FirstName] ,[LastName] ,[UserName], [Password], [Status], [Email], [PhoneNumber], [IdCardNumber], [Gender]) VALUES (@Name, @LastName, @UserName, @Password, '1', @Email, @PhoneNumber, @IdCardNumber, @Gender)";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (Name != null && LastName != null && UserName != null && Password != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@Name", Name));
                    cmd.Parameters.Add(new SqlParameter("@LastName", LastName));
                    cmd.Parameters.Add(new SqlParameter("@UserName", UserName));
                    cmd.Parameters.Add(new SqlParameter("@Password", Password));
                    cmd.Parameters.Add(new SqlParameter("@Email", Email));
                    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", PhoneNumber));
                    cmd.Parameters.Add(new SqlParameter("@IdCardNumber", IdCardNumber));
                    cmd.Parameters.Add(new SqlParameter("@Gender", Gender));
                }
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        //GetPersonData
        public List<Person> GetPersonData()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"SELECT [UserName], [Id], [Status] FROM[InsData].[dbo].[Person]";
            var result = new List<Person>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var readerResult = new Person();

                    readerResult.Id = (int)reader["Id"];
                    readerResult.UserName = (string)reader["UserName"];
                    readerResult.Status = (bool)reader["Status"];
                    result.Add(readerResult);
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        //GetRoleNames
        public List<Roles> GetRoleNames()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"SELECT [Name], [Id], [Status] FROM[InsData].[dbo].[Roles]";
            var result = new List<Roles>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var readerResult = new Roles();

                    readerResult.Id = (int)reader["Id"];
                    readerResult.Name = (string)reader["Name"];
                    readerResult.Status = (bool)reader["Status"];
                    result.Add(readerResult);
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        //GetAppObjectNamesByRoleId
        public List<AppObjects> GetAppObjectNamesByRoleId(int RoleId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"SELECT A.[Id], A.[Name], A.[Status] FROM [InsData].[dbo].[AppObjects] A join [InsData].[dbo].[AppObjectsGroup] Ag on ag.AppObjectId = A.Id where Ag.RoleId = @RoleId";

            var result = new List<AppObjects>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@RoleId", RoleId));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var readerResult = new AppObjects();

                    readerResult.Id = (int)reader["Id"];
                    readerResult.Name = (string)reader["Name"];
                    readerResult.Status = (bool)reader["Status"];
                    result.Add(readerResult);
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        //GetAppObjectNames
        public List<AppObjects> GetAppObjectNames()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"SELECT [Id], [Name], [Status] FROM[InsData].[dbo].[AppObjects]";
            var result = new List<AppObjects>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var readerResult = new AppObjects();

                    readerResult.Id = (int)reader["Id"];
                    readerResult.Name = (string)reader["Name"];
                    readerResult.Status = (bool)reader["Status"];
                    result.Add(readerResult);
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        //SetPermissionByPersonIdAndPersonId
        public void SetPermission(int PersonId, int RoleId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"INSERT INTO [InsData].[dbo].[Permissions]  ([PersonId] ,[RoleId], [Status]) VALUES (@PersonId, @RoleId, 1) ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@PersonId", PersonId));
                cmd.Parameters.Add(new SqlParameter("@RoleId", RoleId));
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        //SetAppObjectsGroupByRoleIdAndAppId
        public void SetAppObjectsGroup(int RoleId, int AppobjectId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"INSERT INTO [InsData].[dbo].[AppObjectsGroup]  ([RoleId] ,[AppobjectId], [Status]) VALUES (@RoleId, @AppobjectId, 1) ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@RoleId", RoleId));
                cmd.Parameters.Add(new SqlParameter("@AppobjectId", AppobjectId));
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        //GetPersonDataByPersonId
        public Person GetPersonDataByPersonId(int PersonId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"SELECT * FROM [InsData].[dbo].[Person] Where [Id] = @Id";
            var result = new Person();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@Id", PersonId));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var readerResult = new Person();
                    readerResult.Id = (int)reader["Id"];
                    readerResult.UserName = reader["UserName"] as string;
                    readerResult.Password = reader["Password"] as string;
                    readerResult.FirstName = reader["FirstName"] as string;
                    readerResult.LastName = reader["LastName"] as string;
                    readerResult.Gender = (bool)reader["Gender"];
                    readerResult.IdCardNumber = reader["IdCardNumber"] as string;
                    readerResult.PhoneNumber = reader["PhoneNumber"] as string;
                    readerResult.Email = reader["Email"] as string;
                    readerResult.Status = (bool)reader["Status"];
                    result = readerResult;
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        //GetRoleNamesByPersonId
        public List<Roles> GetRoleNamesByPersonId(int PersonId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"SELECT [Name], r.[Id], r.[Status] FROM Permissions p inner join Roles r on r.id = p.RoleId WHERE p.PersonId = @Id";
            var result = new List<Roles>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@Id", PersonId));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var readerResult = new Roles();
                    readerResult.Id = (int)reader["Id"];
                    readerResult.Name = (string)reader["Name"];
                    readerResult.Status = (bool)reader["Status"];
                    result.Add(readerResult);
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        //DeleteRoleByPersonId
        public void DeleteRole(int RoleId, int PersonId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"DELETE FROM Permissions WHERE RoleId = @RoleId AND PersonId = @PersonId";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@RoleId", RoleId));
                cmd.Parameters.Add(new SqlParameter("@PersonId", PersonId));
                SqlDataReader reader = cmd.ExecuteReader();
            }
            finally
            {
                conn.Close();
            }
        }
        //PermissionCheckerByRoleIdAndPersonId
        public bool PermissionChecker(int RoleId, int PersonId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"SELECT  [Status] FROM Permissions where RoleId = @RoleId AND PersonId = @PersonId";
            var result = new List<Permissions>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@RoleId", RoleId));
                cmd.Parameters.Add(new SqlParameter("@PersonId", PersonId));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ((bool)reader["Status"] == true)
                    {
                        return true;
                    }
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return false;
        }
        //SetRole
        public void SetRole(string RoleName)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"INSERT INTO [InsData].[dbo].[Roles]  ([Name],  [Status]) VALUES (@RoleName,  1)";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@RoleName", RoleName));
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }

        }
        //RoleCheckerByRoleName
        public bool RoleChecker(string RoleName)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"SELECT [Name], [Status] FROM Roles where Name = @RoleName";
            var result = new List<Roles>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@RoleName", RoleName));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ((bool)reader["Status"] == true)
                    {
                        return true;
                    }
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return false;
        }
        //AppObjectsGroupCheckerByRoleIdAndAppObjectId
        public bool AppObjectsGroupChecker(int RoleId, int AppObjectId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"SELECT [Status] FROM AppObjectsGroup where RoleId = @RoleId AND AppObjectId = @AppObjectId";
            var result = new List<Roles>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@RoleId", RoleId));
                cmd.Parameters.Add(new SqlParameter("@AppObjectId", AppObjectId));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if ((bool)reader["Status"] == true)
                    {
                        return true;
                    }
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return false;
        }
        //DeletePersonByPersonId
        public void DeletePerson(int PersonId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"Delete From [InsData].[dbo].[Person] Where Id = @Id ";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@Id", PersonId));
                cmd.ExecuteNonQuery();

            }
            finally
            {
                conn.Close();
            }
        }
        //DeleteAppObjectsGroupByRoleIdAndAppObjectId
        public void DeleteAppObjectsGroup(int RoleId, int AppObjectId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"Delete From [InsData].[dbo].[AppObjectsGroup] Where RoleId = @RoleId AND AppObjectId = @AppObjectId";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@RoleId", RoleId));
                cmd.Parameters.Add(new SqlParameter("@AppObjectId", AppObjectId));
                cmd.ExecuteNonQuery();

            }
            finally
            {
                conn.Close();
            }
        }
        //UpdatePersonInfoByPersonId
        public void UpdatePersonInfo(string Name, string LastName, string UserName, string Password, string Email, string PhoneNumber, string IdCardNumber, bool Gender, int Id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = @"UPDATE [InsData].[dbo].[Person]
                SET FirstName = @FirstName, LastName = @LastName, UserName = @UserName, Password = @Password, Email = @Email, PhoneNumber = @PhoneNumber, IdCardNumber = @IdCardNumber 
                WHERE  [Id] = @Id";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (Name != null && LastName != null && UserName != null && Password != null)
                {
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));
                    cmd.Parameters.Add(new SqlParameter("@FirstName", Name));
                    cmd.Parameters.Add(new SqlParameter("@LastName", LastName));
                    cmd.Parameters.Add(new SqlParameter("@UserName", UserName));
                    cmd.Parameters.Add(new SqlParameter("@Password", Password));
                    cmd.Parameters.Add(new SqlParameter("@Email", Email));
                    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", PhoneNumber));
                    cmd.Parameters.Add(new SqlParameter("@IdCardNumber", IdCardNumber));
                    cmd.Parameters.Add(new SqlParameter("@Gender", Gender));
                }
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
