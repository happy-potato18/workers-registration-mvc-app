using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WorkersRegistrationApp.Domain
{
    /// <summary>
    /// The <c>SqlDataFlow</c> class.
    /// Contains methods for passing requests and getting responses from database
    /// </summary>
    public static class SqlDataFlow
    {
        //string to create connection to database
        private static  readonly string _connectionString = Base.strConnect;

        /// <summary>
        /// Retrieves all workers' info from database
        /// </summary>
        /// <returns>
        /// List of class <see cref="Domain.Worker"/>
        /// </returns>
         public static  List<Worker> GetWorkerList()
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            //adds info about appropriate company to its worker to response
            string sqlExpression = "SELECT * FROM companyworkersdb.dbo.worker LEFT JOIN companyworkersdb.dbo.company ON" +
                " companyworkersdb.dbo.worker.company_id = companyworkersdb.dbo.company.id";

            List<Worker> workers = new List<Worker>();
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) 
                {                    
                    while (reader.Read())
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

                        Company company = new Company() // adds info about company to worker object
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

        /// <summary>
        /// Adds new <paramref name="worker"/> info to database.
        /// </summary>
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

        /// <summary>
        /// Retrieves worker with specified <paramref name="id"/> from database
        /// </summary>
        /// <returns>
        /// <see cref="Domain.Worker"/> object with info from database
        /// </returns>
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
                if (reader.HasRows) 
                {
                    if (reader.Read()) 
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

        /// <summary>
        /// Delete info about <paramref name="worker"/> from database
        /// </summary>
        public static void DeleteWorker(Worker worker)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string sqlExpression = $"DELETE FROM companyworkersdb.dbo.worker WHERE id ={worker.Id}";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Update info about <paramref name="worker"/> in database
        /// </summary>
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

        /// <summary>
        /// Retrieves all companies info from database
        /// </summary>
        /// <returns>
        /// List of class <see cref="Domain.Company"/>
        /// </returns>
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

                if (reader.HasRows) 
                {
                    while (reader.Read()) 
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

        /// <summary>
        /// Adds new <paramref name="company"/> info to database.
        /// </summary>
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
        
        /// <summary>
        /// Retrieves company with specified <paramref name="id"/> from database
        /// </summary>
        /// <returns>
        /// <see cref="Domain.Company"/> object with info from database
        /// </returns>
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
                if (reader.HasRows) 
                {
                    if (reader.Read())
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

        /// <summary>
        /// Delete info about <paramref name="company"/> from database
        /// </summary>
        public static void DeleteCompany(Company company)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            string sqlExpression = $"DELETE FROM companyworkersdb.dbo.company WHERE id ={company.Id}";
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Update info about <paramref name="company"/> in database
        /// </summary>
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

        /// <summary>
        /// Changes format of <paramref name="date"/>
        /// Used when adding date value to database 
        /// <see cref="AddWorker(Worker)"/>
        /// <see cref="AddCompany(Company)"/>
        /// </summary>
        private static string DateToSqlFriendlyDate(DateTime date)
        {
            return date.Year.ToString() +"-"+ date.Month.ToString() +"-"+ date.Day.ToString();
        }
        /// <summary>
        /// Counts workers in company with specified <paramref name="id"/>
        /// Used when retrieving info about companies <see cref="GetCompanyList"/>
        /// </summary>
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