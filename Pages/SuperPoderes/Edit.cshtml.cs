using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Scaffolding_inverso.Data;
using Scaffolding_inverso.Models;
using SuperPower = Scaffolding_inverso.Models.SuperPoderes;

namespace Scaffolding_inverso.Pages.SuperPoderes
{
    public class EditModel : PageModel
    {
        private readonly Scaffolding_inverso.Data.HeroesContext _context;

        public EditModel(Scaffolding_inverso.Data.HeroesContext context)
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

            var superpoderes =  await _context.SuperPoderes.FirstOrDefaultAsync(m => m.Id == id);
            if (superpoderes == null)
            {
                return NotFound();
            }
            SuperPoderes = superpoderes;
            ViewData["HeroeId"] = new SelectList(
                    _context.Heroes, "Id", "Nombre", SuperPoderes.HeroeId);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!await _context.Heroes.AnyAsync(h => h.Id == SuperPoderes.HeroeId))
            {
                ModelState.AddModelError("SuperPoderes.HeroeId", "Seleccione un héroe válido.");
            }

            if (!ModelState.IsValid)
            {
                ViewData["HeroeId"] = new SelectList(
                    _context.Heroes, "Id", "Nombre", SuperPoderes.HeroeId);
                return Page();
            }

            _context.Attach(SuperPoderes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuperPoderesExists(SuperPoderes.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SuperPoderesExists(int id)
        {
            return _context.SuperPoderes.Any(e => e.Id == id);
        }
    }
}
