using El_Gran_PetCare.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Web.DynamicData;

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


        public ActionResult UpdateAppointment(int appointmentID)
        {
            AppointmentModels appointmentModel = new AppointmentModels();   
            DataTable appointmentData = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM AppointmentTable WHERE appointmentID = @appointmentID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@appointmentID",appointmentID);
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
                string query = "UPDATE ApponitmentTable SET ownerID = @ownerID, petID = @petID, vetID = @vetID, appointmentDate = @appointmentDate, appointmentReason = @appointmentReason, appointmentNote = @appointmentNote where appointmentID = @appointmentID";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@appointmentID", appointmentModel.appointmentID);
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

        public ActionResult UpdateVet(int vetID)
        {
            VetClass vetModel = new VetClass();
            DataTable vetData = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM VetTable WHERE vetID = @vetID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@vetID", vetID);
                adapter.Fill(vetData);
            }
            if (vetData.Rows.Count == 1)
            {
                vetModel.vetID = Convert.ToInt32(vetData.Rows[0][0].ToString());
                vetModel.vetName = vetData.Rows[0][1].ToString();
                vetModel.vetSpecialisation = vetData.Rows[0][2].ToString();

                return View(vetModel);
            }
            else
                return RedirectToAction("VetIndex");
        }

        [HttpPost]
        public ActionResult UpdateVet(VetClass vetClass)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE VetTable SET vetName = @vetName, vetSpecialisation = @vetSpecialisation WHERE vetID = @vetID";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@vetID", vetClass.vetID);
                cmd.Parameters.AddWithValue("@vetName", vetClass.vetName);
                cmd.Parameters.AddWithValue("@vetSpecialisation", vetClass.vetSpecialisation);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("VetIndex");
        }


        public ActionResult UpdateOwner(int ownerID)
        {
            OwnerClass ownerModel = new OwnerClass();
            DataTable ownerData = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM OwnerTable WHERE ownerID = @ownerID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@ownerID", ownerID);
                adapter.Fill(ownerData);
            }
            if (ownerData.Rows.Count == 1)
            {
                ownerModel.ownerID = Convert.ToInt32(ownerData.Rows[0][0].ToString());
                ownerModel.ownerName = ownerData.Rows[0][1].ToString();
                ownerModel.ownerPhone = ownerData.Rows[0][2].ToString();
                ownerModel.ownerEmail = ownerData.Rows[0][3].ToString();

                return View(ownerModel);
            }
            else
                return RedirectToAction("OwnerIndex");
        }

        [HttpPost]
        public ActionResult UpdateOwner(OwnerClass ownerClass)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE OwnerTable SET ownerName = @ownerName, ownerPhone = @ownerPhone, ownerEmail = @ownerEmail WHERE ownerID = @ownerID";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ownerID", ownerClass.ownerID);
                cmd.Parameters.AddWithValue("@ownerName", ownerClass.ownerName);
                cmd.Parameters.AddWithValue("@ownerPhone", ownerClass.ownerPhone);
                cmd.Parameters.AddWithValue("@ownerEmail", ownerClass.ownerEmail);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("OwnerIndex");
        }

        public ActionResult UpdatePet(int petID)
        {
            PetClass petModel = new PetClass();
            DataTable petData = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM PetTable WHERE petID = @petID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@petID", petID);
                adapter.Fill(petData);
            }
            if (petData.Rows.Count == 1)
            {
                petModel.petID = Convert.ToInt32(petData.Rows[0][0].ToString());
                petModel.petName = petData.Rows[0][1].ToString();
                petModel.petSpecies = petData.Rows[0][2].ToString();
                petModel.petBreed = petData.Rows[0][3].ToString();
                petModel.petGender = (Convert.ToBoolean(petData.Rows[0][4]));
                petModel.petAge = Convert.ToInt32(petData.Rows[0][5].ToString());
                petModel.ownerID = Convert.ToInt32(petData.Rows[0][6].ToString());

                return View(petModel);
            }
            else
                return RedirectToAction("PetIndex");
        }

        [HttpPost]
        public ActionResult UpdatePet(PetClass petClass)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE PetTable SET petName = @petName, petSpecies = @petSpecies, petBreed = @petBreed, petGender = @petGender, petAge =@petAge, ownerID = @ownerID  WHERE petID = @petID";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@petID", petClass.petID);
                cmd.Parameters.AddWithValue("@petName", petClass.petName);
                cmd.Parameters.AddWithValue("@petSpecies", petClass.petSpecies);
                cmd.Parameters.AddWithValue("@petBreed", petClass.petBreed);
                cmd.Parameters.AddWithValue("@petGender", petClass.petGender);
                cmd.Parameters.AddWithValue("@petAge", petClass.petAge);
                cmd.Parameters.AddWithValue("@ownerID", petClass.ownerID);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("PetIndex");
        }

        public ActionResult DeleteVet(int vetID)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM VetTable WHERE vetID = @vetID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@vetID", vetID);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("VetIndex");
        }
        public ActionResult DeleteOwner(int ownerID)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM OwnerTable WHERE ownerID = @ownerID";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@ownerID", ownerID);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction("OwnerIndex");
            }
            catch (Exception ex) { return RedirectToAction("OwnerIndex"); };

        }
        public ActionResult DeletePet(int petID)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM PetTable WHERE petID = @petID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@petID", petID);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("PetIndex");
        }
        public ActionResult DeleteAppointment(int appointmentID)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE FROM AppointmentTable WHERE appointmentID = @appointmentID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@appointmentID", appointmentID);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("AppointmentIndex");
        }

        public ActionResult DisplayReport() 
        {
            
            


            
            return View(); 
        }
    }



}
