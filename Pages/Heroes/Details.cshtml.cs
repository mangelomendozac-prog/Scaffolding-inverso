using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Scaffolding_inverso.Data;
using Scaffolding_inverso.Models;
using Hero = Scaffolding_inverso.Models.Heroes;

namespace Scaffolding_inverso.Pages.Heroes
{
    public class DetailsModel : PageModel
    {
        private readonly Scaffolding_inverso.Data.HeroesContext _context;

        public DetailsModel(Scaffolding_inverso.Data.HeroesContext context)
        {
            _context = context;
        }

        public Hero Heroes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var heroes = await _context.Heroes.FirstOrDefaultAsync(m => m.Id == id);

            if (heroes is not null)
            {
                Heroes = heroes;

                return Page();
            }

            return NotFound();
        }
    }
}
