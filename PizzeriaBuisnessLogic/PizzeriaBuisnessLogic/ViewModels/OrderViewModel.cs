using PizzeriaBuisnessLogic.Enums;
using System;
using System.ComponentModel;


namespace PizzeriaBuisnessLogic.ViewModels
{
    public class OrderViewModel
    {
        public int? Id { get; set; }
        public int PizzaId { get; set; }
        [DisplayName("Pizza")]
        public string PizzaName { get; set; }
        [DisplayName("Amount")]
        public int Count { get; set; }
        [DisplayName("Summ")]
        public decimal Sum { get; set; }
        [DisplayName("Status")]
        public OrderStatus Status { get; set; }
        [DisplayName("Date of creation")]
        public DateTime DateCreate { get; set; }
        [DisplayName("Date of completion")]
        public DateTime? DateImplement { get; set; }
    }
}
