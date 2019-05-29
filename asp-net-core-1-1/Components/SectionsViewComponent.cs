using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Components
{
    public class SectionViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;
        public SectionViewComponent(IProductData productData)
        {
            _ProductData = productData;
        }

        public IViewComponentResult Invoke()
        {
            var section = GetSections();
            return View(section);
        }
        public IEnumerable<SectionViewModel> GetSections()
        {
            var sections = _ProductData.GetSections();

            var parent_sections = sections
                .Where(s => s.ParentId == null)
                .Select(s => new SectionViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order
                }).ToList();

            foreach (var parent_section in parent_sections)
            {
                var child_sections = sections.Where(s => s.ParentId == parent_section.Id)
                    .Select(section => new SectionViewModel
                    {
                        Id = section.Id,
                        Name = section.Name,
                        Order = section.Order
                    });
                parent_section.ChildSections.AddRange(child_sections);
                parent_section.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }
            parent_sections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            return parent_sections;
        }
    }
}