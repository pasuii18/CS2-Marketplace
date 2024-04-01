using BlazorWebClient.Dtos.DealsDtos.DealHistory;

namespace BlazorWebClient.Scripts
{
    public static class StyleHelper
    {
        public static string GetRarityColor(int? rarity)
        {
            return rarity switch
            {
                1 => "color: rgb(176, 195, 217);",
                2 => "color: rgb(75, 105, 255);",
                3 => "color: rgb(136, 71, 255);",
                4 => "color: rgb(211, 44, 230);",
                5 => "color: rgb(235, 75, 75);",
                6 => "color: rgb(255, 215, 0);",
                _ => "color: rgb(75, 105, 255);"
            };
        }

        public static string GetRoleColor(string? role)
        {
            return role switch
            {
                "Member" => "color: rgb(111, 255, 59);",
                "Administrator" => "color: rgb(252, 63, 63);",
                "Developer" => "color: rgb(250, 95, 201);",
                _ => "color: rgb(111, 255, 59);"
            };
        }
        
        public static string GetUserHistoryColor(string userUuid, string currentUserUuid, string pageUserUuid)
        {
            return (userUuid == currentUserUuid) ? "color: rgb(255, 170, 59);" :
                (userUuid == pageUserUuid) ? "color: rgb(59, 203, 255);" :
                "color: black;";
        }
    }
}
