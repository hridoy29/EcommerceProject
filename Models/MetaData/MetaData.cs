using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Project_497.Models
{
    public class ProductMetadata
    {

       
        [Display(Name = "Product Identification Number")]
        public string productId;

        [Required]
        [Display(Name = "Product Name")]
        public string productName;

        [Required]
        [Display(Name = "Image")]
        public string image;

        [Required]
        [Display(Name = "Price")]
        public Nullable<double> price;

        [Display(Name = "Product Storage Date")]
        public Nullable<System.DateTime> sellStartDate;

        
        [Display(Name = "Product Sell Date")]
        public Nullable<System.DateTime> sellEndDate;

        

        [Display(Name = "Number Of Click")]
        public Nullable<int> click { get; set; } 

        [Display(Name = "Offer Image")]
        public string offerImage { get; set; }

        [Display(Name = "Offer Details")]
        public string offerDetails { get; set; }

        [Display(Name = "Offer Available")]
        public Nullable<System.DateTime> offerAvailable { get; set; }
        [Display(Name ="Product Remaining")]
        public Nullable<int> Buy { get; set; }


    }
    //For Category class....
    public class CategoryMetadata
    {
        [Required]
        [Display(Name = "Category Name")]
        public string name;
        [Display(Name = "Category")]
        public int categoryId;
    }
   

    
    //For User class...
    public class AdminMetaData
    {
        [Required]
        [Display(Name = "User ID")]
        public string userId;


        [Required]
        [Display(Name = "User Name")]
        public string userName;

        [Required]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 6)]
        public string password;

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("password")]
        [StringLength(50, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email Id")]

        public string EmailId { get; set; }
    }


    //it's For Percentage class...
   
    public class PercentageMetadata
    {
        [Required]
        [Display(Name ="Set Up Noise")]
        public int percentage1 { get; set; }
    }

    public class Product_SellMetadata
    {
        
        [Required]
        [Display(Name = "Product Name")]
        public string productName;

        [Required]
        [Display(Name = "Price")]
        public Nullable<double> price;

        [Display(Name = "Product Storage Date")]
        public Nullable<System.DateTime> sellStartDate;


        [Display(Name = "Product Sell Date")]
        public Nullable<System.DateTime> sellEndDate;
    }
    public class SubCategoryIDMetadata
    {
        [Display(Name = "Category")]
        public Nullable<int> categoryid;
        [Required]
        [Display(Name = "Sub Category")]
        public string name;
    }
        //****************************//


    [MetadataType(typeof(ProductMetadata))]
    public partial class product
    {

    }

    [MetadataType(typeof(CategoryMetadata))]
    public partial class category
    {

    }

   
    [MetadataType(typeof(AdminMetaData))]
    public partial class Admin
    {

    }
    [MetadataType(typeof(PercentageMetadata))]
    public partial class Percentage
    {

    }
    [MetadataType(typeof(Product_SellMetadata))]
    public partial class Product_Sell
    {

    }
    [MetadataType (typeof(SubCategoryIDMetadata))]
    public partial class SubCategoryID
    {

    }
}
