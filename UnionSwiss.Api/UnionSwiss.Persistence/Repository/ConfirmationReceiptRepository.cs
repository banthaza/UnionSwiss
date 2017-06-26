using System;
using Zapper.Common.Constants;
using Zapper.Domain.Model.Entity;
using Zapper.Domain.Persistence.Context;
using Zapper.Domain.Persistence.Factory;

namespace Zapper.Domain.Persistence.Repository
{
    public class ConfirmationReceiptRepository : BaseRepository<PaymentNotification, IZapperClientContext>
    {

        public ConfirmationReceiptRepository(IDbContextFactory<IZapperClientContext> contextFactory) : base(contextFactory)
        {
            
        }

        public PaymentNotification SaveConfirmationReceipt(PaymentNotification confirmationReceipt)
        {
            var receipt = confirmationReceipt;
            try
            {
                Log.Debug(LoggingConstants.Entering);

                var existingReceipt = Get(x => x.PaymentId == receipt.PaymentId);

                if (existingReceipt == null)
                {
                    confirmationReceipt = Save(confirmationReceipt);
                }
                else
                {
                    confirmationReceipt = existingReceipt;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            finally
            {
                Log.Debug(LoggingConstants.Leaving);
            }
            return confirmationReceipt;
        }
    }
}