using PizzeriaBuisnessLogic.BindingModels;
using PizzeriaBuisnessLogic.Enums;
using PizzeriaBuisnessLogic.Interfaces;
using PizzeriaBuisnessLogic.ViewModels;
using System;
using System.Collections.Generic;


namespace PizzeriaBuisnessLogic.BuisnessLogics
{
    public class OrderLogic
    {
        int idcount = 0;
        private readonly IOrderStorage _orderStorage;
        public OrderLogic(IOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
        }
        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }
            return _orderStorage.GetFilteredList(model);
        }
        public void CreateOrder(CreateOrderBindingModel model)
        {
            idcount++;
            _orderStorage.Insert(new OrderBindingModel
            {
                Id = idcount,
                PizzaId = model.PizzaId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Accepted
            });
        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id =
           model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            if (order.Status != OrderStatus.Accepted)
            {
                throw new Exception("The order is not in the status \"Accepted \"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                PizzaId = order.PizzaId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Performed
            });
        }
        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id =
           model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            if (order.Status != OrderStatus.Performed)
            {
                throw new Exception("The order is not in the status \"Performed\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                PizzaId = order.PizzaId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Ready
            });
        }
        public void PayOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel
            {
                Id = model.OrderId
            });
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            if (order.Status != OrderStatus.Ready)
            {
                throw new Exception("The order is not in the status \"Ready\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                PizzaId = order.PizzaId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Paid
            });
        }
    }
}
