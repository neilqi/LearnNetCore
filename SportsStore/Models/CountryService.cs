using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class CountryService : ICountryService
    {
        private static IEnumerable<CountryModel> CountryList = new List<CountryModel>()
        {
            new CountryModel() { Id=1,EnName="China",CnName="中国",Code="China"},
            new CountryModel() { Id=2,EnName="Japan",CnName="日本",Code="Japan"},
            new CountryModel() { Id=3,EnName="America",CnName="美国",Code="America"},
        };

        public IEnumerable<CountryModel> GetAll()
        {
            return CountryList;
        }
    }
}
