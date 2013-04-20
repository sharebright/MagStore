using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MagStore.Web.ShopHelpers;
using RavenDbMembership.Entities.Enums;

namespace MagStore.Web.Models.Promotion
{
    public class CreatePromotionViewModel
    {
        private readonly IPromotionHelper helper;

        public CreatePromotionViewModel(IPromotionHelper helper)
        {
            this.helper = helper;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Exclusivity { get; set; }
        public IEnumerable<RavenDbMembership.Entities.Promotion> Restrictions
        {
            get
            {
                return helper.ExistingPromotions;
            }
        }

        public IEnumerable<KeyValuePair<string, string>> ExclusivityList
        {
            get
            {
                return new[]
                    {
                        new KeyValuePair<string, string>  ("Inclusive", "Can be used with other promotions"), 
                        new KeyValuePair<string, string> ("Exclusive","Cannot be used with other promotions")
                    };
            }
        }

        public IList<SelectListItem> DiscountTypeList
        {
            get
            {
                IList<KeyValuePair<string, string>> combined = new List<KeyValuePair<string, string>>();

                combined.Add(new KeyValuePair<string, string>("-1", "Make a selection"));

                foreach (var enumVal in Enum.GetNames(typeof(DiscountType)))
                {
                    combined.Add(new KeyValuePair<string, string>(enumVal, enumVal));
                }

                return combined.Select(keyValuePair => new SelectListItem
                    {
                        Selected = keyValuePair.Key == "-1",
                        Text = keyValuePair.Value,
                        Value = keyValuePair.Key
                    }).ToList();
            }
        }
    }
}