namespace CSMarketApp.Infrastructure.Business.Algorithms
{
    public static class UUIDGenerating
    {
        public static string GenerateUUID()
        {
            Guid uuid = Guid.NewGuid();
            string uuidString = uuid.ToString("N");
            string uuidShort = uuidString.Substring(0, 10);
            return uuidShort;
        }
    }
}
