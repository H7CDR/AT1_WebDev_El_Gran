using El_Gran_PetCare.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace El_Gran_PetCare.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PetCare_DB;Integrated Security=True;";

        public ActionResult AppointmentIndex()
        {
            var appointments = new List<AppointmentModels>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM AppointmentTable";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                  
                    appointments.Add(new AppointmentModels
                    {
                        appointmentID = reader.GetInt32(0),
                        ownerID = reader.GetInt32(1),
                        petID = reader.GetInt32(2),
                        vetID = reader.GetInt32(3),
                        appointmentDate = reader.GetDateTime(4),
                        appointmentReason = reader.GetString(5),
                        appointmentNote = reader.GetString(6),
                    });
                    
                }
            }
            return View(appointments);
        }

        public ActionResult VetIndex()
        {
            var vet = new List<VetClass>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM VetTable";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    vet.Add(new VetClass
                    { 
                        vetID = reader.GetInt32(0),
                        vetName = reader.GetString(1),
                        vetSpecialisation = reader.GetString(2),
                    });

                }
            }
            return View(vet);
        }

        public ActionResult OwnerIndex()
        {
            var owner = new List<OwnerClass>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM OwnerTable";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    owner.Add(new OwnerClass
                    {
                        ownerID = reader.GetInt32(0),
                        ownerName = reader.GetString(1),
                        ownerPhone = reader.GetString(2),
                        ownerEmail = reader.GetString(3),
                    });

                }
            }
            return View(owner);
        }

        public ActionResult PetIndex()
        {
            var pet = new List<PetClass>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM PetTable";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    pet.Add(new PetClass
                    {
                        petID = reader.GetInt32(0),
                        petName = reader.GetString(1),
                        petSpecies = reader.GetString(2),
                        petBreed = reader.GetString(3),
                        petGender = reader.GetBoolean(4),
                        petAge = reader.GetInt32(5),
                        ownerID = reader.GetInt32(6),
                    });

                }
            }
            return View(pet);
        }

        public ActionResult CreateAppointment()
        {
            return View(new AppointmentModels());
        }

        //Post: Appointment Create also Pet and Owner
        [HttpPost]
        public ActionResult CreateAppointment(AppointmentModels appointmentModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string appointmentQuery = "INSERT INTO AppointmentTable VALUES (@ownerID, @petID, @vetID, @appointmentDate, @appointmentReason, @appointmentNote)";
                SqlCommand appointmentCmd = new SqlCommand(appointmentQuery, connection);
                appointmentCmd.Parameters.AddWithValue("@ownerID", appointmentModel.ownerID);
                appointmentCmd.Parameters.AddWithValue("@petID", appointmentModel.petID);
                appointmentCmd.Parameters.AddWithValue("@vetID", appointmentModel.vetID);
                appointmentCmd.Parameters.AddWithValue("@appointmentDate", appointmentModel.appointmentDate);
                appointmentCmd.Parameters.AddWithValue("@appointmentReason", appointmentModel.appointmentReason);
                appointmentCmd.Parameters.AddWithValue("@appointmentNote", appointmentModel.appointmentNote);
                appointmentCmd.ExecuteNonQuery();

            }
            return RedirectToAction("AppointmentIndex");


        }

        public ActionResult CreateVet()
        {
            return View(new VetClass());
        }

        [HttpPost]
        public ActionResult CreateVet(VetClass vetClass)
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                connection.Open();
                string vetQuery = "INSERT INTO VetTable VALUES (@vetName, @vetSpecialisation)";
                SqlCommand vetCmd = new SqlCommand(vetQuery, connection);
                vetCmd.Parameters.AddWithValue("@vetName", vetClass.vetName);
                vetCmd.Parameters.AddWithValue("@vetSpecialisation", vetClass.vetSpecialisation);
                vetCmd.ExecuteNonQuery();
            }
            return RedirectToAction("VetIndex");
        }

        public ActionResult CreatePet()
        {
            return View(new PetClass());
        }

        [HttpPost]
        public ActionResult CreatePet(PetClass petClass)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string petQuery = "INSERT INTO PetTable VALUES(@petName, @petSpecies, @petBreed, @petGender, @petAge, @ownerID)";
                SqlCommand petCmd = new SqlCommand(petQuery, connection);
                petCmd.Parameters.AddWithValue("@petName", petClass.petName);
                petCmd.Parameters.AddWithValue("@petSpecies", petClass.petSpecies);
                petCmd.Parameters.AddWithValue("@petBreed", petClass.petBreed);
                petCmd.Parameters.AddWithValue("@petGender", petClass.petGender);
                petCmd.Parameters.AddWithValue("@petAge", petClass.petAge);
                petCmd.Parameters.AddWithValue("@ownerID", petClass.ownerID);
                petCmd.ExecuteNonQuery();
            }
            return RedirectToAction("PetIndex");
        }

        public ActionResult CreateOwner()
        {
            return View(new OwnerClass());
        }

        [HttpPost]
        public ActionResult CreateOwner(OwnerClass ownerClass)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string ownerQuery = "INSERT INTO OwnerTable VALUES (@ownerName, @ownerPhone, @ownerEmail)";
                SqlCommand ownerCmd = new SqlCommand(ownerQuery, connection);
                ownerCmd.Parameters.AddWithValue("@ownerName", ownerClass.ownerName);
                ownerCmd.Parameters.AddWithValue("@ownerPhone", ownerClass.ownerPhone);
                ownerCmd.Parameters.AddWithValue("@ownerEmail", ownerClass.ownerEmail);
                ownerCmd.ExecuteNonQuery();
            }
            return RedirectToAction("OwnerIndex");
        }


        public ActionResult UpdateAppointment(int appointID)
        {
            AppointmentModels appointmentModel = new AppointmentModels();   
            DataTable appointmentData = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM AppointmentTable WHERE appointmentID = @appointID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(appointmentData);
            }
            if (appointmentData.Rows.Count == 1)
            {
                appointmentModel.appointmentID = Convert.ToInt32(appointmentData.Rows[0][0].ToString());
                appointmentModel.ownerID = Convert.ToInt32(appointmentData.Rows[0][1].ToString());
                appointmentModel.petID = Convert.ToInt32(appointmentData.Rows[0][2].ToString());
                appointmentModel.vetID = Convert.ToInt32(appointmentData.Rows[0][3].ToString());
                appointmentModel.appointmentDate = Convert.ToDateTime(appointmentData.Rows[0][4].ToString());
                appointmentModel.appointmentReason = appointmentData.Rows[0][5].ToString();
                appointmentModel.appointmentNote = appointmentData.Rows [0][6].ToString();
                return View(appointmentModel);
            }
            else
            return RedirectToAction("AppointmentIndex");
        }

        [HttpPost]
        public ActionResult UpdateAppontment(AppointmentModels appointmentModel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            { 
                connection.Open();
                string query = "UPDATE ApponitmentTable SET ownerID = @ownerID, petID = @petID, vetID = @vetID, appointmentDate = @appointmentDate, appointmentReason = @appointmentReason, appointmentNote = @appointmentNote where appointmentID = @appointID";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@appointID", appointmentModel.appointmentID);
                cmd.Parameters.AddWithValue("@ownerID", appointmentModel.ownerID);
                cmd.Parameters.AddWithValue("@petID", appointmentModel.petID);
                cmd.Parameters.AddWithValue("@vetID", appointmentModel.vetID);
                cmd.Parameters.AddWithValue("@appointmentDate", appointmentModel.appointmentDate);
                cmd.Parameters.AddWithValue("@appointmentReason",appointmentModel.appointmentReason);
                cmd.Parameters.AddWithValue("@appointmentNote",appointmentModel.appointmentNote);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("AppointmentIndex"); 
        }


    }
}