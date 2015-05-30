using System;
using System.Collections.Generic;
using StudyDirectionDbWebApp.Models;
using System.Timers;
using Microsoft.Ajax.Utilities;

namespace StudyDirectionDbWebApp.Utility
{
    public static class DatabaseLogger
    {
        readonly static StudyDirectionDbEntities1 Db = new StudyDirectionDbEntities1();
        readonly static List<Log> LogBuffer = new List<Log>();
        readonly static Timer WriteTimer = new Timer();

        static DatabaseLogger()
        {
            WriteTimer.Interval = 5000;
            WriteTimer.Elapsed += SaveChanges;
            WriteTimer.Enabled = true;
            WriteTimer.Start();
        }
        public static void Log(string message)
        {
            if(message.IsNullOrWhiteSpace())
                return;

            var record = new Log() {Message = message};
            AddToBufferConcurrently(record);
        }

        private static void AddToBufferConcurrently(Log record)
        {
            lock (LogBuffer)
            {
                LogBuffer.Add(record);
            }
        }

        private static void SaveChanges(object sender, EventArgs e)
        {
            if (BufferIsEmpty())
                return;

            AddToDbConcurrently();
        }
        private static bool BufferIsEmpty()
        {
            return LogBuffer.Count == 0;
        }

        private static void AddToDbConcurrently()
        {
            lock (Db)
            {
                AddToDbAndClearBufferConcurrently();
                Db.SaveChanges();
            }
        }

        private static void AddToDbAndClearBufferConcurrently()
        {
            lock (LogBuffer)
            {
                Db.Logs.AddRange(LogBuffer);
                LogBuffer.Clear();
            }
        }


    }
}
