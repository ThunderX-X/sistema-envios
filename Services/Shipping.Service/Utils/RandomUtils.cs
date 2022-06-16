namespace Shipments.Service.Utils
{
    public class RandomUtils
    {
        private static Random random = new Random();
        public string RandomString(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(characters, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
