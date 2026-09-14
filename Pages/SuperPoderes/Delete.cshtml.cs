using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Scaffolding_inverso.Data;
using Scaffolding_inverso.Models;
using SuperPower = Scaffolding_inverso.Models.SuperPoderes;

namespace Scaffolding_inverso.Pages.SuperPoderes
{
    public class DeleteModel : PageModel
    {
        private readonly Scaffolding_inverso.Data.HeroesContext _context;

        public DeleteModel(Scaffolding_inverso.Data.HeroesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SuperPower SuperPoderes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superpoderes = await _context.SuperPoderes.FirstOrDefaultAsync(m => m.Id == id);

            if (superpoderes is not null)
            {
                SuperPoderes = superpoderes;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var superpoderes = await _context.SuperPoderes.FindAsync(id);
            if (superpoderes != null)
            {
                SuperPoderes = superpoderes;
                _context.SuperPoderes.Remove(SuperPoderes);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
