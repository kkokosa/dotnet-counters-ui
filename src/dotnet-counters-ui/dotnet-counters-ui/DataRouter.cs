using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;

namespace DotnetCountersUi
{
    public class DataRouter : IDataRouter
    {
        private bool _isStarted;
        private Thread _collectRoutine;

        public DataRouter()
        {
            _isStarted = false;
        }

        public void Start(int pid)
        {
            if (!_isStarted)
            {
                _collectRoutine = new Thread(CollectRoutine);
                _collectRoutine.IsBackground = true;
                _collectRoutine.Start(pid);
            }
        }

        public GraphData Register(string graphId, Action<double> action)
        {
            var series = _series.FirstOrDefault(x => x.Id == graphId);
            if (series == null)
            {
                throw new ArgumentException();
            }

            if (!_registrations.ContainsKey(graphId))
            {
                _registrations[graphId] = new List<Action<double>>();
            }

            _registrations[graphId].Add(action);

            return new GraphData
            {
                Name = series.Name
            };
        }

        public void Unregister(string graphId, Action<double> action)
        {
            _registrations[graphId].Remove(action);
        }

        private void CollectRoutine(object data)
        {
            var providers = new List<EventPipeProvider>()
            {
                new EventPipeProvider("System.Runtime",
                    EventLevel.Informational,
                    (long)ClrTraceEventParser.Keywords.None,
                    new Dictionary<string, string>()
                    {
                        { "EventCounterIntervalSec", "1" }
                    })
            };

            var pid = (int)data;
            var client = new DiagnosticsClient(pid);
            using (var session = client.StartEventPipeSession(providers, false))
            {
                var source = new EventPipeEventSource(session.EventStream);
                source.Dynamic.All += DynamicOnAll;
                source.Process();
            }
        }

        private void DynamicOnAll(TraceEvent obj)
        {
            if (obj.EventName.Equals("EventCounters"))
            {
                var payloadVal = (IDictionary<string, object>)(obj.PayloadValue(0));
                var payloadFields = (IDictionary<string, object>)(payloadVal["Payload"]);
                var name = payloadFields["Name"].ToString();
                var metadata = _series.FirstOrDefault(x => x.Id == name);
                if (metadata != null)
                {
                    foreach (var registration in _registrations)
                    {
                        if (registration.Key == name)
                        {
                            var value = metadata.Selector(payloadFields);
                            foreach (var listener in registration.Value)
                            {
                                listener(value);
                            }
                        }
                    }
                }
            }
        }

        private readonly Dictionary<string, List<Action<double>>> _registrations = new();

        private List<SeriesMetadata> _series = new List<SeriesMetadata>()
        {
            new SeriesMetadata()
            {
                Id = "alloc-rate",
                Name = "Allocation rate",
                Selector = payload => double.Parse(payload["Increment"].ToString())
            },
            new SeriesMetadata()
            {
                Id = "cpu-usage",
                Name = "CPU usage (%)",
                Selector = payload => double.Parse(payload["Mean"].ToString())
            }
        };
    }
}
