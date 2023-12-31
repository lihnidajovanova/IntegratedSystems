﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TicketShop.Domain
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public int SmtpServerPort { get; set; }
        public bool EnableSsl { get; set; }
        public string EmailDisplayName { get; set; }
        public string SenderName { get; set; }

        public EmailSettings()
        {
        }

        public EmailSettings(string smtpServer, string smtpUserName, string smtpPassword, string smtpServerPort)
        {
            SmtpServer = smtpServer;
            SmtpUserName = smtpUserName;
            SmtpPassword = smtpPassword;
            SmtpServerPort = smtpServerPort;
        }
    }
}
