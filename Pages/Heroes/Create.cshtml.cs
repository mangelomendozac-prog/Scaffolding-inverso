using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scaffolding_inverso.Data;
using Scaffolding_inverso.Models;
using Hero = Scaffolding_inverso.Models.Heroes;

namespace Scaffolding_inverso.Pages.Heroes
{
    public class CreateModel : PageModel
    {
        private readonly Scaffolding_inverso.Data.HeroesContext _context;

        public CreateModel(Scaffolding_inverso.Data.HeroesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Hero Heroes { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Heroes.Add(Heroes);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
