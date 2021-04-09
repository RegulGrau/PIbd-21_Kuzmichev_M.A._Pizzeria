using PizzeriaBuisnessLogic.BindingModels;
using PizzeriaBuisnessLogic.Interfaces;
using PizzeriaBuisnessLogic.ViewModels;
using PizzeriaListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PizzeriaListImplement.Implements
{
    public class PizzaStorage : IPizzaStorage
    {
        private readonly DataListSingleton source;
        public PizzaStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<PizzaViewModel> GetFullList()
        {
            List<PizzaViewModel> result = new List<PizzaViewModel>();
            foreach (var component in source.Pizzas)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<PizzaViewModel> GetFilteredList(PizzaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<PizzaViewModel> result = new List<PizzaViewModel>();
            foreach (var pizza in source.Pizzas)
            {
                if (pizza.PizzaName.Contains(model.PizzaName))
                {
                    result.Add(CreateModel(pizza));
                }
            }
            return result;
        }
        public PizzaViewModel GetElement(PizzaBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var pizza in source.Pizzas)
            {
                if (pizza.Id == model.Id || pizza.PizzaName ==
                model.PizzaName)
                {
                    return CreateModel(pizza);
                }
            }
            return null;
        }
        public void Insert(PizzaBindingModel model)
        {
            Pizza tempPizza = new Pizza
            {
                Id = 1,
                PizzaComponents = new
            Dictionary<int, int>()
            };
            foreach (var pizza in source.Pizzas)
            {
                if (pizza.Id >= tempPizza.Id)
                {
                    tempPizza.Id = pizza.Id + 1;
                }
            }
            source.Pizzas.Add(CreateModel(model, tempPizza));
        }
        public void Update(PizzaBindingModel model)
        {
            Pizza tempPizza = null;
            foreach (var pizza in source.Pizzas)
            {
                if (pizza.Id == model.Id)
                {
                    tempPizza = pizza;
                }
            }
            if (tempPizza == null)
            {
                throw new Exception("Element not found");
            }
            CreateModel(model, tempPizza);
        }
        public void Delete(PizzaBindingModel model)
        {
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                if (source.Pizzas[i].Id == model.Id)
                {
                    source.Pizzas.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Element not found");
        }
        private Pizza CreateModel(PizzaBindingModel model, Pizza pizza)
        {
            pizza.PizzaName = model.PizzaName;
            pizza.Price = model.Price;
            foreach (var key in pizza.PizzaComponents.Keys.ToList())
            {
                if (!model.PizzaComponents.ContainsKey(key))
                {
                    pizza.PizzaComponents.Remove(key);
                }
            }
            foreach (var component in model.PizzaComponents)
            {
                if (pizza.PizzaComponents.ContainsKey(component.Key))
                {
                    pizza.PizzaComponents[component.Key] =
                    model.PizzaComponents[component.Key].Item2;
                    
                }
                else
                {
                    pizza.PizzaComponents.Add(component.Key,
                    model.PizzaComponents[component.Key].Item2);
                }
            }
            return pizza;
        }
        private PizzaViewModel CreateModel(Pizza pizza)
        {
        Dictionary<int, (string, int)> pizzaComponents = new
        Dictionary<int, (string, int)>();
            foreach (var pc in pizza.PizzaComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (pc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                pizzaComponents.Add(pc.Key, (componentName, pc.Value));
            }
            return new PizzaViewModel
            {
                Id = pizza.Id,
                PizzaName = pizza.PizzaName,
                Price = pizza.Price,
                PizzaComponents = pizzaComponents
            };
        }
    }
}

