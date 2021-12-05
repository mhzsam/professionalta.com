using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstZX.Datalayer.Entities.User
{
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RoleId { get; set; }
        [Display(Name = "Role Title")]
        [Required(ErrorMessage = "please enter {0}")]
        [MaxLength(200,ErrorMessage = "maxlenth is 200")]
        public string RoleTitle { get; set; }



        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }


        #endregion
    }
}