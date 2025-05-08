# 🧪 JobStairs API Testautomatisierung (SpecFlow + NUnit + RestSharp)

## 📘 Projektübersicht

Dieses Projekt ist ein vollständiges, erweiterbares automatisiertes Testframework zur Überprüfung einer RESTful Web API, exemplarisch am JobStairs-Portal. Es dient als Beispiel für professionelle Softwaretests mit Fokus auf:

- API-Testautomatisierung mit **SpecFlow (BDD)**, **NUnit**, **RestSharp**
- Testdaten-Generierung mit **Bogus**
- Parallele Testausführung und Tagging
- HTML-Testbericht mit **LivingDoc**
- Erweiterbarkeit für UI-Tests (z. B. mit Playwright in C#)

---

## 🧰 Technologien

| Komponente | Zweck                |
| ---------- | -------------------- |
| .NET 8     | Zielplattform        |
| SpecFlow   | BDD mit Gherkin      |
| NUnit      | Testframework        |
| RestSharp  | HTTP Client          |
| Bogus      | Fake-Daten Generator |
| LivingDoc  | HTML-Testbericht     |
| GitHub     | Quellcode-Verwaltung |
| VS Code    | Entwicklungsumgebung |

---

## 📁 Projektstruktur

```plaintext
JobStairs-QA-Projekt/
├── src/
│   └── JobStairsSpecflowTest/
│       ├── Features/
│       ├── Steps/
│       ├── Models/
│       ├── Helpers/
│       ├── Hooks/
│       ├── TestResults/
│       ├── Properties/
│       │   └── AssemblyInfo.cs
│       └── specflow.json
├── LivingDoc.html
├── specflow.runsettings
├── README.md
```

---

## ⚙️ Installation & Erste Schritte

```bash
git clone https://github.com/HakanTosun/jobstairs-api-testframework.git
cd jobstairs-api-testframework
dotnet restore
```

---

## 🚀 Tests ausführen

```bash
dotnet test --settings specflow.runsettings
dotnet test --filter TestCategory=smoke
livingdoc test-assembly bin/Debug/net8.0/JobStairsSpecflowTest.dll -t bin/Debug/net8.0/TestExecution.json -o LivingDoc.html
```

---

## 🧪 Beispieltests

### Feature-Datei

```gherkin
@smoke @api
Feature: Bewerbung abschicken

  Scenario: Erfolgreiche Bewerbung
    Given gültige Bewerberdaten sind vorhanden
    When die Bewerbung an die API gesendet wird
    Then der Statuscode ist 200 OK
    And die Antwort enthält "Bewerbung erfolgreich empfangen"
```

### Step-Definition

```csharp
[Binding]
public class ApplySteps {
    private readonly ScenarioContext _context;
    private Applicant _requestData;

    public ApplySteps(ScenarioContext context) => _context = context;

    [Given(@"gültige Bewerberdaten sind vorhanden")]
    public void GivenValidApplicantData() {
        _requestData = BogusGenerator.CreateFakeApplicant();
        _context["data"] = _requestData;
    }
}
```

---

## 🏷️ Tags & Kategorien

```gherkin
@parallel @regression
Scenario: Ungültige Bewerbung ohne Name
```

```csharp
[Test, Category("Smoke"), Category("API")]
```

```bash
dotnet test --filter TestCategory=regression
```

---

## 🧵 Parallele Testausführung

```csharp
// src/JobStairsSpecflowTest/Properties/AssemblyInfo.cs
using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(4)]
```

---

## 🔧 Erweiterte Features mit Beispielen

### ✅ TestContext

```csharp
[Test]
public void LogTestStatus() {
    TestContext.WriteLine("Test wird gestartet...");
    Assert.Pass("Alles OK!");
}
```

### ✅ SetUp und TearDown

```csharp
[SetUp]
public void VorJederTest() {
    _client = new RestClient("https://example.org");
}

[TearDown]
public void NachJedemTest() {
    TestContext.WriteLine("Test beendet.");
}
```

### ✅ TestCaseSource

```csharp
[Test, TestCaseSource(nameof(Testdaten))]
public void BewerbungsTest(string name) {
    Assert.IsNotNull(name);
}

static IEnumerable<string> Testdaten => new[] { "Max", "Anna", "Lena" };
```

### ✅ TestFixture + Parallelizable

```csharp
[TestFixture]
[Parallelizable(ParallelScope.All)]
public class ParallelTests {
    [Test]
    public void Test1() => Assert.Pass();

    [Test]
    public void Test2() => Assert.Pass();
}
```

### ✅ ScenarioContext & FeatureContext

```csharp
[Binding]
public class BeispielSteps {
    private readonly ScenarioContext _context;

    public BeispielSteps(ScenarioContext context) => _context = context;

    [Given("ich speichere einen Wert")]
    public void WertSpeichern() => _context["Wert"] = "Hallo";

    [Then("der gespeicherte Wert sollte Hallo sein")]
    public void WertPruefen() => Assert.AreEqual("Hallo", _context["Wert"]);
}
```

---

## 🧠 Best Practices

- Verwende Tags zur Testfilterung und Berichterstellung
- Halte Feature-Dateien klar, strukturiert und verständlich
- Testdaten über `Bogus` oder `TestCaseSource` generieren
- Setze `ScenarioContext` für komplexe Szenarien ein
- Verwende `LivingDoc` für Stakeholder-freundliche Reports

---

## 📄 Lizenz

Dieses Projekt verwendet die **MIT-Lizenz** – frei verwendbar auch für kommerzielle Nutzung.

---

## 👨‍💻 Autor

**Hakan Tosun**
[https://github.com/HakanTosun]
