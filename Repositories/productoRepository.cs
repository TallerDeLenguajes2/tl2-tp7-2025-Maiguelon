using Microsoft.Data.Sqlite;
using presupuestario;

public class ProductoRepository
{
    // Cadena de conexión para SQLite
    private string connectionString = "Data Source=db/tiendadb.db";

    public int Alta(Producto producto)
    {
        int nuevoId = 0;
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string sql =
                @"INSERT INTO Productos (Descripcion, Precio) 
                            VALUES (@desc, @prec); 
                            SELECT MAX(idProducto) FROM Productos";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@desc", producto.Descripcion);
                command.Parameters.AddWithValue("@prec", producto.Precio);
                nuevoId = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return nuevoId;
    }

    public bool Modificar(int id, Producto producto)
    {
        int filasAfectadas = 0;
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string sql =
                @"UPDATE Productos SET Descripcion = @desc, Precio = @prec 
                Where idProducto = @id";

            using (var command = new SqliteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@desc", producto.Descripcion);
                command.Parameters.AddWithValue("@prec", producto.Precio);
                command.Parameters.AddWithValue("@id", id);

                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas > 0;
    }
}
