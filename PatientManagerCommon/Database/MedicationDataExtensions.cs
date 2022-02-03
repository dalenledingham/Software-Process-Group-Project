using Dapper;
using PatientManagerCommon.ViewModel;
using System.Data.SQLite;
using System.Linq;

namespace PatientManagerCommon.Database
{
    public static class MedicationDataExtensions
    {
        private static SQLiteConnection connection = DatabaseManager.Connection;

        public static void SaveInDatabase(this Medication data)
        {
            if (!data.ExistsInDatabase())
                data.SaveAsNewItem();
        }

        public static void RemoveFromDatabase(this Medication data)
        {
            RemoveFromDb(data);
        }

        public static bool ExistsInDatabase(this Medication data)
        {
            var rows = connection.Query(string.Format(
                "SELECT COUNT(1) as 'Count' " +
                "FROM Medications " +
                "WHERE MedicationID = '{0}' ",
                data.MedicationID));

            return (int)rows.First().Count > 0;
        }

        private static void SaveAsNewItem(this Medication data)
        {
            connection.ExecuteNonQuery(@"
            INSERT INTO Medications (PatientID, Name, Dosage)
            VALUES (@PatientID, @Name, @Dosage)",
            data);

            int medicationID = connection.Query<int>(string.Format(
                "SELECT MedicationID " +
                "FROM Medications " +
                "WHERE PatientID = '{0}' AND Name = '{1}' AND Dosage = '{2}'",
                data.PatientID, data.Name, data.Dosage)).Single();

            data.MedicationID = medicationID;
        }

        private static void RemoveFromDb(this Medication data)
        {
            connection.ExecuteNonQuery(
                "DELETE FROM Medications " +
                "WHERE MedicationID = @MedicationID",
                data);
        }
    }
}
