using System.Collections.Generic;
using System.ComponentModel;

namespace PizzeriaBuisnessLogic.ViewModels
{
    public class PizzaViewModel
    {
        public int Id { get; set; }
        [DisplayName("Pizza name")]
        public string PizzaName { get; set; }
        [DisplayName("Price")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> PizzaComponents { get; set; }
    }
}
