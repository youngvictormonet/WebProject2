using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
namespace WebShop.ViewModels.Product
{
    public class FilterViewModel
    {
        public FilterViewModel(List<ProductListingModel> companies, string company, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            companies.Insert(0, new ProductListingModel{ Name = "All", Description= "All" });
            Companies = new SelectList(companies, "Description", "Name", company);
            SelectedCompany = company;
            SelectedName = name;
        }
        public SelectList Companies { get; private set; } // список компаний
        public string SelectedCompany { get; private set; }   // выбранная компания
        public string SelectedName { get; private set; }    // введенное имя
    }
}
