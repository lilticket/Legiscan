using Legiscan.Helpers;
using Legiscan.Providers;
using Newtonsoft.Json;
using Syncfusion.Blazor.Diagrams;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using static Legiscan.Data.GraphController;
using static Legiscan.Data.Models;

namespace Legiscan.Data
{
    public class PersonController
    {
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

        public async Task<(bool, List<PlotPerson1>)> GenerateNodes(string personId)
        {

            List<PlotPerson1> persons = new List<PlotPerson1>();
            List<PlotBill> bills = await GetBills();
            bills.ForEach(b =>
            {
                persons.Add(new PlotPerson1() { Id = b.Id, ParentId = new string[] { personId }, Name = "Bill", Type = 1 });
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
            if (!String.IsNullOrEmpty(personId))
            {
                List<PlotPerson1> filteredPeeps = new List<PlotPerson1>();
                PlotPerson1 thisPerson = persons.Find(p => p.Id == personId);
                if (thisPerson != null)
                {
                    int i = 0;
                    while (i < thisPerson.ParentId.Count())
                    {
                        PlotPerson1 aParent = persons.Find(p => p.Id.Equals(thisPerson.ParentId[i]));
                        filteredPeeps.Add(aParent);
                        i++;
                    }
                    List<PlotPerson1> peepsToAdd = new List<PlotPerson1>();
                    filteredPeeps.ForEach(f =>
                    {
                        PlotBill bill = bills.Find(b => b.Id == f.Id);
                        bill.Sponsors.ForEach(s =>
                        {
                            if (s.Item1 != thisPerson.Id)
                            {
                                peepsToAdd.Add(new PlotPerson1() { Name = s.Item2, Id = s.Item1 + f.Id, ParentId = new string[] { f.Id }, Type = 2, Party = s.Item3 });
                            }
                        });
                    });
                    filteredPeeps.AddRange(peepsToAdd);
                    thisPerson.ParentId = new string[] { "" };
                    thisPerson.Type = 0;
                    filteredPeeps.Add(thisPerson);
                    persons = filteredPeeps;
                }
                else
                {
                    return (false, new List<PlotPerson1>());
                }
            }
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
                        connectors.Add(CreateConnector(p.Id, p.ParentId[i], "1"));
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
                        // Size of the node
                        Width = 15,
                        Height = 15,
                        Style = new NodeShapeStyle()
                        {
                            //Fill = ,
                            StrokeWidth = 0,
                        },
                        // Custom Shadow of the node
                        Annotations = new ObservableCollection<DiagramNodeAnnotation>()
                        {
                        new DiagramNodeAnnotation()
                        {
                            Content = p.Name,
                            Style = new AnnotationStyle()
                            {
                                FontSize = 2
                            }
                        }
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
                        // Size of the node
                        Width = 2 + points.Where(x => x.Name == p.Name).Count(),
                        Height = 2 + points.Where(x => x.Name == p.Name).Count(),

                        Style = new NodeShapeStyle()
                        {
                            //Fill = p.Party == "1" ? "#6BA5D7" : "#FF0000",
                            StrokeWidth = 0,
                        },
                        // Custom Shadow of the node
                        Annotations = new ObservableCollection<DiagramNodeAnnotation>()
                    {
                        new DiagramNodeAnnotation()
                        {
                            Content = p.Name,
                            Style = new AnnotationStyle()
                            {
                                FontSize = 2,
                                Color = p.Party == "1" ? "#6BA5D7" : "#FF0000",
                            }
                        }
                    },
                    };
                    nodes.Add(node1);
                }
                else
                {
                    DiagramNode node1 = new DiagramNode()
                    {
                        Id = p.Id,
                        Shape = new DiagramShape()
                        {
                            Type = Shapes.Image,
                            Source = "Images/people.png",
                            Scale = Stretch.Meet,
                            Align = ImageAlignment.XMinYMin
                        },
                        // Size of the node
                        Width = 10,
                        Height = 10,
                        Style = new NodeShapeStyle()
                        {
                            Fill = "#6BA5D7",
                            StrokeWidth = 0,
                        },
                        // Custom Shadow of the node
                        Annotations = new ObservableCollection<DiagramNodeAnnotation>()
                    {
                        new DiagramNodeAnnotation()
                        {
                            Content = p.Name,
                            Style = new AnnotationStyle()
                            {
                                FontSize = 2
                            }
                        }
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
                //Target node id of the connector.
                TargetID = target,
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
    }
}
