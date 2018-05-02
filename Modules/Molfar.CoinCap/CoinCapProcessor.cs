using MolfarCoinCap.Models;
using Molfar.Core;
using Molfar.Core.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Molfar.CoinCap
{
    public class CoinCapProcessor : MolfarCommandProcessor
    {
        private static readonly HttpClient client = new HttpClient();
        private const string BASE_URI = "http://coincap.io/page/";


        public override bool CanExcecute(string message)
        {
            bool result = false;
            var parts = message.Split(' ');
            if (parts.Length == 2)
            {
                result = true;
            }

            return result;
        }

        public override async Task<MolfarAnswer> ExcecuteCommand(string message)
        {
            var parts = message.Split(' ');
            var cur1 = parts[1];
            var answer = await GetRate(cur1);
            return new MolfarAnswer(answer);
        }

        private async Task<string> GetRate(string cur1)
        {
            var lastRate = await RequestRate(cur1);
            return $"1 {cur1.ToUpper()} -> {lastRate} USD";
        }

        private async Task<double> RequestRate(string cur1)
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", "molfar");


            string uri = $"{BASE_URI}{cur1.ToUpper()}";

            //var stringTask = await client.GetStringAsync(uri);

            var streamTask = client.GetStreamAsync(uri);


            var serializer = new DataContractJsonSerializer(typeof(CoinCapResponse));
            var coinCapResponse = serializer.ReadObject(await streamTask) as CoinCapResponse;

            var msg = coinCapResponse.USD;
            return msg;
        }
    }
}
