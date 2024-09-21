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

        public ActionResult Index()
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

        public ActionResult Create()
        {
            return View(new AppointmentModels());
        }

        //Post: Appointment Create also Pet and Owner
        [HttpPost]
        public ActionResult Create(AppointmentModels appointmentModel, OwnerClass ownerClass, PetClass petClass, VetClass vetClass)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string ownerQuery = "INSERT INTO OwnerTable VALUES (@ownerName, @ownerPhone, @ownerEmail)";
                string petQuery = "INSERT INTO PetTable VALUES(@petName, @petSpecies, @petBreed, @petGender, @petAge, @ownerID";
                string appointmentQuery = "INSERT INTO AppointmentTable VALUES (@owenerID, @petID, @vetID, @appointmentDate, @appointmentReason, @appointNote";
                SqlCommand ownerCmd = new SqlCommand(ownerQuery, connection);
                ownerCmd.Parameters.AddWithValue("@ownerName", ownerClass.ownerName);
                ownerCmd.Parameters.AddWithValue("@ownerPhone", ownerClass.ownerPhone);
                ownerCmd.Parameters.AddWithValue("@ownerEmail", ownerClass.ownerEmail);
                ownerCmd.ExecuteNonQuery();
                SqlCommand petCmd = new SqlCommand(petQuery, connection);
                petCmd.Parameters.AddWithValue("@petName", petClass.petName);
                petCmd.Parameters.AddWithValue("@petSpecies", petClass.petSpecies);
                petCmd.Parameters.AddWithValue("@petBreed", petClass.petBreed);
                petCmd.Parameters.AddWithValue("@petGender", petClass.petGender);
                petCmd.Parameters.AddWithValue("@petAge", petClass.petAge);
                petCmd.Parameters.AddWithValue("@ownerID", ownerClass.ownerID);
                petCmd.ExecuteNonQuery();
                SqlCommand appointmentCmd = new SqlCommand(appointmentQuery, connection);
                appointmentCmd.Parameters.AddWithValue("@ownerID", ownerClass.ownerID);
                appointmentCmd.Parameters.AddWithValue("@petID", petClass.petID);
                appointmentCmd.Parameters.AddWithValue("@vetID", vetClass.vetID);
                appointmentCmd.Parameters.AddWithValue("@appointmentDate", appointmentModel.appointmentDate);
                appointmentCmd.Parameters.AddWithValue("@appointmentReason", appointmentModel.appointmentReason);
                appointmentCmd.Parameters.AddWithValue("@appointmentNote", appointmentModel.appointmentNote);
                appointmentCmd.ExecuteNonQuery();

            }
            return RedirectToAction("Index");


        }

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
            return RedirectToAction("Index");
        }



        public ActionResult UpdateAppointment(int appointID)
        {
            AppointmentModels appointmentModel = new AppointmentModels();   
            DataTable appointmentData = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
            }

            return RedirectToAction("Index");
        }
    }
}