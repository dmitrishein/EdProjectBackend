using EdProject.DAL.Entities.Enums;

namespace EdProject.BLL.Models.PrintingEditions
{
    public class EditionModel:BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public EditionStatusTypes Status { get; set; }
        public CurrencyTypes Currency { get; set; }
        public EditionStatusTypes Type { get; set; }
    }
}
