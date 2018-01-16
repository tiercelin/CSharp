using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

// NonQuery = create, insert into, delete
//Query = select
public class SimpleDataSource : IDisposable
{
    MySqlConnection conn;

    /// <summary>
    /// Constructor, calls Connect method with params
    /// </summary>
    /// <param name="server">Server IP or hostname</param>
    /// <param name="database">Database/schema name</param>
    /// <param name="port">Port number</param>
    /// <param name="user">Username</param>
    /// <param name="password">Password</param>
    public SimpleDataSource(string server, string database, int port,
        string user, string password)
    {
        // TODO: Call the Connect method with the parameters.
        Connect(server, database, port, user, password);
    }

    /// <summary>
    /// Intialises MySqlConnection object with parameters provided.
    /// </summary>
    /// <param name="server">Server IP or hostname</param>
    /// <param name="database">Database/schema name</param>
    /// <param name="port">Port number</param>
    /// <param name="user">Username</param>
    /// <param name="password">Password</param>
    public void Connect(string server, string database, int port,
        string user, string password)
    {
        // TODO: Initialise MySqlConnection object with parameters,
        // open connection with suitable exception handling.
        try
        {
            conn = new MySqlConnection(string.Format("server={0};database={1};port={2};user={3};password={4}", server, database, port, user, password));
            conn.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            if (conn==null)
            { conn.Close();  }
        }

    }

    /// <summary>
    /// Creates an SQL query from the provided string and
    /// executes it.
    /// </summary>
    /// <param name="queryString">A string containing an SQL query</param>
    /// <returns>A MySqlDataReader object with the results 
    /// of the query</returns>
    
    
    public MySqlDataReader Query(string queryString)
    {
        // TODO: Declares MySqlDataReader and MySqlCommand objects.
        // When the MySqlCommand object is executed with the query
        // string, the return value will be assigned to the MySqlDataReader
        // object. This object is then returned with the "return" keyword.
        MySqlDataReader reader = null;
        MySqlCommand Command = new MySqlCommand(queryString, conn);
        if (Command != null)
        {
            reader = Command.ExecuteReader();
        }
        return reader;
    }

    /// <summary>
    /// Creates an SQL statement from the provided string and
    /// executes it. 
    /// </summary>
    /// <param name="updateString">A string containing an SQL non-query</param>
    public void Update(string updateString)
    {
        // TODO: Creates and initialises a MySqlCommand object with
        // the provided string parameter, and executes this update
        // with suitable exception handling.
        MySqlCommand Command = new MySqlCommand(updateString, conn);
        try
        {
            if (Command != null && conn != null)
            {
                Command.ExecuteNonQuery();
            }

        }
        catch (Exception ex)
        { 
            Console.WriteLine(ex.ToString());
        }
    }

    /// <summary>
    /// Garbage collection method called by Garbage Collector.
    /// SimpleDataSource implements IDisposable, so the GC will
    /// know to call this method when the object is no longer
    /// needed.
    /// </summary>
    public void Dispose()
    {
        GC.Collect();
        if (conn != null)
            conn.Dispose();
    }
    public void QueryPreparedStatement(string QueryPreparedString, Dictionary<int, string> openWith)
    {
        MySqlDataReader reader = null;

            foreach (KeyValuePair<int, string> kvp in openWith)
            {
                MySqlCommand Command = new MySqlCommand();
                Command.CommandText = QueryPreparedString;
                Command.Connection = conn;

                if (Command != null && conn!=null)
                {
                    Command.Parameters.AddWithValue("@iddictionary", kvp.Key);
                    Command.Parameters.AddWithValue("@data", kvp.Value);
                    reader = Command.ExecuteReader();

                while (reader.Read())
                    {
                    Console.WriteLine(string.Format(" {0} => {1} ", reader[0], reader[1]));
                    }

                    reader.Close();
                }
            }
        
    }
    public void NonQueryPreparedStatement(string NonQueryPreparedString, Dictionary<int, string> openWith)
    {
        foreach (KeyValuePair<int, string> kvp in openWith)
        {
            MySqlCommand command = new MySqlCommand();

            command.CommandText = NonQueryPreparedString;
            command.Connection = conn;

            if (conn != null && command != null)
            {
                command.Parameters.AddWithValue("@iddictionary", kvp.Key);
                command.Parameters.AddWithValue("@data", kvp.Value);
                command.ExecuteNonQuery();
            }
        }
    }
    public DataTable DataTableQuery(string sqlQuery)
    {
        DataTable myDataTable = null;
        if(conn!=null)
        {
            try
            {
                myDataTable = new DataTable();
                MySqlCommand command = new MySqlCommand(sqlQuery, conn);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(command);
                //MySqlDataAdapter myAdapter = new MySqlDataAdapter(sqlQuery, conn);
                myAdapter.Fill(myDataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
         return myDataTable;
    }
}
/*
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

class SimpleDataSource : IDisposable
{
    MySqlConnection Conn;
    string Server, Database, User, Password;
    int Port;

    /// <summary>
    /// Constructor, calls Connect method with params
    /// </summary>
    /// <param name="Server">Server IP or hostname</param>
    /// <param name="Database">Database/schema name</param>
    /// <param name="Port">Port number</param>
    /// <param name="User">Username</param>
    /// <param name="Password">Password</param>
    public SimpleDataSource(string Server, string Database, int Port,
        string User, string Password)
    {
        Connect(Server, Database, Port, User, Password);
    }

    /// <summary>
    /// Intialises MySqlConnection object with parameters provided.
    /// </summary>
    /// <param name="Server">Server IP or hostname</param>
    /// <param name="Database">Database/schema name</param>
    /// <param name="Port">Port number</param>
    /// <param name="User">Username</param>
    /// <param name="Password">Password</param>
    public void Connect(string Server, string Database, int Port,
        string User, string Password)
    {
    	this.Server = Server;
    	this.Database = Database;
    	this.Port = Port;
    	this.User = User;
    	this.Password = Password;

    	Conn = new MySqlConnection(
        	string.Format("server={0};user={1};database={2};port={3};password={4};",
        		Server, User, Database, Port, Password));
        try
        {
        	Conn.Open();
        } catch (MySqlException E)
        {
        	Console.WriteLine(E.Message);
        	Conn = null;
        }
    }

    /// <summary>
    /// Creates an SQL query from the provided string and
    /// executes it.
    /// </summary>
    /// <param name="QueryString">A string containing an SQL query</param>
    /// <returns>A MySqlDataReader object with the results 
    /// of the query or null if an exception occurs.</returns>
    public MySqlDataReader Query(string QueryString)
    {
    	lock (Conn)
    	{
	        MySqlDataReader Reader = null;
	        if (Conn != null)
	        {
	        	try
	        	{
	        		MySqlCommand Command = new MySqlCommand(QueryString, Conn);
	        		Reader = Command.ExecuteReader();
	        	} catch (MySqlException E)
	        	{
	        		Console.WriteLine(E.Message);
	        	}
	        }

        	return Reader;
    	}
    }

    /// <summary>
    /// Creates an SQL statement from the provided string and
    /// executes it. 
    /// </summary>
    /// <param name="updateString">A string containing an SQL non-query</param>
    public void Update(string UpdateString)
    {
    	lock (Conn)
    	{
	        if (Conn != null)
	        {
	        	try
	        	{
	        		MySqlCommand Command = new MySqlCommand(UpdateString, Conn);
	        		Command.ExecuteNonQuery();
	        	} catch (MySqlException E)
	        	{
	        		Console.WriteLine(E.Message);
	        	}
	        }
	    }
    }

    /// <summary>
    /// Garbage collection method called by Garbage Collector.
    /// SimpleDataSource implements IDisposable, so the GC will
    /// know to call this method when the object is no longer
    /// needed.
    /// </summary>
    public void Dispose()
    {
        if (Conn != null)
            Conn.Dispose();
    }
}
*/
/*
 in the xaml.cs, 
 button_clicked // delegate
 dataGrid.ItemsSource= SDS.QueryDataTable("SELECT * FROM Branch").DefaultView;
SELECT CONCAT(fName,' ', lName) AS 'Name', prefType AS 'Prefered Accomodation' FROM => Display one Name Colomn (Julie Tiercelin)
    public DataTable DataTableQuery(string sqlQuery)
   {
       DataTable myDataTable = null;
       if(conn!=null)
       {
          
    try
    {
            myDataTable = new DataTable();
           MySqlCommand command = new MySqlCommand(sqlQuery, conn);
           MySqlDataAdapter myAdapter = new MySqlDataAdapter(command);
           //MySqlDataAdapter = new MySqlDataAdapter( sqlQuery, conn);
           myAdapter.Fill(myDataTable);
      }
      catch (MySqlException E)
      {
        Console.WriteLine(
      }

       }
        return myDataTable;
   }
    * 
    * When we click on the name of the colomn we sort it */
