using Dapper;
using System.Data.SqlClient;

namespace Shop.Classes;

public class Db : IDisposable
{
    public string ConnectionString { get; set; }
    public SqlConnection Connection { get; set; }

    public Db(string connectionString = "Data Source=localhost; Trusted_Connection=True; TrustServerCertificate=True")
    {
        ConnectionString = connectionString;
        Connection = new SqlConnection(connectionString);
        Open();

        Exec("""
            IF OBJECT_ID('dbo.users', 'u') IS NULL
                CREATE TABLE users
                (
                    id BIGINT PRIMARY KEY IDENTITY(1, 1),
                    login VARCHAR(100),
                    password VARCHAR(100),

                    UNIQUE(login, password)
                )
            """);

        Exec("""
            IF OBJECT_ID('dbo.things', 'u') IS NULL
                CREATE TABLE things
                (
                    id BIGINT PRIMARY KEY IDENTITY(1, 1),
                    name VARCHAR(100),
                    description VARCHAR(100),
                    price FLOAT,

                    UNIQUE(name, description)
                )
            """);
    }

    public void Open()
    {
        Connection.Open();
    }

    public void Close()
    {
        Connection.Close();
    }

    public void Exec(string sql)
    {
        using (SqlCommand cmd = new(sql, Connection))
        {
            cmd.ExecuteNonQuery();
        }
    }

    public List<User> GetUsers()
    {
        return Connection.Query<User>("SELECT * FROM users").ToList();
    }

    public User GetUserById(long id)
    {
        return Connection.QueryFirstOrDefault<User>(
            $"SELECT * FROM users WHERE id = {id}"
        ) ?? throw new Exception("User not found");
    }

    public User GetUserByLoginAndPassword(string login, string password)
    {
        return Connection.QueryFirstOrDefault<User>(
            $"SELECT * FROM users WHERE login = '{login}' AND password = '{password}'"
        ) ?? throw new Exception("User not found");
    }

    public List<User> GetUsersByLikeLogin(string login)
    {
        return Connection.Query<User>(
            $"SELECT * FROM users WHERE login LIKE '%{login}%'"
        ).ToList();
    }

    public void AddUser(User user)
    {
        Exec($"INSERT INTO users(login, password) VALUES ('{user.Login}', '{user.Password}')");
    }

    public void RemoveUserById(long id)
    {
        Exec($"DELETE FROM users WHERE id = {id}");
    }

    public List<Thing> GetThings()
    {
        return Connection.Query<Thing>("SELECT * FROM things").ToList();
    }

    public Thing GetThingById(long id)
    {
        return Connection.QueryFirstOrDefault<Thing>(
            $"SELECT * FROM things WHERE id = {id}"
        ) ?? throw new Exception("Thing not found");
    }

    public List<Thing> GetThingsByLikeQuery(string query)
    {
        return Connection.Query<Thing>(
            $"SELECT * FROM things WHERE name LIKE '%{query}%' OR description LIKE '%{query}%'"
        ).ToList();
    }

    public void AddThing(Thing thing)
    {
        string query = "INSERT INTO things(name, description, price) VALUES (@Name, @Description, @Price)";

        using (SqlCommand command = new(query, Connection))
        {
            command.Parameters.AddWithValue("@Name", thing.Name);
            command.Parameters.AddWithValue("@Description", thing.Description);
            command.Parameters.AddWithValue("@Price", thing.Price);
            command.ExecuteNonQuery();
        }
    }

    public void RemoveThingById(long id)
    {
        Exec($"DELETE FROM things WHERE id = {id}");
    }

    public void Dispose()
    {
        Close();
        Connection.Dispose();
    }
}
