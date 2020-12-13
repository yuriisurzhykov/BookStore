using BookStore.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BookStore.Utils.Payment
{
    public class EmailPaymentSender : IPaymentSender
    {
        public string TrySendReceipt(EmailContext context, bool isModelValid)
        {
            try
            {
                if (isModelValid)
                {
                    var fromAddress = new MailAddress("bookstoretest12345@gmail.com", "Book Store");
                    var toAddress = new MailAddress(context.Receiver, context.Name);
                    const string fromPassword = "bookstore@20@20";
                    string subject = "BookStore | Receipt";
                    string body = new EmailBodyBuilder()
                        .BuildHead(context.Name)
                        .BuildCart(context.Cart)
                        .BuildShippingInfo(context.Address)
                        .BuildFooter()
                        .Build();

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                        Timeout = 20000
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                    return "E-mail has been sent successfully!";
                }
                return "Input values are not correct!";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return "Error occured while sending e-mail!";
            }
        }

        private class EmailBodyBuilder
        {
            private StringBuilder StringBuilder;
            public EmailBodyBuilder()
            {
                StringBuilder = new StringBuilder();
            }

            public EmailBodyBuilder BuildHead(string name)
            {
                StringBuilder
                    .AppendFormat("Уважаемый, {0}!\n", name)
                    .AppendLine("Ваш заказ принят. Детали заказа")
                    .AppendLine("-------------------------------");
                return this;
            }

            public EmailBodyBuilder BuildCart(Cart cart)
            {
                StringBuilder
                    .AppendLine("Сведения о товарах")
                    .AppendLine("-------------------------------");
                foreach (var line in cart.lineCollection)
                {
                    StringBuilder
                        .AppendFormat("Книга: {0}\t Стоимость: {1}\t Общая сумма: {2}\n", line.Book.Name, line.Book.Cost, line.Book.Cost * line.Quantity);
                }
                StringBuilder
                    .AppendFormat("Общая сумма заказа: {0}\n", cart.ComputeSum())
                    .AppendLine("-------------------------------");
                return this;
            }
            public EmailBodyBuilder BuildShippingInfo(Address address)
            {
                StringBuilder
                    .AppendLine("")
                    .AppendLine("Адрес доставки книг")
                    .AppendLine("-------------------------------")
                    .AppendFormat("Город: {0}\t Улица: {1}\t Дом: {2}/{3}\n", address.City, address.Street, address.Home, address.Flat);
                return this;
            }

            public EmailBodyBuilder BuildFooter()
            {
                StringBuilder
                    .AppendLine("")
                    .AppendLine("Усли у вас возникли вопросы по заказу, пишите в Telegram: @yuriy_su")
                    .AppendFormat("BookStore - {0}", DateTime.Now.ToShortDateString());
                return this;
            }

            public string Build()
            {
                return StringBuilder.ToString();
            }
        }
    }
}