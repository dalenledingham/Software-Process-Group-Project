using Dapper;
using PatientManagerCommon.ViewModel;
using System.Data.SQLite;
using System.Linq;

namespace PatientManagerCommon.Database
{
    public static class PatientDataExtensions
    {
        private static SQLiteConnection connection = DatabaseManager.Connection;

        public static int SaveInDatabase(this Patient data)
        {
            if (data.ExistsInDatabase())
                data.SaveChanges();
            else
                data.SaveAsNewItem();

            return data.PatientID;
        }

        public static void RemoveFromDatabase(this Patient data)
        {
            RemoveFromDb(data);
        }

        public static bool ExistsInDatabase(this Patient data)
        {
            var rows = connection.Query(string.Format(
                "SELECT COUNT(1) as 'Count' " +
                "FROM Patients " +
                "WHERE PatientID = '{0}' ",
                data.PatientID));

            return (int)rows.First().Count > 0;
        }

        private static void SaveAsNewItem(this Patient data)
        {
            connection.ExecuteNonQuery(@"
            INSERT INTO Patients (FirstName, LastName, AccountNumber)
            VALUES (@FirstName, @LastName, @AccountNumber)",
            data);

            data.PatientID = connection.Query<int>(string.Format(
                "SELECT PatientID " +
                "FROM Patients " +
                "WHERE AccountNumber = '{0}' ",
                data.AccountNumber)).Single();

            SaveMedications(data);
        }

        private static void SaveChanges(this Patient data)
        {
            connection.ExecuteNonQuery(@"
                UPDATE Patients
                SET FirstName = @FirstName, LastName = @LastName, AccountNumber = @AccountNumber
                WHERE PatientID = @PatientID",
                data);

            SaveMedications(data);
        }

        private static void RemoveFromDb(this Patient data)
        {
            connection.ExecuteNonQuery(
                "DELETE FROM Patients " +
                "WHERE PatientID = @PatientID",
                data);

            connection.ExecuteNonQuery(
                "DELETE FROM Medications " +
                "WHERE PatientID = @PatientID",
                data);
        }

        private static void SaveMedications(Patient patient)
        {
            foreach (Medication medication in patient.Medications)
            {
                medication.PatientID = patient.PatientID;
                medication.SaveInDatabase();
            }
        }
    }
}
