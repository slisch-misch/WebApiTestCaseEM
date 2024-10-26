using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using WebApplicationTestCaseEM.Database.Entity;
using WebApplicationTestCaseEM.Database.Models;

namespace WebApplicationTestCaseEM.Database;

public class DatabaseContext
{
    private readonly List<District> _districts = [];
    private readonly List<Order> _orders = [];

    private readonly string _pathDistricts = Path.Combine(
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
        throw new InvalidOperationException(), "Database/Districts.csv");

    private readonly string _pathOrders = Path.Combine(
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
        throw new InvalidOperationException(), "Database/Orders.csv");

    public DatabaseContext()
    {
        LoadDistricts();
        LoadOrders();
    }

    public int AddDistrict(string name)
    {
        var id = _districts.Count + 1;
        var district = new District(id, name);
        _districts.Add(district);

        WriteRecordToFile(_pathDistricts, district);

        return id;
    }

    public District[] GetDistricts()
    {
        return _districts.ToArray();
    }

    public Order[] GetOrders(int? districtId, DateTime? deliveryStartFrom = null, DateTime? deliveryStartTo = null)
    {
        var orders = _orders.ToArray();
        
        if(districtId.HasValue)
            orders = orders.Where(o => o.DistrictId == districtId.Value).ToArray();
        
        if (deliveryStartFrom.HasValue)
            orders = orders.Where(o => o.DeliveryTime >= deliveryStartFrom.Value).ToArray();
        
        if (deliveryStartTo.HasValue)
            orders = orders.Where(o => o.DeliveryTime <= deliveryStartTo.Value).ToArray();
        
        return orders.ToArray();
    }

    private void LoadDistricts()
    {
        var records = ReadFromFile<DistrictDto>(_pathDistricts);
        _districts.AddRange(records.Select(c => new District(c.Id, c.Name)));
    }

    private void LoadOrders()
    {
        if (!File.Exists(_pathOrders))
            GenerateOrdersToFile();

        var records = ReadFromFile<OrderDto>(_pathOrders);
        _orders.AddRange(records.Select(c => new Order(c.Id, c.Weight, DateTime.Parse(c.DeliveryTime), c.DistrictId)));
    }

    private static T[] ReadFromFile<T>(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csv.GetRecords<T>().ToArray();
    }

    private void GenerateOrdersToFile()
    {
        var orders = new List<OrderDto>();
        using (var _ = File.Create(_pathOrders))
        {
            for (var i = 1; i < 101; i++)
            {
                var weight = Random.Shared.Next(1,3000);
                var deliveryTime = DateTime.Now.AddDays(Random.Shared.Next(-5, 5))
                    .AddSeconds(Random.Shared.Next(-86400, 86400)).ToString("yyyy-MM-dd HH:mm:ss");
                var districtId = _districts[Random.Shared.Next(0, _districts.Count)].Id;
                var order = new OrderDto()
                {
                    Id = i,
                    Weight = weight,
                    DeliveryTime = deliveryTime,
                    DistrictId = districtId,
                };
                orders.Add(order);
            }
        }


        WriteRecordsToFile(_pathOrders, orders.ToArray());
    }

    private static void WriteRecordToFile(string path, object record)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };

        using var stream = File.Open(path, FileMode.Append);
        using var writer = new StreamWriter(stream);
        using var csv = new CsvWriter(writer, config);

        csv.NextRecord();
        csv.WriteRecord(record);
    }

    private static void WriteRecordsToFile(string path, OrderDto[] records)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        };

        using var stream = File.Open(path, FileMode.Append);
        using var writer = new StreamWriter(stream);
        using var csv = new CsvWriter(writer, config);

        csv.NextRecord();
        csv.WriteRecords(records);
    }
}