using System.Data.SQLite;
using tl2_tp09_2023_InakiPoch.Models;

public interface IUserRepository {
    void Add(User user);
    void Update(int id, User user);
    List<User> GetAll();
    User GetById(int id);
    void Delete(int id);
}

namespace tl2_tp09_2023_InakiPoch.Repositories {
    public class UserRepository : IUserRepository {
        readonly string connectionPath = "Data Source=DataBase/board.db;Cache=Shared";

        public void Add(User user) {
            string queryText = "INSERT INTO user (id, username) VALUES (@id, @username)";
            using(SQLiteConnection connection = new SQLiteConnection(connectionPath)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", user.Id));
                query.Parameters.Add(new SQLiteParameter("@username", user.Username));
                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Update(int id, User user) {
            string queryText = "UPDATE user SET username = @username WHERE id = @id";
            using(SQLiteConnection connection = new SQLiteConnection(connectionPath)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", id));
                query.Parameters.Add(new SQLiteParameter("@username", user.Username));
                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<User> GetAll() {
            string queryText = "SELECT * FROM user";
            List<User> users = new List<User>();
            using(SQLiteConnection connection = new SQLiteConnection(connectionPath)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                connection.Open();
                using(SQLiteDataReader reader = query.ExecuteReader()) {
                    while(reader.Read()) {
                        var user = new User() {
                            Id = Convert.ToInt32(reader["id"]),
                            Username = reader["username"].ToString()
                        };
                        users.Add(user);
                    }
                }
                connection.Close();
            }
            return users;
        }

        public User GetById(int id) {
            string queryText = "SELECT * FROM user WHERE id = @id";
            User user = new User();
            using(SQLiteConnection connection = new SQLiteConnection(connectionPath)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                using(SQLiteDataReader reader = query.ExecuteReader()) {
                    while(reader.Read()) {
                        user.Id = Convert.ToInt32(reader["id"]);
                        user.Username = reader["username"].ToString();
                    }
                }
                connection.Close();
            }
            return user;
        }

        public void Delete(int id) {
            string queryText = "DELETE FROM user WHERE id = @id";
            using(SQLiteConnection connection = new SQLiteConnection(connectionPath)) {
                SQLiteCommand query = new SQLiteCommand(queryText, connection);
                query.Parameters.Add(new SQLiteParameter("@id", id));
                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}