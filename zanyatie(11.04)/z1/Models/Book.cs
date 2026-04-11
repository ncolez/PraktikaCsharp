using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название книги")]
        [Display(Name = "Название")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите автора")]
        [Display(Name = "Автор")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите ISBN")]
        [RegularExpression(@"^(?:\d{13}|\d{10})$", ErrorMessage = "ISBN должен содержать 10 или 13 цифр")]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите жанр")]
        [Display(Name = "Жанр")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите год издания")]
        [Range(1000, 2025, ErrorMessage = "Год должен быть от 1000 до 2025")]
        [Display(Name = "Год издания")]
        public int Year { get; set; }
    }
}