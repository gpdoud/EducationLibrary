using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationLibrary {

	public class StudentCollection : List<Student> {

		private static string connStr = @"Server=DSI-WORKSTATION\SQLEXPRESS;Database=DotNetDatabase;Trusted_Connection=yes;";

		public static StudentCollection Select() {
			//var connStr = @"Server=DSI-WORKSTATION\SQLEXPRESS;Database=DotNetDatabase;Trusted_Connection=yes;";
			SqlConnection connection = new SqlConnection(connStr);
			connection.Open();
			if(connection.State != System.Data.ConnectionState.Open) {
				Console.WriteLine("Connection did not open.");
				return null;
			}
			StudentCollection students = new StudentCollection();
			var sql = "Select * from Student";
			SqlCommand cmd = new SqlCommand(sql, connection);
			SqlDataReader reader = cmd.ExecuteReader();
			while(reader.Read()) {
				Student student = new Student(reader);
				students.Add(student);
			}
			reader.Close();
			connection.Close();
			return students;
		}
		public static Student Select(int id) {
			//var connStr = @"Server=DSI-WORKSTATION\SQLEXPRESS;Database=DotNetDatabase;Trusted_Connection=yes;";
			SqlConnection connection = new SqlConnection(connStr);
			connection.Open();
			if (connection.State != System.Data.ConnectionState.Open) {
				Console.WriteLine("Connection did not open.");
				return null;
			}
			StudentCollection students = new StudentCollection();
			var sql = $"Select * from Student where Id = {id}";
			SqlCommand cmd = new SqlCommand(sql, connection);
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read()) {
				Student student = new Student(reader);
				students.Add(student);
			}
			reader.Close();
			connection.Close();
			if(students.Count == 0) {
				return null;
			} else {
				return students[0];
			}
		}
		public static bool Insert(Student student) {
			//var connStr = @"Server=DSI-WORKSTATION\SQLEXPRESS;Database=DotNetDatabase;Trusted_Connection=yes;";
			SqlConnection connection = new SqlConnection(connStr);
			connection.Open();
			if (connection.State != System.Data.ConnectionState.Open) {
				Console.WriteLine("Connection did not open.");
				return false;
			}
			var sql = $"INSERT into Student (FirstName, LastName, Address, City, State, Zipcode," +
						$"PhoneNumber, Email, Birthday, MajorId, SAT, GPA)" +
						$" VALUES " +
						$"('{student.FirstName}', '{student.LastName}', '{student.Address}', " +
						$"'{student.City}', '{student.State}', '{student.Zipcode}', " +
						$"'{student.PhoneNumber}', '{student.Email}', '{student.Birthday}', " +
						$"{student.MajorId}, {student.SAT}, {student.GPA} )";
			SqlCommand cmd = new SqlCommand(sql, connection);
			var recsAffected = cmd.ExecuteNonQuery();
			return (recsAffected == 1);
		}
		public static bool Update(Student student) {
			//var connStr = @"Server=DSI-WORKSTATION\SQLEXPRESS;Database=DotNetDatabase;Trusted_Connection=yes;";
			SqlConnection connection = new SqlConnection(connStr);
			connection.Open();
			if (connection.State != System.Data.ConnectionState.Open) {
				Console.WriteLine("Connection did not open.");
				return false;
			}
			var sql = $"UPDATE Student Set " +
						$"FirstName = '{student.FirstName}'," +
						$"LastName = '{student.LastName}'," +
						$"Address = '{student.Address}'," +
						$"City = '{student.City}'," +
						$"State = '{student.State}'," +
						$"Zipcode = '{student.Zipcode}'," +
						$"Birthday = '{student.Birthday}'," +
						$"PhoneNumber = '{student.PhoneNumber}'," +
						$"Email = '{student.Email}'," +
						$"MajorId = {student.MajorId}," +
						$"SAT = {student.SAT}," +
						$"GPA = {student.GPA}" +
						$" WHERE ID = {student.Id}";

			SqlCommand cmd = new SqlCommand(sql, connection);
			var recsAffected = cmd.ExecuteNonQuery();
			return (recsAffected == 1);
		}
		public static bool Delete(int id) {
			//var connStr = @"Server=DSI-WORKSTATION\SQLEXPRESS;Database=DotNetDatabase;Trusted_Connection=yes;";
			SqlConnection connection = new SqlConnection(connStr);
			connection.Open();
			if (connection.State != System.Data.ConnectionState.Open) {
				Console.WriteLine("Connection did not open.");
				return false;
			}
			var sql = $"DELETE from Student" +
						$" WHERE ID = {id}";

			SqlCommand cmd = new SqlCommand(sql, connection);
			var recsAffected = cmd.ExecuteNonQuery();
			return (recsAffected == 1);
		}
	}
}
