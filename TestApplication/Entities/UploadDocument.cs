using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestApplication.Helpers;

namespace TestApplication.Entities
{
    public class UploadDocument
    {
        #region Properties  

        public int Id { get; set; }

        /// <summary>  
        /// Gets or sets Image file.  
        /// </summary>  
        [Required]
        [Display(Name = "Upload File")]
        [AllowFileSize(FileSize = 4 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 4 MB")]
        public HttpPostedFileBase FileAttach { get; set; }

        
        public string Name { get; set; }

        public int MemberId { get; set; }

        /// <summary>  
        /// Gets or sets a value indicating whether file size is valid or not property.  
        /// </summary>  
        public bool IsValid { get; set; }

        #endregion
    }
}