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
    public class IndexModel : PageModel
    {
        private readonly Scaffolding_inverso.Data.HeroesContext _context;

        public IndexModel(Scaffolding_inverso.Data.HeroesContext context)
        {
            _context = context;
        }

        public IList<SuperPower> SuperPoderes { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SuperPoderes = await _context.SuperPoderes
                .Include(s => s.Heroe).ToListAsync();
        }
    }
}
