﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace El_Gran_PetCare.Models
{
    public class AppointmentModels
    {
        public int appointmentID { get; set; }

        public DateTime appointmentDate { get; set; }
        public string appointmentReason { get; set; }
        public string appointmentNote { get; set; }

        [ForeignKey("owner")]
        public int ownerID { get; set; }
        public virtual OwnerClass OwnerClass { get; set; }

        [ForeignKey("pet")]
        public int petID { get; set; }
        public virtual PetClass PetClass { get; set; }

        [ForeignKey("vet")]
        public int vetID { get; set; }
        public virtual VetClass VetClass { get; set; }




    }
    public class OwnerClass
    {
        public int ownerID { get; set; }
        public string ownerName { get; set; }
        public string ownerPhone { get; set; }
        public string ownerEmail { get; set; }


    }
    public class PetClass
    {
        public int petID { get; set; }
        public string petName { get; set; }
        public string petSpecies { get; set; }
        public string petBreed { get; set; }
        public string petGender { get; set; }
        public int petAge { get; set; }

        [ForeignKey("owner")]
        public int ownerID { get; set; }
        public virtual OwnerClass OwnerClass { get; set; }

    }
    public class VetClass
    {
        public int vetID { get; set; }
        public string vetName { get; set; }
        public string vetSpecialisation { get; set; }
    }
}
