﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models
{
    public class AddABookModel: IValidatableObject
    {
        [Required(ErrorMessage = "Please add a title")][MaxLength(200)]
        public string Title { get; set; }
        [Required][MaxLength(200)]
        public string Author { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required][Range(1, int.MaxValue)]
        public int NumberOfPages { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Title.ToLower() == "it" && Author.ToLower() == "king")
            {
                yield return new ValidationResult("I hate that book",
                    new string[] { "Title", "Author" });
            }
        }
    }
}
