using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AMTDll.Models;
using Newtonsoft.Json;

namespace AMTDll
{
    public class Repository<T> : IRepository<T>
    {
        private string DATA_DIRECTORY = "./data";//Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private const string VEHICLE_DATA_PATH = "vehicles.dat";
        private const string SERVICES_DATA_PATH = "services.dat";
        private const string SERVICE_PROVIDERS_DATA_PATH = "service-providers.dat";
        private Dictionary<Type, string> _dataPaths = new Dictionary<Type, string>();
        private List<T> _itemList;

        public Repository()
        {
            _dataPaths.Add(typeof(VehicleModel), Path.Combine(DATA_DIRECTORY, VEHICLE_DATA_PATH));
            _dataPaths.Add(typeof(ServiceModel), Path.Combine(DATA_DIRECTORY, SERVICES_DATA_PATH));
            _dataPaths.Add(typeof(ServiceProviderModel), Path.Combine(DATA_DIRECTORY, SERVICE_PROVIDERS_DATA_PATH));

            _itemList = new List<T>();
            try
            {
                if (!Directory.Exists(DATA_DIRECTORY))
                {
                    Directory.CreateDirectory(DATA_DIRECTORY);
                    return;
                }
                using (var sr = new StreamReader(_dataPaths[typeof(T)]))
                {
                    var lst = JsonConvert.DeserializeObject<List<T>>(sr.ReadToEnd());
                    _itemList = lst == null ? _itemList : lst;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public bool Create(T newObject)
        {
            try
            {
                if (_itemList.Any(item => item.Equals(newObject))) return true;
                _itemList.Add(newObject);
                if (!Directory.Exists(DATA_DIRECTORY)) Directory.CreateDirectory(DATA_DIRECTORY);
                WriteItemListToFile();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Update(T newObject)
        {
            try
            {
                if (!_itemList.Any(item => item.Equals(newObject))) return false;
                _itemList.Remove(newObject);
                _itemList.Add(newObject);
                WriteItemListToFile();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<T> Read()
        {
            return _itemList;
        }

        public bool Remove(T obj)
        {
            try
            {
                _itemList.Remove(obj);
                WriteItemListToFile();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }      
        }

        private void WriteItemListToFile()
        {
            try
            {
                using (var sw = File.CreateText(_dataPaths[typeof(T)]))                 
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(sw, _itemList.ToArray());                
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
