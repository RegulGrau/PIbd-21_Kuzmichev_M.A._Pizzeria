using System.ComponentModel;

namespace PizzeriaBuisnessLogic.ViewModels
{
    public class ComponentViewModel
    {
        public int Id { get; set; }
        [DisplayName("Component name")]
        public string ComponentName { get; set; }
    }
}
