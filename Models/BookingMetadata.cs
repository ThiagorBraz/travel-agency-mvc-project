using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Travel_Agency_2nd_Semester_Project.Models
{
    [MetadataType(typeof(BookingMetadata))]
    public partial class Booking { }
    public class BookingMetadata
	{
        [Required(ErrorMessage = "Full name is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a gender.")]
        public int GenderID { get; set; }

        [Required(ErrorMessage = "Age group is required.")]
        public int AgeGroupID { get; set; }

        [Required(ErrorMessage = "Tour is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a tour.")]
        public int TourID { get; set; }

        [Required(ErrorMessage = "Number of participants is required.")]
        [Range(1, 100, ErrorMessage = "Please enter at least 1 participant.")]
        public int NumberOfParticipants { get; set; }

        [Required(ErrorMessage = "Tour date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date.")]
        public DateTime TourDate { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        public int PaymentMethodID { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions.")]
        public bool TermsAccepted { get; set; }
    }

}