using LibraryAPI.Interface.Utility;

namespace LibraryAPI.Service
{
    public class SerialNumberGeneratorService : ISerialNumberGeneratorService
    {
        //Format: SN-20250407-9F4C2A
        public string GenereateBookCopySerialNumber()
        {
            return $"SN-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
        }
    }
}
