using Dapper;
using PatientManagerCommon.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace PatientManagerCommon.Database
{
    public static class DatabaseManager
    {
        public static SQLiteConnection Connection;

        public static void InitializeDatabase()
        {
            var filePath = "PatientDB.sqlite";

            bool newDatabase = false;
            if (!File.Exists(filePath))
            {
                SQLiteConnection.CreateFile(filePath);
                newDatabase = true;
            }

            Connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", filePath));
            Connection.Open();

            if (newDatabase)
                SeedDatabase();
        }

        public static void ExecuteNonQuery(this SQLiteConnection connection, string commandText, object param = null)
        {
            if (connection == null)
                throw new NullReferenceException("Please provide a connection");

            if (connection.State != ConnectionState.Open)
                connection.Open();

            connection.Execute(commandText, param);
        }

        public static Patient GetPatient(string lastName, string accountNumber)
        {
            Patient patient = Connection.Query<Patient>(string.Format(
                "SELECT * FROM Patients " +
                "WHERE LastName = '{0}'",
                lastName)).FirstOrDefault();

            if(patient != null)
                patient.Medications = new ObservableCollection<Medication>(GetMedicationsByPatient(patient.PatientID).ToList());

            return patient;
        }

        public static IEnumerable<Patient> GetAllPatients()
        {
            IEnumerable<Patient> patients = Connection.Query<Patient>(
                "SELECT PatientID, FirstName, LastName, AccountNumber FROM Patients");

            foreach (Patient patient in patients)
                patient.Medications = new ObservableCollection<Medication>(GetMedicationsByPatient(patient.PatientID).ToList());

            return patients;
        }

        public static IEnumerable<Medication> GetMedicationsByPatient(int patientID)
        {
            return Connection.Query<Medication>(string.Format(
                "SELECT * FROM Medications " +
                "WHERE PatientID = {0}",
                patientID));
        }

        public static void SeedDatabase()
        {
            Connection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [Patients] (
                    [PatientID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [FirstName] TEXT,
                    [LastName] TEXT,
                    [AccountNumber] TEXT
                )");

            Connection.ExecuteNonQuery(@"
                CREATE TABLE IF NOT EXISTS [Medications] (
                    [MedicationID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [PatientID] INTEGER NOT NULL,
                    [Name] TEXT,
                    [Dosage] TEXT,
                    FOREIGN KEY(PatientID) REFERENCES Patients(PatientID)
                )");
        }
    }
}
