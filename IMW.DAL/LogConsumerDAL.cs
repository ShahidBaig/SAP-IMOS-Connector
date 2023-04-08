namespace IMW.DAL
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Threading;

    public class LogConsumerDAL
    {
        private string logFileName = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{("LogConsumer-" + DateTime.Now.ToString("yyyyMMdd") + ".txt")}";
        private FileStream logFileStream;
        private StreamWriter logStreamWriter;
        private Queue<LogItem> queue = new Queue<LogItem>();
        private static readonly int BUFFER_SIZE = 10;
        private Semaphore fillCount = new Semaphore(0, BUFFER_SIZE);
        private Semaphore emptyCount = new Semaphore(BUFFER_SIZE, BUFFER_SIZE);
        private Mutex bufferMutex = new Mutex();
        private Thread consumerThread;
        private static LogConsumerDAL instance = new LogConsumerDAL();

        private LogConsumerDAL()
        {
            //this.OpenFileStream();
            //this.consumerThread = new Thread(new ThreadStart(this.Consumer));
            //this.consumerThread.Start();
        }

        //private void CloseFileStream()
        //{
        //    if (this.logStreamWriter != null)
        //    {
        //        this.logStreamWriter.Flush();
        //        this.logStreamWriter.Close();
        //        this.logStreamWriter = null;
        //    }
        //    if (this.logFileStream != null)
        //    {
        //        this.logFileStream.Close();
        //        this.logFileStream = null;
        //    }
        //}

        //private void Consumer()
        //{
        //    while (true)
        //    {
        //        this.fillCount.WaitOne();
        //        this.bufferMutex.WaitOne();
        //        LogItem logItem = this.removeItemFromBuffer();
        //        this.bufferMutex.ReleaseMutex();
        //        this.emptyCount.Release();
        //        this.WriteLog(logItem);
        //    }
        //}

        ~LogConsumerDAL()
        {
            //this.CloseFileStream();
        }

        //private void OpenFileStream()
        //{
        //    if (!string.IsNullOrEmpty(this.logFileName))
        //    {
        //        if (ReferenceEquals(this.logFileStream, null))
        //        {
        //            this.logFileStream = new FileStream(this.logFileName, FileMode.Append);
        //        }
        //        if (ReferenceEquals(this.logStreamWriter, null))
        //        {
        //            this.logStreamWriter = new StreamWriter(this.logFileStream);
        //        }
        //    }
        //}

        private LogItem ProduceItem(string logTime, string logModule, string logContent)
        {
            LogItem item1 = new LogItem();
            item1.time = logTime;
            item1.module = logModule;
            item1.content = logContent;
            return item1;
        }

        //private void putItemIntoBuffer(LogItem item)
        //{
        //    this.queue.Enqueue(item);
        //}

        //private LogItem removeItemFromBuffer()
        //{
        //    LogItem item = this.queue.Peek();
        //    this.queue.Dequeue();
        //    return item;
        //}

        public void Write(string content)
        {
            string logTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MethodBase method = new StackTrace().GetFrame(1).GetMethod();
            string str2 = method.ReflectedType.Namespace;
            string name = method.ReflectedType.Name;
            string str4 = method.Name;
            string[] textArray1 = new string[] { str2, ":", name, ".", str4 };



            LogItem item = this.ProduceItem(logTime, string.Concat(textArray1), content);
            string[] textArray2 = new string[] { item.time, "  ", item.module, "  ", item.content };

            using (var s1 = new StreamWriter(new FileStream(this.logFileName, FileMode.Append, FileAccess.Write)))
            {
                s1.WriteLine(string.Concat(textArray2));
            }

            //this.emptyCount.WaitOne();
            //this.bufferMutex.WaitOne();
            //this.putItemIntoBuffer(item);
            //this.bufferMutex.ReleaseMutex();
            //this.fillCount.Release();
        }

        //private void WriteLog(LogItem logItem)
        //{
        //    if (ReferenceEquals(this.logStreamWriter, null))
        //    {
        //        this.OpenFileStream();
        //    }
        //    string[] textArray1 = new string[] { logItem.time, "  ", logItem.module, "  ", logItem.content };
        //    this.logStreamWriter.WriteLine(string.Concat(textArray1));
        //    this.logStreamWriter.Flush();
        //}

        public static LogConsumerDAL Instance
        {
            get => 
                instance;
            private set
            {
            }
        }
    }
}

