using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Outsourced.Models
{
    public class Sendler
    {
        public string Send(string mailTo, string message, string header)
        {
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential("vanya.pupkin.2021@internet.ru", "QWEasd123");

            client.Host = "smtp.mail.ru";

            client.Port = 587;

            client.EnableSsl = true;

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("vanya.pupkin.2021@internet.ru");

            mail.To.Add(mailTo);

            mail.Subject = header;

            mail.Body = message;
            try
            {
                client.Send(mail);
                return "Успешно!";
            }
            catch (SmtpException)
            {
                return
                    "К сожалению, не получится отправить сообщение с localhost, данная функция будет работать только при размещении на хостинге.";
            }
            
        }

        public string GeneratePassword()
        {
            int[] arr = new int[12]; 
            Random rnd = new Random();
            string Password = "";

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(33, 125);
                Password += (char)arr[i];
            }
            return Password;
        }
    }
}
