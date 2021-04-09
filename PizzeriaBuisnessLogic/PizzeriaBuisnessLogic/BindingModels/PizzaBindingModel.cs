﻿using System.Collections.Generic;

namespace PizzeriaBuisnessLogic.BindingModels
{
    public class PizzaBindingModel
    {
        public int? Id { get; set; }
        public string PizzaName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> PizzaComponents { get; set; }
    }
}
