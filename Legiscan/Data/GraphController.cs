using Legiscan.Helpers;
using Legiscan.Providers;
using Newtonsoft.Json;
using Syncfusion.Blazor.Diagrams;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using static Legiscan.Data.Models;

namespace Legiscan.Data
{
    public class GraphController
    {
        public class PlotPerson
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public string ParentId { get; set; }
        }
        public class PlotBill
        {
            public string Label { get; set; }
            public string Id { get; set; }
            public string ParentId { get; set; }
            public string BillNumber { get; set; }
            public string Status { get; set; }

            public List<(string, string, string)> Sponsors = new List<(string, string, string)>();
        }
        public class PlotPerson1
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public string[] ParentId { get; set; }
            public string BillNumber { get; set; }
            public string Status { get; set; }
            public string Party { get; set; }
            public int Type { get; set; }
        }
        public async Task<List<PlotPerson>> GetPeople()
        {
            List<Person> People = new List<Person>();
            List<PlotPerson> PeoplePlot = new List<PlotPerson>();
            People = await PeopleProvider.GetPeople();
            People.ForEach(p =>
            {
                PlotPerson plotPerson = new PlotPerson();
                plotPerson.Name = p.name;
                plotPerson.Id = p.people_id.HasValue ? p.people_id.Value.ToString() : "";
                plotPerson.ParentId = p.party_id.HasValue ? p.party_id.Value.ToString() : "";
                PeoplePlot.Add(plotPerson);
            });


            return PeoplePlot;
        }

        public async Task<List<PlotBill>> GetBills()
        {
            List<ApiResultBill> bills = new List<ApiResultBill>();
            List<PlotBill> pBills = new List<PlotBill>();
            string fileName = @"D:\AllBills.txt";
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    bills.Add(JsonConvert.DeserializeObject<ApiResultBill>(line));
                }
            }
            int i = 0;
            bills.ForEach(b =>
            {
                if (i < 25)
                {
                    PlotBill plotBill = new PlotBill();
                    plotBill.Id = b.bill.bill_id;
                    plotBill.Label = b.bill.title;
                    plotBill.BillNumber = b.bill.bill_number;
                    plotBill.Status = b.bill.status;
                    b.bill.sponsors.ForEach(sponsor =>
                    {
                        plotBill.Sponsors.Add((sponsor.people_id, sponsor.name, sponsor.party_id));
                    });
                    pBills.Add(plotBill);
                    i++;
                }

            });
            //List<PlotBill> oneBill = new List<PlotBill>();
            //oneBill.Add(pBills.FirstOrDefault());

            //bills.FirstOrDefault().bill.sponsors.ForEach(sponsor =>
            //                {
            //                    PlotBill bill = new PlotBill();
            //                    bill.Label = sponsor.name;
            //                    bill.Id = sponsor.people_id;
            //                    bill.ParentId = bills.FirstOrDefault().bill.bill_id;
            //                    oneBill.Add(bill);
            //                });
            return pBills;
        }
        public async Task<(bool, List<PlotPerson1>)> GenerateNodes()
        {

            List<PlotPerson1> persons = new List<PlotPerson1>();
            List<(string, string)> people = new List<(string, string)>();
            List<PlotBill> bills = await GetBills();
            persons.Add(new PlotPerson1() { Id = "MIDDLE", ParentId = new string[] { "" }, Name = "MIDDLE", Type = 0 });
            persons.Add(new PlotPerson1() { Id = "HB1", ParentId = new string[] { "MIDDLE" }, Name = "HB1", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "HR1", ParentId = new string[] { "MIDDLE" }, Name = "HR1", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "SB1", ParentId = new string[] { "MIDDLE" }, Name = "SB1", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "SR1", ParentId = new string[] { "MIDDLE" }, Name = "SR1", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "HB2", ParentId = new string[] { "MIDDLE" }, Name = "HB2", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "HR2", ParentId = new string[] { "MIDDLE" }, Name = "HR2", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "SB2", ParentId = new string[] { "MIDDLE" }, Name = "SB2", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "SR2", ParentId = new string[] { "MIDDLE" }, Name = "SR2", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "HB3", ParentId = new string[] { "MIDDLE" }, Name = "HB3", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "HR3", ParentId = new string[] { "MIDDLE" }, Name = "HR3", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "SB3", ParentId = new string[] { "MIDDLE" }, Name = "SB3", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "SR3", ParentId = new string[] { "MIDDLE" }, Name = "SR3", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "HB4", ParentId = new string[] { "MIDDLE" }, Name = "HB4", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "HR4", ParentId = new string[] { "MIDDLE" }, Name = "HR4", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "SB4", ParentId = new string[] { "MIDDLE" }, Name = "SB4", Type = 1 });
            persons.Add(new PlotPerson1() { Id = "SR4", ParentId = new string[] { "MIDDLE" }, Name = "SR4", Type = 1 });
            bills.ForEach(b =>
            {
                string Parent = "MIDDLE";
                if (b.BillNumber.Contains("HB") && b.Status.Contains("1")) { Parent = "HB1"; }
                if (b.BillNumber.Contains("HR") && b.Status.Contains("1")) { Parent = "HR1"; }
                if (b.BillNumber.Contains("SB") && b.Status.Contains("1")) { Parent = "SB1"; }
                if (b.BillNumber.Contains("SR") && b.Status.Contains("1")) { Parent = "SR1"; }
                if (b.BillNumber.Contains("HB") && b.Status.Contains("2")) { Parent = "HB2"; }
                if (b.BillNumber.Contains("HR") && b.Status.Contains("2")) { Parent = "HR2"; }
                if (b.BillNumber.Contains("SB") && b.Status.Contains("2")) { Parent = "SB2"; }
                if (b.BillNumber.Contains("SR") && b.Status.Contains("2")) { Parent = "SR2"; }
                if (b.BillNumber.Contains("HB") && b.Status.Contains("3")) { Parent = "HB3"; }
                if (b.BillNumber.Contains("HR") && b.Status.Contains("3")) { Parent = "HR3"; }
                if (b.BillNumber.Contains("SB") && b.Status.Contains("3")) { Parent = "SB3"; }
                if (b.BillNumber.Contains("SR") && b.Status.Contains("3")) { Parent = "SR3"; }
                if (b.BillNumber.Contains("HB") && b.Status.Contains("4")) { Parent = "HB4"; }
                if (b.BillNumber.Contains("HR") && b.Status.Contains("4")) { Parent = "HR4"; }
                if (b.BillNumber.Contains("SB") && b.Status.Contains("4")) { Parent = "SB4"; }
                if (b.BillNumber.Contains("SR") && b.Status.Contains("4")) { Parent = "SR4"; }
                persons.Add(new PlotPerson1() { Id = b.Id, ParentId = new string[] { Parent }, Name = b.Label, Type = 1 });
                if (String.IsNullOrWhiteSpace(b.BillNumber) )
                {
                    string a = "a";
                }

                b.Sponsors.ForEach(p =>
                {
                    var existing = persons.Where(x => x.Id == p.Item1);
                    if (!existing.Any())
                    {
                        persons.Add(new PlotPerson1() { Name = p.Item2, Id = p.Item1, ParentId = new string[] { b.Id }, Type = 2, Party = p.Item3 });
                    }
                    else
                    {
                        int i = 0;
                        string[] newIds = new string[existing.First().ParentId.Count() + 1];
                        while (i < existing.First().ParentId.Count())
                        {
                            newIds[i] = existing.First().ParentId[i];
                            i++;
                        }
                        newIds[newIds.Length - 1] = b.Id;
                        persons.ElementAt(persons.IndexOf(existing.First())).ParentId = newIds;
                    }
                });
            });
            return (true, persons);
        }
        public async Task<(ObservableCollection<DiagramNode>, ObservableCollection<DiagramConnector>)> ConfigureNodes(List<PlotPerson1> points)
        {
            ObservableCollection<DiagramNode> nodes = new ObservableCollection<DiagramNode>();
            ObservableCollection<DiagramConnector> connectors = new ObservableCollection<DiagramConnector>();
            points.ForEach(p =>
            {
                if (p.Type == 1)
                {
                    int i = 0;
                    while (i < p.ParentId.Length)
                    {
                        connectors.Add(CreateConnector(p.ParentId[i], p.Id, "1"));
                        i++;
                    }
                    DiagramNode node1 = new DiagramNode()
                    {
                        Id = p.Id,
                        Shape = new DiagramShape()
                        {
                            Type = Shapes.Image,
                            Source = "Images/open-book.png",
                            Scale = Stretch.Meet,
                            Align = ImageAlignment.XMinYMin
                        },
                        Tooltip = new NodeTooltip()
                        {
                            Content = p.Name,
                            Position = Syncfusion.Blazor.Popups.Position.BottomCenter,
                            RelativeMode = TooltipRelativeMode.Object,
                        },
                        // Size of the node
                        Width = 15,
                        Height = 15,
                        ZIndex = 6,
                        Style = new NodeShapeStyle()
                        {
                            //Fill = ,
                            StrokeWidth = 0,
                            
                        },
                        Constraints =  (NodeConstraints.Default | NodeConstraints.Tooltip) & ~(NodeConstraints.Select | NodeConstraints.Shadow),
                        Annotations = new ObservableCollection<DiagramNodeAnnotation>()
                        {
                        //new DiagramNodeAnnotation()
                        //{
                        //    Content = p.Name,
                        //    Style = new AnnotationStyle()
                        //    {
                        //        FontSize = 2
                        //    }
                        //}
                    },
                    };
                    nodes.Add(node1);
                }
                else if (p.Type == 2)
                {
                    int i = 0;
                    while (i < p.ParentId.Length)
                    {
                        connectors.Add(CreateConnector(p.Id, p.ParentId[i], p.Party));
                        i++;
                    }
                    DiagramNode node1 = new DiagramNode()
                    {
                        Id = p.Id,
                        Shape = new DiagramShape()
                        {
                            Type = Shapes.Image,
                            Source = "Images/people.png",
                            Scale = Stretch.Meet,
                            Align = ImageAlignment.XMinYMin,
                        },
                        Tooltip = new NodeTooltip()
                        {
                            Content = p.Name,
                            Position = Syncfusion.Blazor.Popups.Position.BottomCenter,
                            RelativeMode = TooltipRelativeMode.Object,
                        },
                        // Size of the node
                        Width = 2 + p.ParentId.Length,
                        Height = 2 + p.ParentId.Length,
                        ZIndex = 6,
                        Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) & ~(NodeConstraints.Select | NodeConstraints.Shadow),
                        Style = new NodeShapeStyle()
                        {
                            //Fill = p.Party == "1" ? "#6BA5D7" : "#FF0000",
                            StrokeWidth = 0,
                        },
                        // Custom Shadow of the node
                        Annotations = new ObservableCollection<DiagramNodeAnnotation>()
                    {
                        //new DiagramNodeAnnotation()
                        //{
                        //    Content = p.Name,
                        //    Style = new AnnotationStyle()
                        //    {
                        //        FontSize = 2,
                        //        Color = p.Party == "1" ? "#6BA5D7" : "#FF0000",
                        //    }
                        //}
                    },
                    };
                    nodes.Add(node1);
                }
                else
                {
                    DiagramNode node1 = new DiagramNode()
                    {
                        Id = p.Id,

                        // Size of the node
                        Width = 10,
                        Height = 10,
                        ZIndex = 6,
                        Style = new NodeShapeStyle()
                        {
                            //Fill = "#6BA5D7",
                            StrokeWidth = 0,
                        },
                        Tooltip = new NodeTooltip()
                        {
                            Content = p.Name,
                            Position = Syncfusion.Blazor.Popups.Position.BottomCenter,
                            RelativeMode = TooltipRelativeMode.Object,
                        },
                        Constraints = (NodeConstraints.Default | NodeConstraints.Tooltip) & ~(NodeConstraints.Select | NodeConstraints.Shadow),
                        Annotations = new ObservableCollection<DiagramNodeAnnotation>()
                    {
                        //new DiagramNodeAnnotation()
                        //{
                        //    Content = p.Name,
                        //    Style = new AnnotationStyle()
                        //    {
                        //        FontSize = 2
                        //    }
                        //}
                    },
                    };
                    nodes.Add(node1);
                }
            });
            return (nodes, connectors);
        }

        public DiagramConnector CreateConnector(string source, string target, string party)
        {
            DiagramConnector diagramConnector = new DiagramConnector()
            {
                //Source node id of the connector.
                Id = source + "-" + target,
                SourceID = source,
                ZIndex = 2,
                //Target node id of the connector.
                TargetID = target,
                Constraints = ConnectorConstraints.Default & ~ConnectorConstraints.Select,
                TargetDecorator = new ConnectorTargetDecorator()
                {
                    Shape = DecoratorShapes.None,
                },
                Style = new ConnectorShapeStyle()
                {
                    StrokeColor = party == "1" ? "#37909A" : "#FF0000",
                    StrokeWidth = .25
                },
                Type = Segments.Straight,
            };
            return diagramConnector;
        }
        public async Task<List<ApiResultBill>> FetchBills()
        {
            List<string> ids = await MasterListProvider.GetMasterList();
            List<WebProxy> proxies = ProxyHelper.GenerateProxyList();
            List<ApiResultBill> bills = new List<ApiResultBill>();
            List<string> billStrings = new List<string>();
            int i = 0;
            int p = 0;
            while (i < ids.Count)
            {
                if (p > proxies.Count - 1)
                {
                    p = 0;
                }
                ApiResultBill bill = await MasterListProvider.GetThisBill(ids[i], proxies[p]);
                billStrings.Add(JsonConvert.SerializeObject(bill));
                bills.Add(bill);
                Console.WriteLine(i + "||" + ids.Count);
                p++;
                i++;
            }
            File.WriteAllLines(@"D:/AllBills.txt", billStrings);
            return bills;
        }
    }
}
