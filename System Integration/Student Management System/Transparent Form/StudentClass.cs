using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Transparent_Form
{
    class StudentClass
    {
        DBconnect connect = new DBconnect();

        // INSERT STUDENT
        public bool insertStudent(string fname, string lname, DateTime bdate, string gender, string phone, string address, byte[] img)
        {
            MySqlCommand command = new MySqlCommand(
                "INSERT INTO student (StdFirstName, StdLastName, Birthdate, Gender, Phone, Address, Photo) " +
                "VALUES (@fn, @ln, @bd, @gd, @ph, @adr, @img)",
                connect.getconnection
            );

            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

            connect.openConnect();
            bool success = command.ExecuteNonQuery() == 1;
            connect.closeConnect();

            return success;
        }

        // GET STUDENT LIST
        public DataTable getStudentlist(MySqlCommand command)
        {
            command.Connection = connect.getconnection;

            connect.openConnect();

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            connect.closeConnect();

            return table;
        }

        // GENERIC COUNT FUNCTION
        public string exeCount(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connect.getconnection);

            connect.openConnect();
            string count = command.ExecuteScalar().ToString();
            connect.closeConnect();

            return count;
        }

        // TOTAL STUDENTS
        public string totalStudent()
        {
            return exeCount("SELECT COUNT(*) FROM student");
        }

        // MALE STUDENTS
        public string maleStudent()
        {
            return exeCount("SELECT COUNT(*) FROM student WHERE Gender='Male'");
        }

        // FEMALE STUDENTS
        public string femaleStudent()
        {
            return exeCount("SELECT COUNT(*) FROM student WHERE Gender='Female'");
        }

        // TOTAL STUDENTS WITH SCORE
        public string totalStudentWithScore()
        {
            return exeCount(
                "SELECT COUNT(*) FROM student " +
                "INNER JOIN score ON student.StdId = score.StdId"
            );
        }

        // COUNT STUDENTS BY COURSE
        public string countStudentByCourse(string courseName)
        {
            MySqlCommand command = new MySqlCommand(
                "SELECT COUNT(*) " +
                "FROM student " +
                "INNER JOIN score ON student.StdId = score.StdId " +
                "INNER JOIN course ON score.CourseId = course.CourseId " +
                "WHERE course.CourseName = @courseName",
                connect.getconnection
            );

            command.Parameters.Add("@courseName", MySqlDbType.VarChar).Value = courseName;

            connect.openConnect();
            string count = command.ExecuteScalar().ToString();
            connect.closeConnect();

            return count;
        }

        // SEARCH STUDENT
        public DataTable searchStudent(string searchdata)
        {
            MySqlCommand command = new MySqlCommand(
                "SELECT * FROM student WHERE CONCAT(StdFirstName, StdLastName, Address) LIKE @search",
                connect.getconnection
            );

            command.Parameters.Add("@search", MySqlDbType.VarChar).Value = "%" + searchdata + "%";

            connect.openConnect();

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            connect.closeConnect();

            return table;
        }

        // UPDATE STUDENT
        public bool updateStudent(int id, string fname, string lname, DateTime bdate, string gender, string phone, string address, byte[] img)
        {
            MySqlCommand command = new MySqlCommand(
                "UPDATE student SET StdFirstName=@fn, StdLastName=@ln, Birthdate=@bd, Gender=@gd, Phone=@ph, Address=@adr, Photo=@img WHERE StdId=@id",
                connect.getconnection
            );

            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bd", MySqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gd", MySqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adr", MySqlDbType.VarChar).Value = address;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;

            connect.openConnect();
            bool success = command.ExecuteNonQuery() == 1;
            connect.closeConnect();

            return success;
        }

        // DELETE STUDENT
        public bool deleteStudent(int id)
        {
            MySqlCommand command = new MySqlCommand(
                "DELETE FROM student WHERE StdId=@id",
                connect.getconnection
            );

            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            connect.openConnect();
            bool success = command.ExecuteNonQuery() == 1;
            connect.closeConnect();

            return success;
        }

        // GENERIC GET LIST
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.getconnection;

            connect.openConnect();

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            connect.closeConnect();

            return table;
        }
    }
}