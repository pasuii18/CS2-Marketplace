using CSMarketApp.Domain.Core.Models.ItemsModels;
using CSMarketApp.Services.Interfaces.Dtos.ItemsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMarketApp.Services.Interfaces.Dtos
{
    public class ItemSellingsDto
    {
        public ItemsReadDto Item { get; set; }
        public int DealsCount { get; set; }
        public int OffersCount { get; set; }
    }
}
