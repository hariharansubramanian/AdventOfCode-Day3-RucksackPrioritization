Console.WriteLine("Scanning rucksacks...");

var priorityDict = new Dictionary<char, int>();
for (var c = 'a'; c <= 'z'; c++) priorityDict.Add(c, c - 'a' + 1); // 1-26
for (var c = 'A'; c <= 'Z'; c++) priorityDict.Add(c, c - 'A' + 27); // 27-52

var inventories = File.ReadLines("inventory_list.txt").ToList();

var totalPriorityValue = 0;
const int groupCount = 3;

inventories
    .Select((inv, idx) => new {inv, idx})
    .GroupBy(g => g.idx / groupCount, i => i.inv)
    .ToList()
    .ForEach(g =>
    {
        var groupInventoryList = g.ToList();
        var commonInGroup = groupInventoryList.Aggregate((s1, s2) => new string(s1.Intersect(s2).ToArray()));
        var commonPrioritySum = commonInGroup.Sum(c => priorityDict[c]);
        totalPriorityValue += commonPrioritySum;
    });

Console.WriteLine($"Common rucksack items have a total priority of {totalPriorityValue}");