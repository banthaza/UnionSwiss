using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zapper.Common;
using Zapper.Domain.Model.Entity;
using Zapper.Domain.Persistence.Context;
using Zapper.Domain.Persistence.Factory;
using Zapper.Domain.Repositories;

namespace Zapper.Domain.Persistence.Repository
{
    public class MerchantSiteRepositrory : BaseRepository<IPointOfSaleAdminContext>, IMerchantSiteRepositrory
    {
        public MerchantSiteRepositrory(IDbContextFactory<IPointOfSaleAdminContext> dbContextFactory) : base(dbContextFactory)
        {
        }

        public MerchantSite SaveMerchantSite(MerchantSite merchantSite)
        {

            try
            {
                Guard.ArgumentNotNull(merchantSite, "merchantSite");
                var existingMerchantSite = Find<MerchantSite>(
                    x =>
                        x.MerchantId == merchantSite.MerchantId &&
                        merchantSite.MerchantSiteId == merchantSite.MerchantId).FirstOrDefault();

                if (existingMerchantSite == null)
                {
                    merchantSite = Create<MerchantSite>(merchantSite);
                }
                else
                {
                    existingMerchantSite.ResturantName = merchantSite.ResturantName;
                    existingMerchantSite.Country = merchantSite.Country;
                    existingMerchantSite.Currency = merchantSite.Currency;
                    existingMerchantSite.Key = merchantSite.Key;
                    existingMerchantSite.Secret = merchantSite.Secret;
                    existingMerchantSite.TaskId = merchantSite.TaskId;

                    merchantSite = UpdateFull(existingMerchantSite);
                }

            }
            catch (Exception ex)
            {
                var message = string.Format("Error saving merchant site  {0}: {1}", merchantSite.MerchantId,merchantSite.ResturantName  , ex.Message);
                Log.Error(message, ex);
            }
            return merchantSite;
        }
    }
}
