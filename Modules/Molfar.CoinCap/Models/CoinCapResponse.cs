using System.Runtime.Serialization;

namespace MolfarCoinCap.Models
{
    [DataContract(Name = "coincap")]
    public class CoinCapResponse
    {
        [DataMember(Name = "price_usd")]
        public double USD { get; set; }
    }
}
