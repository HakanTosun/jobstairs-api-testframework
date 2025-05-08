using TechTalk.SpecFlow;
using RestSharp;

namespace JobStairsSpecflowTest.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void VorSzenario()
        {
            Console.WriteLine($"\n🚀 START SZENARIO: {_scenarioContext.ScenarioInfo.Title}");
        }

        [AfterScenario]
        public void NachSzenario()
        {
            Console.WriteLine($"✅ ENDE SZENARIO: {_scenarioContext.ScenarioInfo.Title}");

            if (_scenarioContext.TestError != null)
            {
                Console.WriteLine("❌ Test fehlgeschlagen!");
                Console.WriteLine("🔍 Fehler: " + _scenarioContext.TestError.Message);

                if (_scenarioContext.ContainsKey("response"))
                {
                    var response = _scenarioContext["response"] as RestResponse;
                    Console.WriteLine("📩 Antwortinhalt:\n" + response?.Content);
                }
            }
        }
    }
}