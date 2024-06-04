namespace MIS.Application._Helpers
{
    public static class CodeHelper
    {
        public static string GenerateGuestCode()
        {
            return GenerateCode("G");
        }

        public static string GenerateMemberCode(int? count = null)
        {
            // count is used when importing member data
            return GenerateCode(count != null ? $"M{count}" : "M");
        }

        private static string GenerateCode(string type)
        {
            var dateNow = DateTime.Now;
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var randomCode = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return $"{dateNow:yyyyMMddHHmm}-{type}-{randomCode}";
        }
    }
}
