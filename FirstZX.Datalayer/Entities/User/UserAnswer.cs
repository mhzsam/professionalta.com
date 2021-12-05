using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FirstZX.Datalayer.Entities.User
{
    public class UserAnswer
    {
        public UserAnswer()
        {
            
            
        }
        [Key]
        public int UserAnswerId { get; set; }

        public int UserId { get; set; }
        
        public string Email { get; set; }
        [DataType(DataType.DateTime, ErrorMessage = "Invalid Date Format")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "DateTime")]
        public DateTime DateTime { get; set; }
        public bool IsDeactive { get; set; }
        public bool IsDelete { get; set; }



        #region relation
        public User User { get; set; }
        public List<Answer> Answers { get; set; }
       
        

        #endregion
    }
}