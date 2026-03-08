using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsR4U.Data
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public decimal Amount { get; set; }

        //Fakturadatum knyts till inckeckningsdatum
        public DateTime InvoiceDate { get; set; }

        //Förfalludatum
        public DateTime DueDate { get; set; }

        public int BookingServiceID { get; set; }
        [ForeignKey(nameof(BookingServiceID))]
        public virtual BookingService BookingService { get; set; }

    }
}