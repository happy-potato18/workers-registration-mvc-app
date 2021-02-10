using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WorkersRegistrationApp.Domain
{
    public  static class SqlDataFlow
    {
        private static  readonly string _connectionString = Base.strConnect;
        public static  List<Worker> GetWorkerList()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string sqlExpression = "SELECT * FROM companyworkersdb.dbo.worker LEFT JOIN companyworkersdb.dbo.company ON" +
                " companyworkersdb.dbo.worker.company_id = companyworkersdb.dbo.company.id";
            List<Worker> workers = new List<Worker>();
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    
                    while (reader.Read()) // построчно считываем данные
                    {
                        Worker worker = new Worker()
                        {
                            Id = (int)reader.GetValue(0),
                            Name = (string)reader.GetValue(1),
                            MiddleName = (string)reader.GetValue(2),
                            Surname = (string)reader.GetValue(3),
                            Date = ((DateTime)reader.GetValue(4)).Date,
                            Job = (string)reader.GetValue(5),
                            CompanyId = (int)reader.GetValue(6)
                        };

                        Company company = new Company()
                        {
                            Id = (int)reader.GetValue(7),
                            Title = (string)reader.GetString(8),
                            LegalForm = (string)reader.GetString(9)
                        };

                        worker.Company = company;
                        workers.Add(worker);
                    }
                }

                reader.Close();
                connection.Close();
            }

            return workers;
        }
        public static void AddWorker(Worker worker)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            string sqlExpression = $"INSERT INTO companyworkersdb.dbo.worker " +
                $"VALUES ('{worker.Name}','{worker.Surname}','{worker.MiddleName}','{DateToSqlFriendlyDate(worker.Date)}'," +
                $"'{worker.Job}',{worker.CompanyId})";
     
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                connection.Close();               
            }
        }
        
        public static Worker FindWorkerById(int? id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string sqlExpression = $"SELECT * FROM companyworkersdb.dbo.worker WHERE id ={id}";
            Worker worker = default;
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {

                    if (reader.Read()) // построчно считываем данные
                    {
                        worker = new Worker()
                        {
                            Id = (int)reader.GetValue(0),
                            Name = (string)reader.GetValue(1),
                            MiddleName = (string)reader.GetValue(2),
                            Surname = (string)reader.GetValue(3),
                            Date = ((DateTime)reader.GetValue(4)).Date,
                            Job = (string)reader.GetValue(5),
                            CompanyId = (int)reader.GetValue(6)
                        };
                    }
                }

                reader.Close();
                connection.Close();
            }

            return worker;

        }

        public static void DeleteWorker(Worker worker)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string sqlExpression = $"DELETE FROM companyworkersdb.dbo.worker WHERE id ={worker.Id}";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void UpdateWorker(Worker worker)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            string sqlExpression = $"UPDATE companyworkersdb.dbo.worker SET" +
                $" name = '{worker.Name}', surname ='{worker.Surname}', middle_name = '{worker.MiddleName}'," +
                $"employment_date = '{DateToSqlFriendlyDate(worker.Date)}'," +
                $"job = '{worker.Job}'," +
                $"company_id ={worker.CompanyId} WHERE id = {worker.Id}";

            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static List<Company> GetCompanyList()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string sqlExpression = "SELECT * FROM companyworkersdb.dbo.company";
            List<Company> companies = new List<Company>();
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    while (reader.Read()) // построчно считываем данные
                    {
                        Company company = new Company()
                        {
                            Id = (int)reader.GetValue(0),
                            Title = (string)reader.GetString(1),
                            LegalForm = (string)reader.GetString(2)
                        };

                        company.CountOfWorkers = CountWorkers(company.Id);

                        companies.Add(company);
                    }
                }

                reader.Close();
                connection.Close();
            }

            return companies;
        }

        public static void AddCompany(Company company)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            string sqlExpression = $"INSERT INTO companyworkersdb.dbo.company " +
                $"VALUES ('{company.Title}','{company.LegalForm}')";

            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static Company FindCompanyById(int? id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string sqlExpression = $"SELECT * FROM companyworkersdb.dbo.company WHERE id ={id}";
            Company company = default;
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {

                    if (reader.Read()) // построчно считываем данные
                    {
                        company = new Company()
                        {
                            Id = (int)reader.GetValue(0),
                            Title = (string)reader.GetValue(1),
                            LegalForm = (string)reader.GetValue(2)
                        };
                    }
                }

                reader.Close();
                connection.Close();
            }

            return company;

        }

        public static void DeleteCompany(Company company)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string sqlExpression = $"DELETE FROM companyworkersdb.dbo.company WHERE id ={company.Id}";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void UpdateCompany(Company company)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            string sqlExpression = $"UPDATE companyworkersdb.dbo.company SET" +
                $" title = '{company.Title}', legal_form ='{company.LegalForm}'WHERE id = {company.Id}";

            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        private static string DateToSqlFriendlyDate(DateTime date)
        {
            return date.Year.ToString() + date.Month.ToString() + date.Day.ToString();
        }

        private static int CountWorkers(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            string sqlExpression = $"SELECT COUNT(*) FROM companyworkersdb.dbo.worker WHERE company_id = {id}";
            int count = 0;
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {

                    if (reader.Read()) // построчно считываем данные
                    {
                        count = reader.GetInt32(0);
                    }
                }
                reader.Close();
                connection.Close();
            }

            return count;
        }

    }

}