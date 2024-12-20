using System.Data.SqlClient;
using System.Data.SQLite;

ReadData(CreateConnecion());
//InsertCustomer(CreateConnecion());
RemoveCustomer(CreateConnecion));

static SQLiteConnection CreateConnecion()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version = 3; New = True; Compress = True");

    try
    {
        connection.Open();
        //Console.WriteLine("DB found.");
    }
    catch
    {
        Console.WriteLine("DB not found.");
    }
    return connection;
}


static void ReadData(SQLiteConnection myconnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myconnection.CreateCommand();
    command.CommandText = "SELECT * FROM customer";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader["rowId"].ToString();
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringDoB = reader.GetString(3);

        Console.WriteLine($"{readerRowId}. Full name: {readerStringFirstName} {readerStringLastName}; Status: {readerStringDoB}");

    }
        
    myconnection.Close();
}


static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName, DoB;

    Console.WriteLine("Enter first name:");
    fName = Console.ReadLine();
    Console.WriteLine("Enter last name:");
    lName = Console.ReadLine();
    Console.WriteLine("Enter date of birth (mm-dd-yyyy:");
    DoB = Console.ReadLine();   
    
    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT into customer(firstName, lastName, dateOfBirth)" +
    $"VALUES ('{fName}', '{lName}', '{DoB}')";

    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted: {rowInserted}");

    

    ReadData(myConnection);
}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter an id to delete a customer:");
    idToDelete = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} was removed from the table customer");

    ReadData(myConnection);
}