namespace Bookify.Web.Core.ViewModels
{
    public class CategoryFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(100,ErrorMessage ="Max Length cannot be more than 100")]
        public string Name { get; set; } = null!;
    }
}
