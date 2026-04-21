using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Transparent_Form
{
    class ScoreClass
    {
        DBconnect connect = new DBconnect();

        // INSERT SCORE
        public bool insertScore(int stdId, int courseId, double score, string desc)
        {
            MySqlCommand command = new MySqlCommand(
                "INSERT INTO score (StdId, CourseId, Score, Description) " +
                "VALUES (@stid, @cid, @sco, @desc)",
                connect.getconnection
            );

            command.Parameters.Add("@stid", MySqlDbType.Int32).Value = stdId;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseId;
            command.Parameters.Add("@sco", MySqlDbType.Double).Value = score;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;

            connect.openConnect();
            bool success = command.ExecuteNonQuery() == 1;
            connect.closeConnect();

            return success;
        }

        // GET LIST
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

        // CHECK IF SCORE ALREADY EXISTS
        public bool checkScore(int stdId, int courseId)
        {
            MySqlCommand command = new MySqlCommand(
                "SELECT * FROM score WHERE StdId = @stid AND CourseId = @cid",
                connect.getconnection
            );

            command.Parameters.Add("@stid", MySqlDbType.Int32).Value = stdId;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseId;

            DataTable table = getList(command);

            return table.Rows.Count > 0;
        }

        // UPDATE SCORE
        public bool updateScore(int stdId, int courseId, double score, string desc)
        {
            MySqlCommand command = new MySqlCommand(
                "UPDATE score SET Score = @sco, Description = @desc " +
                "WHERE StdId = @stid AND CourseId = @cid",
                connect.getconnection
            );

            command.Parameters.Add("@stid", MySqlDbType.Int32).Value = stdId;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseId;
            command.Parameters.Add("@sco", MySqlDbType.Double).Value = score;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;

            connect.openConnect();
            bool success = command.ExecuteNonQuery() == 1;
            connect.closeConnect();

            return success;
        }

        // DELETE SCORE
        public bool deleteScore(int stdId, int courseId)
        {
            MySqlCommand command = new MySqlCommand(
                "DELETE FROM score WHERE StdId = @stid AND CourseId = @cid",
                connect.getconnection
            );

            command.Parameters.Add("@stid", MySqlDbType.Int32).Value = stdId;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = courseId;

            connect.openConnect();
            bool success = command.ExecuteNonQuery() == 1;
            connect.closeConnect();

            return success;
        }

        // GET SCORE LIST WITH STUDENT AND COURSE NAMES
        public DataTable getScoreList()
        {
            MySqlCommand command = new MySqlCommand(
                "SELECT score.StdId, student.StdFirstName, student.StdLastName, " +
                "score.CourseId, course.CourseName, score.Score, score.Description " +
                "FROM score " +
                "INNER JOIN student ON score.StdId = student.StdId " +
                "INNER JOIN course ON score.CourseId = course.CourseId",
                connect.getconnection
            );

            return getList(command);
        }
    }
}