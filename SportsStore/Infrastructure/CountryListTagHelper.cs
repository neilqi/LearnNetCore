using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models;

namespace SportsStore.Infrastructure
{
    [HtmlTargetElement("country-lists")]
    public class CountryListTagHelper:TagHelper
    {
        private readonly ICountryService _countryService;

        public string SelectedValue { get; set; }

        public CountryListTagHelper(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";
            output.Content.Clear();

            foreach (var item in _countryService.GetAll())
            {
                var seleted = "";
                if (SelectedValue != null && SelectedValue.Equals(item.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    seleted = " selected=\"selected\"";
                }

                var listItem = $"<option value=\"{item.Code}\"{seleted}>{item.CnName}-{item.EnName}</option>";
                output.Content.AppendHtml(listItem);

            }
        }
    }
}
