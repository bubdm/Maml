using Naml;

namespace Naml
{
    /// <summary>
    /// Represents CData in an XML document.  Can be casted explicity to and from a string.
    /// </summary>
    public class CData
    {
        /// <summary>
        /// String data of the CData
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// Public constructore
        /// </summary>
        /// <param name="data">Initial string in CData object</param>
        public CData(string data)
        {
            Data = data;
        }

        /// <summary>
        /// Explicitly cast string to CData
        /// </summary>
        /// <param name="data">string to cast</param>
        /// <returns>CData equivalent</returns>
        public static explicit operator CData(string data)
        {
            return new CData(data);
        }
        
        /// <summary>
        /// Explicitly cast CData to string
        /// </summary>
        /// <param name="data">CData to cast</param>
        /// <returns>string equivalent</returns>
        public static explicit operator string(CData data)
        {
            return data.Safe(x => x.Data);
        }
    }
}
