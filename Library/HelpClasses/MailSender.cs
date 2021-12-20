﻿using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace Library.HelpClasses
{
    public class MailSender 
    {



        /// <summary>
        /// Krypterar och struktuerar upp för mailutskick
        /// </summary>
        /// <param name="email"></param>
        /// <param name="query"></param>
        /// <param name="subject"></param>
        public static void SendEmail(string email, string query, string? subject, MySettings mySettings)
        {

            if (subject == null)
            {
                subject = "Posting";
            }
            var sw = new StringBuilder();
            sw.Append(DateTime.Now.ToString() + "/()/");
            sw.Append(subject);

            string encrypt = AesCryption.Encrypt(query, mySettings.Secret);

            var mailAddress = mySettings.Email;
            var password = mySettings.Password;
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(mySettings.Username, mailAddress));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = sw.ToString();
            message.Body = new TextPart("plain")
            {
                Text = $"{encrypt}XYXY/(/(XYXY7"
            };
            SmtpClient client = new SmtpClient();
            try
            {
                client.CheckCertificateRevocation = false;
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate(mailAddress, password);
                client.Send(message);
                //Console.WriteLine("Email Sent!");
                //dh.SendSqlQuery(r.ToSQL());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        public static void SendDeniedRequest(MySettings mySettings, string ToEmail)
        {
            string subject = "friendRequestDenied";

            var sw = new StringBuilder();
            sw.Append(DateTime.Now.ToString() + "/()/");
            sw.Append(subject);


            //Vill endast skicka username och Email.

            string username = mySettings.Username;
            string mailAddress = mySettings.Email;


            var crypt = new StringBuilder();

            crypt.Append(username);
            crypt.Append("/()/");
            crypt.Append(mailAddress);

            string encrypt = AesCryption.Encrypt(crypt.ToString(), mySettings.Secret);

            var body = encrypt;

            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(username, mailAddress));
            message.To.Add(MailboxAddress.Parse(ToEmail));
            message.Subject = sw.ToString();
            message.Body = new TextPart("plain")
            {
                Text = $"{body}XYXY/(/(XYXY7"
            };


            SmtpClient client = new SmtpClient();
            try
            {
                client.CheckCertificateRevocation = false;
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate(mailAddress, mySettings.Password); ///avkryptera lösen från db:n
                client.Send(message);
                //Console.WriteLine("Email Sent!");
                //dh.SendSqlQuery(r.ToSQL());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }


        }

        public static void SendAcceptedRequest(MySettings mySettings, string ToEmail, string myDiscussion, string myPost, string myFriend)
        {
            ///Här slutar vi onsdag


            string subject = "friendRequestAccepted";

            var sw = new StringBuilder();
            sw.Append(DateTime.Now.ToString() + "/()/");
            sw.Append(subject);

            //Vill endast skicka username och Email.

            string username = mySettings.Username;
            string mailAddress = mySettings.Email;

            var password = mySettings.Password;

            var crypt = new StringBuilder();

            crypt.Append(mailAddress);
            crypt.Append("/()/");
            crypt.Append(myDiscussion);
            crypt.Append("/()/");
            crypt.Append(myPost);
            crypt.Append("/()/");
            crypt.Append(myFriend);

            string encrypt = AesCryption.Encrypt(crypt.ToString(), mySettings.Secret);

            var body = encrypt;

            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(username, mailAddress));
            message.To.Add(MailboxAddress.Parse(ToEmail));
            message.Subject = sw.ToString();
            message.Body = new TextPart("plain")
            {
                Text = $"{body}XYXY/(/(XYXY7"
            };


            SmtpClient client = new SmtpClient();
            try
            {
                client.CheckCertificateRevocation = false;
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate(mailAddress, password);
                client.Send(message);
                //Console.WriteLine("Email Sent!");
                //dh.SendSqlQuery(r.ToSQL());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        public static void SendFriendRequest(MySettings mySettings, string ToEmail)
        {
            string subject = "friendRequest";
            
            var sw = new StringBuilder();
            sw.Append(DateTime.Now.ToString() + "/()/");
            sw.Append(subject);


            //Vill endast skicka username och Email.

            string username = mySettings.Username;
            string mailAddress = mySettings.Email;

            var password = mySettings.Password;

            var crypt = new StringBuilder();

            crypt.Append(username);
            crypt.Append("/()/");
            crypt.Append(mailAddress);

            string encrypt = AesCryption.Encrypt(crypt.ToString(), mySettings.Secret);

            var body = encrypt;

            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(username, mailAddress));
            message.To.Add(MailboxAddress.Parse(ToEmail));
            message.Subject = sw.ToString();
            message.Body = new TextPart("plain")
            {
                Text = $"{body}XYXY/(/(XYXY7"
            };


            SmtpClient client = new SmtpClient();
            try
            {
                client.CheckCertificateRevocation = false;
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate(mailAddress, password);
                client.Send(message);
                //Console.WriteLine("Email Sent!");
                //dh.SendSqlQuery(r.ToSQL());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
