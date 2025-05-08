using System.Net;
using System.Text.Json;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using JobStairsSpecflowTest.Models;
using JobStairsSpecflowTest.Helpers;

namespace JobStairsSpecflowTest.Steps
{
    [Binding]
    public class ApplySteps
    {
        private readonly ScenarioContext _scenarioContext;
        private Bewerber? _requestData = null;
        private RestResponse? _response = null;

        public ApplySteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("gültige Bewerberdaten sind vorbereitet")]
        public void GegebeneGueltigeBewerberdaten()
        {
            _requestData = TestdatenGenerator.ErzeugeBewerber();
        }

        [Given("ein Bewerber ohne Namen")]
        public void GegebenerBewerberOhneName()
        {
            _requestData = new Bewerber
            {
                Name = "",
                Email = "negativ@example.com",
                Position = "Tester"
            };
        }

        [When("die Bewerbung an die API gesendet wird")]
        public void WennBewerbungGesendetWird()
        {
            if (_requestData == null)
                throw new InvalidOperationException("Anfragedaten dürfen nicht null sein.");

            _response = ApiClient.Post("/api/apply", _requestData);
            _scenarioContext["response"] = _response;
        }

        [Then("der Statuscode sollte 200 OK sein")]
        public void DannStatuscodeSollte200Sein()
        {
            Assert.That(_response?.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Antwort ist null oder Statuscode passt nicht.");
        }

        [Then("der Statuscode sollte 400 BadRequest sein")]
        public void DannStatuscodeSollte400Sein()
        {
            Assert.That(_response?.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest), "Antwort ist null oder Statuscode passt nicht.");
        }

        [Then(@"die Antwort sollte in ein Modell gebunden sein und ""(.*)"" enthalten")]
        public void DannAntwortSollteEnthalten(string erwarteteNachricht)
        {
            var content = _response?.Content ?? "";
            var model = JsonSerializer.Deserialize<ApiAntwort>(content);

            Console.WriteLine($"🔁 Gebundene Nachricht: {model?.Message}");
            Assert.That(model?.Message, Is.EqualTo(erwarteteNachricht));
        }
    }
}