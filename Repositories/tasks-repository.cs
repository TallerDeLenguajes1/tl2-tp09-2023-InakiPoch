using System.Data.SQLite;
using tl2_tp09_2023_InakiPoch.Models;

public interface ITasksRepository {
    void Add(int boardId, Tasks task);
    void Update(int id, Tasks task);
    Tasks GetById(int id);
    List<Tasks> GetByUser(int userId);
    List<Tasks> GetByBoard(int boardId);
    void Delete(int id);
    void AssignTask(int usarId, int taskId);
}

namespace tl2_tp09_2023_InakiPoch.Repositories {
    public class TasksRepository : ITasksRepository {
        readonly string connectionPath = "Data Source=DataBase/board.db;Cache=Shared";

        public void Add(int boardId, Tasks task) {
            string queryText = "INSERT INTO task (id, board_id, name, state, description, color, assigned_user_id) " +  
                                "VALUES (@id, @board_id, @name, @state, @description, @color, @assigned_user_id)";
            using(SQLiteConnection connection = new SQLiteConnection(connectionPath)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", task.Id));
                query.Parameters.Add(new SQLiteParameter("@board_id", boardId));
                query.Parameters.Add(new SQLiteParameter("@name", task.Name));
                query.Parameters.Add(new SQLiteParameter("@state", task.State));
                query.Parameters.Add(new SQLiteParameter("@description", task.Description));
                query.Parameters.Add(new SQLiteParameter("@color", task.Color));
                query.Parameters.Add(new SQLiteParameter("@assigned_user_id", task.AssignedUserId));
                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Update(int id, Tasks task) {
            string queryText = "UPDATE user SET board_id = @board_id, name = @name, state = @state, description = @description " +  
                                "color = @color, assigned_user_id = @assigned_user_id WHERE id = @id";
            using(SQLiteConnection connection = new SQLiteConnection(connectionPath)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", id));
                query.Parameters.Add(new SQLiteParameter("@board_id", task.BoardId));
                query.Parameters.Add(new SQLiteParameter("@name", task.Name));
                query.Parameters.Add(new SQLiteParameter("@state", task.State));
                query.Parameters.Add(new SQLiteParameter("@description", task.Description));
                query.Parameters.Add(new SQLiteParameter("@color", task.Color));
                query.Parameters.Add(new SQLiteParameter("@assigned_user_id", task.AssignedUserId));
                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Tasks GetById(int id) {
            string queryText = "SELECT * FROM task WHERE id = @id";
            Tasks task = new Tasks();
            using(SQLiteConnection connection = new SQLiteConnection(connectionPath)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                using(SQLiteDataReader reader = query.ExecuteReader()) {
                    while(reader.Read()) {
                        task.Id = Convert.ToInt32(reader["id"]);
                        task.BoardId = Convert.ToInt32(reader["board_id"]);
                        task.Name = reader["name"].ToString();
                        task.State = (TasksState)Convert.ToInt32(reader["state"]);
                        task.Description = reader["description"].ToString();
                        task.Color = reader["color"].ToString();
                        if(reader["assigned_user_id"] != DBNull.Value) {
                            task.AssignedUserId = Convert.ToInt32(reader["assigned_user_id"]);
                        } else {
                            task.AssignedUserId = null;
                        }
                    }
                }
                connection.Close();
            }
            return task;
        }

        public List<Tasks> GetByUser(int userId) {
            string queryText = "SELECT * FROM task t INNER JOIN user u ON t.assigned_user_id = @id";
            List<Tasks> tasks = new List<Tasks>();
            using(SQLiteConnection connection = new SQLiteConnection(connectionPath)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", userId));
                connection.Open();
                using(SQLiteDataReader reader = query.ExecuteReader()) {
                    while(reader.Read()) {
                        Tasks task = new Tasks() {
                            Id = Convert.ToInt32(reader["id"]),
                            BoardId = Convert.ToInt32(reader["board_id"]),
                            Name = reader["name"].ToString(),
                            State = (TasksState)Convert.ToInt32(reader["state"]),
                            Description = reader["description"].ToString(),
                            Color = reader["color"].ToString(),
                            AssignedUserId = reader["assigned_user_id"] == DBNull.Value ? null : Convert.ToInt32(reader["assigned_user_id"]) 
                        };
                        tasks.Add(task);
                    }
                }
                connection.Close();
            }
            return tasks;
        }

        public List<Tasks> GetByBoard(int boardId) {
            string queryText = "SELECT * FROM task t INNER JOIN board b ON t.board_id = @id";
            List<Tasks> tasks = new List<Tasks>();
            using(SQLiteConnection connection = new SQLiteConnection(connectionPath)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", boardId));
                connection.Open();
                using(SQLiteDataReader reader = query.ExecuteReader()) {
                    while(reader.Read()) {
                        Tasks task = new Tasks() {
                            Id = Convert.ToInt32(reader["id"]),
                            BoardId = Convert.ToInt32(reader["board_id"]),
                            Name = reader["name"].ToString(),
                            State = (TasksState)Convert.ToInt32(reader["state"]),
                            Description = reader["description"].ToString(),
                            Color = reader["color"].ToString(),
                            AssignedUserId = reader["assigned_user_id"] == DBNull.Value ? null : Convert.ToInt32(reader["assigned_user_id"])
                        };
                        tasks.Add(task);
                    }
                }
                connection.Close();
            }
            return tasks;
        }

        public void Delete(int id) {
            string queryText = "DELETE FROM task WHERE id = @id";
            using(SQLiteConnection connection = new SQLiteConnection(connectionPath)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void AssignTask(int userId, int taskId) {
            string queryText = "UPDATE task SET assigned_user_id = @assigned_user_id WHERE id = @id";
            using(SQLiteConnection connection = new SQLiteConnection(connectionPath)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@assigned_user_id", userId));
                query.Parameters.Add(new SQLiteParameter("@id", taskId));
                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}