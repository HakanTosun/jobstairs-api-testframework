Feature: Bewerbung abschicken

  @Smoke @Positive
  Scenario: Erfolgreiche Bewerbung
    Given gültige Bewerberdaten sind vorbereitet
    When die Bewerbung an die API gesendet wird
    Then der Statuscode sollte 200 OK sein
    And die Antwort sollte in ein Modell gebunden sein und "Bewerbung erfolgreich empfangen" enthalten

  @Smoke @Negative
  Scenario: Ungültige Bewerbung ohne Name
    Given ein Bewerber ohne Namen
    When die Bewerbung an die API gesendet wird
    Then der Statuscode sollte 400 BadRequest sein
    And die Antwort sollte in ein Modell gebunden sein und "Ungültige Daten" enthalten