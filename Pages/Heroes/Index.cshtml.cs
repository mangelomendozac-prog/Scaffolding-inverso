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
    public class IndexModel : PageModel
    {
        private readonly Scaffolding_inverso.Data.HeroesContext _context;

        public IndexModel(Scaffolding_inverso.Data.HeroesContext context)
        {
            _context = context;
        }

        public IList<Hero> Heroes { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Heroes = await _context.Heroes.ToListAsync();
        }
    }
}
