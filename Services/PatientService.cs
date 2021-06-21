namespace play_web_api.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using play_web_api.Model;

    public static class PatientService
    {
        public static List<Patient> patientList;
        private static int Identifier = 1005;
        static PatientService()
        {
            patientList = new List<Patient>();
            patientList.Add(new() { PatientId = 1001, FirstName = "Venkatesan", LastName = "Rajendran" });
            patientList.Add(new() { PatientId = 1002, FirstName = "Harish", LastName = "Unnikrishnan" });
            patientList.Add(new() { PatientId = 1003, FirstName = "Saravanan", LastName = "Devarajan" });
            patientList.Add(new() { PatientId = 1004, FirstName = "David", LastName = "Pradeep" });
        }

        public static IEnumerable<Patient> ReadAllPatients() => patientList.AsReadOnly();

        public static Patient ReadByPatientId(int patientId) => patientList.FirstOrDefault(pt => pt.PatientId == patientId);

        public static void CreatePatient(Patient patient)
        {
            if (patient is null)
                throw new ArgumentNullException(nameof(patient));

            if (patientList.Any(pt => pt.PatientId == patient.PatientId))
                throw new Exception("Patient with same Id exists");

            patient.PatientId = Identifier++;
            patientList.Add(patient);
        }

        public static void Update(Patient patient)
        {
            if (patient is null)
                throw new ArgumentNullException(nameof(patient));

            int index = patientList.FindIndex(pt => pt.PatientId == patient.PatientId);
            if (index < 0)
                throw new Exception("Patient not found");

            patientList[index] = patient;
        }

        public static void Delete(Patient patient)
        {
            ReadByPatientId(patient.PatientId);
            if (patient is null)
                throw new Exception("Patient not found");
            patientList.Remove(patient);
        }
    }
}