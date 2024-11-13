using BookingTour.Model;

namespace BookingTour.Business.Service.IService
{
    public interface IEmailService
    {
        void SendEmail(Message message);

    }
}
