using OlavTiming.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace OlavTiming.Repositories
{
    public class UserTaskRepository : IRepository<UserTask>
    {
        private readonly string path = @".\OlavTiming";
        private readonly string file = $"{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}.xml";

        public UserTaskRepository()
        {
            Create();
        }

        public IList<UserTask> Create(IList<UserTask> itemList)
        {
            var stream = new StringWriter();

            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(nameof(IList<UserTask>));
                foreach (var item in itemList)
                {
                    writer.WriteStartElement(nameof(UserTask));
                    writer.WriteAttributeString(nameof(UserTask.Id), item.Id.ToString());
                    writer.WriteAttributeString(nameof(UserTask.Name), item.Name);
                    foreach (var timeFrame in item.Timeframes)
                    {
                        writer.WriteStartElement(nameof(Timeframe));
                        writer.WriteElementString(nameof(Timeframe.Start) + "time", timeFrame.Start.ToString());
                        writer.WriteElementString(nameof(Timeframe.End) + "time", timeFrame.End.ToString());
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                writer.Flush();
            }

            using (StreamWriter streamWriter = File.CreateText(Path.Combine(path, file)))
            {
                streamWriter.Write(stream);
            }

            return itemList;
        }

        public UserTask Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<UserTask> Get()
        {
            return (Get(file));
        }

        public IList<UserTask> Get(string filename)
        {
            var UserTasksList = new List<UserTask>();
            var fileString = File.ReadAllText(Path.Combine(path, file));

            if (string.IsNullOrWhiteSpace(fileString))
            {
                return UserTasksList;
            }

            using (var stringReader = new StringReader(fileString))
            {
                using (var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings() { IgnoreWhitespace = true }))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement(nameof(IList<UserTask>));
                    do
                    {
                        var readUserTask = new UserTask();

                        readUserTask.Id = int.Parse(xmlReader.GetAttribute(nameof(UserTask.Id)));
                        readUserTask.Name = xmlReader.GetAttribute(nameof(UserTask.Name));

                        xmlReader.ReadStartElement(nameof(UserTask));

                        readUserTask.Timeframes = new List<Timeframe>();

                        do
                        {
                            var readTimeFrame = new Timeframe();
                            xmlReader.ReadStartElement(nameof(Timeframe));
                            xmlReader.ReadStartElement(nameof(Timeframe.Start) + "time");
                            readTimeFrame.Start = DateTime.Parse(xmlReader.ReadContentAsString());
                            xmlReader.ReadEndElement();
                            xmlReader.ReadStartElement(nameof(Timeframe.End) + "time");
                            readTimeFrame.End = DateTime.Parse(xmlReader.ReadContentAsString());
                            xmlReader.ReadEndElement();

                            readUserTask.Timeframes.Add(readTimeFrame);

                        } while (xmlReader.ReadToNextSibling(nameof(Timeframe)));


                        UserTasksList.Add(readUserTask);
                    } while (xmlReader.ReadToNextSibling(nameof(UserTask)));
                }
            }

            return UserTasksList;
        }

        public UserTask Update(UserTask item)
        {
            throw new System.NotImplementedException();
        }

        private void Create()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!File.Exists(Path.Combine(path, file)))
            {
                var createdFile = File.Create(Path.Combine(path, file));
                createdFile.Close();
            }
        }
    }
}
