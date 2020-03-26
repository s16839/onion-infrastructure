using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;


namespace Onion.Infrastructure.SqlSeverService.Services

{
    public class SqlServerService : IStudentDbService
    {
        private List<Student> students = new List<Student>();
        private string connectionString = "Data Source=db-mssql;Initial Catalog=s16839;Integrated Security=True";

        public bool EnrollStudent(Student newStudent, int semestr)
        {
            int queryResult = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"insert into Student (Id, FirstName, LastName) values ({newStudent.IdStudent},{newStudent.FirstName}),{newStudent.LastName}", connection))
                {
                    
                    queryResult = command.ExecuteNonQuery();
                    
                }
            }

            if (queryResult == 1)
                return true;

            return false;
        }

        public IEnumerable<Student> GetStudents() {
            DataTable dataReceived = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select * from Students", connection))
                {
                    dataTable.Load(command.ExecuteReader());
                }
            }
            foreach (DataRow row in dataTable.Rows)
            {
                students.Add(new Student()
                {
                    IdStudent = Convert.ToInt32(row[0]),
                    FirstName = row[1].ToString(),
                    LastName = row[2].ToString()
                });
            }
            return students;
        }
    }
}

