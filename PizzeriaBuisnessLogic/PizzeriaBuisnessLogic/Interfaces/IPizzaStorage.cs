using PizzeriaBuisnessLogic.BindingModels;
using PizzeriaBuisnessLogic.ViewModels;
using System.Collections.Generic;

namespace PizzeriaBuisnessLogic.Interfaces
{
    public interface IPizzaStorage
    {
        List<PizzaViewModel> GetFullList();
        List<PizzaViewModel> GetFilteredList(PizzaBindingModel model);
        PizzaViewModel GetElement(PizzaBindingModel model);
        void Insert(PizzaBindingModel model);
        void Update(PizzaBindingModel model);
        void Delete(PizzaBindingModel model);
    }
}
