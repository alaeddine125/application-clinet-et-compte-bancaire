using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClientApp.Pages
{
    public class IndexModel : PageModel
    {
        // Liste des clients en mémoire
        public static List<Client> Clients { get; set; } = new List<Client>();
        private static int nextId = 1;

        [BindProperty]
        public Client NouveauClient { get; set; }

        // Champ de recherche
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        // Liste des clients filtrés (affichée dans la vue)
        public List<Client> ClientsFiltres { get; set; }

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
                ClientsFiltres = Clients;
            else
                ClientsFiltres = Clients
                    .Where(c => c.Nom.Contains(SearchTerm, System.StringComparison.OrdinalIgnoreCase)
                             || c.Prenom.Contains(SearchTerm, System.StringComparison.OrdinalIgnoreCase)
                             || c.CIN.ToString().Contains(SearchTerm)
                             || c.Telephone.ToString().Contains(SearchTerm))
                    .ToList();
        }

        public IActionResult OnPostAjouter()
        {
            NouveauClient.Id = nextId++;
            Clients.Add(NouveauClient);
            return RedirectToPage(new { SearchTerm });
        }

        public IActionResult OnPostSupprimer(int id)
        {
            var client = Clients.FirstOrDefault(c => c.Id == id);
            if (client != null) Clients.Remove(client);
            return RedirectToPage(new { SearchTerm });
        }

        public IActionResult OnPostModifier(int id, string nom, string prenom, long cin, long telephone)
        {
            var client = Clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
            {
                client.Nom = nom;
                client.Prenom = prenom;
                client.CIN = cin;
                client.Telephone = telephone;
            }
            return RedirectToPage(new { SearchTerm });
        }
    }
}
