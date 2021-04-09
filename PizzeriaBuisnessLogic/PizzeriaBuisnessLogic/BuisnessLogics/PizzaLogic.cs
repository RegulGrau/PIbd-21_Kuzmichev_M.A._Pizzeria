using PizzeriaBuisnessLogic.BindingModels;
using PizzeriaBuisnessLogic.Interfaces;
using PizzeriaBuisnessLogic.ViewModels;
using System;
using System.Collections.Generic;


namespace PizzeriaBuisnessLogic.BuisnessLogics
{
    public class PizzaLogic
    {
        private readonly IPizzaStorage _pizzaStorage;
        public PizzaLogic(IPizzaStorage pizzaStorage)
        {
            _pizzaStorage = pizzaStorage;
        }
        public List<PizzaViewModel> Read(PizzaBindingModel model)
        {
            if (model == null)
            {
                return _pizzaStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<PizzaViewModel> { _pizzaStorage.GetElement(model)};
            }
            return _pizzaStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(PizzaBindingModel model)
        {
            var pizza = _pizzaStorage.GetElement(new PizzaBindingModel
            {
                PizzaName = model.PizzaName
            });
            if (pizza != null && pizza.Id != model.Id)
            {
                throw new Exception("There is already a Pizza with the same name");
            }
            if (model.Id.HasValue)
            {
                _pizzaStorage.Update(model);
            }
            else
            {
                _pizzaStorage.Insert(model);
            }
        }
        public void Delete(PizzaBindingModel model)

        {
            var element = _pizzaStorage.GetElement(new PizzaBindingModel
            {
                Id =
           model.Id
            });
            if (element == null)
            {
                throw new Exception("Pizza not found");
            }
            _pizzaStorage.Delete(model);
        }
    }
}
