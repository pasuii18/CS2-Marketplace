﻿namespace BlazorWebClient.Dtos.DealsDtos.DealHistory
{
    public class ItemDealsHistoryReadDto
    {
        public int IdItem { get; set; }
        public int? Rarity { get; set; }
        public string? SkinName  { get; set; }
        public string? ItemTypeName { get; set; }
        public string? ItemClassName { get; set; }
        public string? ItemSubClassName { get; set; }
    }
}
