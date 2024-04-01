using System.Text;

namespace CSMarketApp.Algorithms
{
    static public class UUIDGenerating
    {
        static public string GenerateUUID()
        {
            Guid uuid = Guid.NewGuid();
            string uuidString = uuid.ToString("N");
            string uuidShort = uuidString.Substring(0, 10);
            return uuidShort;
        }
    }
}
