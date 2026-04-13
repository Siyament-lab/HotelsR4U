
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsR4U.Entities
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public decimal Amount { get; set; }

        //Fakturadatum knyts till inckeckningsdatum
        public DateTime InvoiceDate { get; set; }

        //Förfalludatum
        public DateTime DueDate { get; set; }

        public int BookingID { get; set; }
        [ForeignKey(nameof(BookingID))]
        public Booking Booking { get; set; } = null!;

    }
}