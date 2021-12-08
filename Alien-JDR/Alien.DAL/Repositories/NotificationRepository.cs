﻿using Alien.DAL.Entities;
using Alien.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Delegates;
using TableDependency.SqlClient.Base.EventArgs;

namespace Alien.DAL.Repositories
{
    public class NotificationRepository : RepositoryBase<NotificationEntity, int>, INotificationRepository
    {
        private const string CONNECTION_STRING = @"Server=(localdb)\MSSQLLocalDB;
                                                Database=Alien-JDR-DB;
                                                Integrated Security=True;
                                                Connect Timeout=60;";
        private const string TABLE_NAME = "Notifications";

        private readonly SqlTableDependency<NotificationEntity> _sqlTableDependency;

        public event ChangedEventHandler<NotificationEntity> NotificationEvent;

        public NotificationRepository(AlienContext context)
            : base(context)
        {
            _sqlTableDependency = new SqlTableDependency<NotificationEntity>(CONNECTION_STRING, TABLE_NAME);
            _sqlTableDependency.OnChanged += NewNotification;
        }

        private void NewNotification(object sender, RecordChangedEventArgs<NotificationEntity> e)
        {
            NotificationEvent.Invoke(sender, e);
        }
    }
}
