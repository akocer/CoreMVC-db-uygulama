using System.ComponentModel.DataAnnotations;

namespace uy01.ViewModels
{
    public class ProductModel
    {
        public int Id { get; set; }



        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "Ürün Adı Giriniz!")]
        public string Name { get; set; }



        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Açıklama Giriniz!")]
        public string Description { get; set; }


        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "Fiyat Giriniz!")]
        public decimal Price { get; set; }


        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
    }
}
