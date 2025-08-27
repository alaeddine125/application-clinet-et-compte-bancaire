using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientApp.Models;

namespace ClientApp.Pages
{
    public class CompteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Nom { get; set; }

        public string NomClient => Nom;
        public int ClientId => Id;

        [BindProperty]
        public decimal Solde { get; set; }

        [BindProperty]
        public string RIB { get; set; }

        public Compte CompteCree { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            CompteCree = new Compte
            {
                ClientId = Id,
                NomClient = Nom,
                Solde = Solde,
                RIB = RIB
            };
        }
    }
}
