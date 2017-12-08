using System;
using FreshMvvm;
using System.Collections.ObjectModel;
using Nearest_Pharmacy.Models;
using PropertyChanged;
using AutoMapper;

namespace Nearest_Pharmacy.PageModels
{
    [ImplementPropertyChanged]
    public class BasketPageModel:FreshBasePageModel
    {
        public ObservableCollection<Basket> basket { get; set; }

        public bool isEmpty = false;
        public BasketPageModel()
        {
            if (isEmpty !=true)
            {
                basket = new ObservableCollection<Basket>(Application.Database.GetBasket());
            }
        }

        public async override void Init(object initData)
        {
            if (initData != null)
            {
                try
                {
                    Product productItem = (Product)initData;
                    Mapper.Initialize(cfg => cfg.CreateMap<Product, Basket>());  
                    var basketItems = Mapper.Map<Product, Basket>(productItem);
                    
                  
                    Application.Database.AddBasket(basketItems);
                    basket = new ObservableCollection<Basket>(Application.Database.GetBasket());
                    isEmpty = false;
                }
                catch(Exception e)
                {
                    await CoreMethods.DisplayAlert(Convert.ToString(e), "", "Ок");
                }
            }
        }
    }
}
