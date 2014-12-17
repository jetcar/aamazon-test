using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using uptimeTest.AmazonReference1;
using uptimeTest.Repositories.AmazonHelpers;

namespace uptimeTest.Repositories
{
    public class AmazonRepository : IAmazonRepository
    {
        public AmazonRepository()
        {
            client = new AWSECommerceServicePortTypeClient();
            var keyid = Encoding.ASCII.GetString(Convert.FromBase64String(ConfigurationManager.AppSettings["keyid"]));
            var key = Encoding.ASCII.GetString(Convert.FromBase64String(ConfigurationManager.AppSettings["key"]));
            client.ChannelFactory.Endpoint.Behaviors.Add(new AmazonSigningEndpointBehavior(keyid, key));

        }

        private AWSECommerceServicePortTypeClient client;

        public List<Item> ItemSearch(string search, int page, int pagesize, out int totalpages)
        {
            var result = new List<Item>();
            var amazonPage = page * pagesize / 10;
            while (true)
            {
                var response =
                    client.ItemSearch(
                        new ItemSearchRequest1(new ItemSearch()
                        {
                            AssociateTag = "MyTag",
                            Request =
                                new ItemSearchRequest[] { new ItemSearchRequest() { ResponseGroup = new string[] { "Offers", "Large", "Request" }, Keywords = search, SearchIndex = "All", ItemPage = amazonPage.ToString() }, }
                        }));
                var totalResults = Convert.ToInt32(response.ItemSearchResponse.Items[0].TotalResults);
                {
                    totalpages = totalResults / pagesize;
                    if (totalResults % pagesize > 0)
                        totalpages++;
                    int i = (page - 1) * pagesize - ((amazonPage - 1) * 10);
                    if (i < 0)
                        i = 0;
                    for (; i < response.ItemSearchResponse.Items[0].Item.Length; i++)
                    {
                        var item = response.ItemSearchResponse.Items[0].Item[i];

                        result.Add(item);

                        if (result.Count >= pagesize)
                            return result;
                    }
                    if (pagesize * page >= totalResults)
                        return result;
                    if (totalResults < pagesize)
                        return result;
                    amazonPage++;
                    if (amazonPage > 5)
                        return result;
                }
            }
        }

    }

    public interface IAmazonRepository
    {
        List<Item> ItemSearch(string search, int page, int pagesize, out int totalpages);
    }
}