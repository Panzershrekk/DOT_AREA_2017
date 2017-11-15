using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Properties
{
    public class Properties
    {
        public Dictionary<string, string> Data { get; set; }
        
        public Properties(string propertiesPath)
        {
            Data = new Dictionary<string, string>();
            try
            {
                foreach (var row in File.ReadAllLines(propertiesPath))
                    Data.Add(row.Split('=')[0], string.Join("=",row.Split('=').Skip(1).ToArray()));
            }
            catch (Exception e)
            {
                throw new PropertiesException($"{e.Message}");
                throw new PropertiesException($"Invalid read of properties file \"{propertiesPath}\"");
            }
        }

        public string GetValue(string index)
        {
            try
            {
                return Data[index];
            }
            catch (Exception e)
            {
                throw new PropertiesException("Invalid data has been try to be reached");
            }
        }
    }
}