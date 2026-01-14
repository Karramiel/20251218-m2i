using MySql.Data.MySqlClient;
using SQL_Csharp;

string connectionString =
    "server=localhost;database=demo_ado;user id=root;password=formation;";

void AjouterPersonne()
{
    Console.WriteLine("Ajout d'une nouvelle personne:");
    
    Console.Write("Nom : ");
    string nom = Console.ReadLine();

    Console.Write("Prénom : ");
    string prenom = Console.ReadLine();

    Console.Write("Âge : ");
    int age = int.Parse(Console.ReadLine());

    Console.Write("Email : ");
    string email = Console.ReadLine();

    using MySqlConnection connection = new MySqlConnection(connectionString);

    try
    {
        connection.Open();
        string query = @"INSERT INTO Personne (Nom, Prenom, Age, Email)
                         VALUES (@Nom, @Prenom, @Age, @Email)";
        using MySqlCommand command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@Nom", nom);
        command.Parameters.AddWithValue("@Prenom", prenom);
        command.Parameters.AddWithValue("@Age", age);
        command.Parameters.AddWithValue("@Email", email);
        
        int rowsAffected = command.ExecuteNonQuery();

        if(rowsAffected > 0)
        {
            Console.WriteLine("Personne ajoutée avec succès.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur lors de l'ajout de la personne: " + ex.Message);
    }
    finally
    {
        connection.Close();
    }
}



void AfficherToutesLesPersonnes()
{
    Console.WriteLine("Liste des personnes:");
    using MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        connection.Open();
        string query = "SELECT * FROM Personne";
        using var command = new MySqlCommand(query, connection);
        using MySqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Personne p= new Personne(
                reader.GetInt32("Id"),
                reader.GetString("Nom"),
                reader.GetString("Prenom"),
                reader.GetInt32("Age"),
                reader.GetString("Email")
                );
                Console.WriteLine(p);
            }

        }
        else
        {
            Console.WriteLine("Aucune personne trouvée.");
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur lors de la récupération des personnes: " + ex.Message);
    }
    finally
    {
        connection.Close();
    }
}


void RechercherPersonneParId()
{
    Console.Write("Entrez l'ID de la personne à rechercher: ");

    int id = int.Parse(Console.ReadLine());

    using MySqlConnection connection = new MySqlConnection(connectionString);

    try
    {
        connection.Open();
        string query = "SELECT * FROM Personne WHERE Id = @Id";
     
        
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        using MySqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            Personne p = new Personne(
                            reader.GetInt32("Id"),
                            reader.GetString("Nom"),
                            reader.GetString("Prenom"),
                            reader.GetInt32("Age"),
                            reader.GetString("Email")
                            );
            Console.WriteLine("Personne trouvée: " + p);
        }
        else
        {
            Console.WriteLine("Aucune personne trouvée avec cet ID.");
        }
        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur lors de la recherche de la personne: " + ex.Message);
    }

}

void UpdaterPersonne()
{
    Console.Write("Entrez l'ID de la personne à mettre à jour: ");
    int id = int.Parse(Console.ReadLine());
    
    using MySqlConnection connection = new MySqlConnection(connectionString);

    try
    {
        connection.Open();

        string queryCheck = @"SELECT COUNT(*) FROM Personne WHERE Id = @Id";
        using MySqlCommand command = new MySqlCommand(queryCheck, connection);
        command.Parameters.AddWithValue("@Id", id);
        int count = Convert.ToInt32(command.ExecuteScalar());
        if (count == 0)
        {
            Console.WriteLine("Aucune personne trouvée avec cet ID.");
            return;
        }

        Console.Write("Nouveau Nom : ");
        var nom = Console.ReadLine();
        Console.Write("Nouveau Prénom : ");
        var prenom = Console.ReadLine();
        Console.Write("Nouveau Âge : ");
        var age = int.Parse(Console.ReadLine());
        Console.Write("Nouveau Email : ");
        var email = Console.ReadLine();
        
        string query = @"UPDATE Personne 
                         SET Nom = @Nom, Prenom = @Prenom, Age = @Age, Email = @Email
                         WHERE Id = @Id";


        using MySqlCommand commandUpdate = new MySqlCommand(query, connection);
        commandUpdate.Parameters.AddWithValue("@Nom", nom);
        commandUpdate.Parameters.AddWithValue("@Prenom", prenom);
        commandUpdate.Parameters.AddWithValue("@Age", age);
        commandUpdate.Parameters.AddWithValue("@Email", email);
        commandUpdate.Parameters.AddWithValue("@Id", id);

        int rowsAffected = commandUpdate.ExecuteNonQuery();
        if (rowsAffected > 0)
        { 
            Console.WriteLine("Personne mise à jour avec succès.");
        }
        else
        {  
            Console.WriteLine("Erreur : aucune modification effectuée.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur lors de la mise à jour de la personne: " + ex.Message);
    }
}


void DeletePersonne()
{
    Console.Write("Entrez l'ID de la personne à supprimer: ");
    int id = int.Parse(Console.ReadLine());
    using MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        connection.Open();
        string query = "DELETE FROM Personne WHERE Id = @Id";
        using MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        int rowsAffected = command.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
            Console.WriteLine("Personne supprimée avec succès.");
        }
        else
        {
            Console.WriteLine("Aucune personne trouvée avec cet ID.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur lors de la suppression de la personne: " + ex.Message);
    }
}   


AjouterPersonne();
AfficherToutesLesPersonnes();
RechercherPersonneParId();
UpdaterPersonne();
DeletePersonne();