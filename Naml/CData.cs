using Naml;

namespace Naml
{
    public class CData
    {
        public string Data { get; private set; }
        public CData(string data)
        {
            Data = data;
        }

        public static explicit operator CData(string data)
        {
            return new CData(data);
        }
        
        public static explicit operator string(CData data)
        {
            return data.Safe(x => x.Data);
        }
    }
}
