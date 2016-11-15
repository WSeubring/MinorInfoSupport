Feature: Vaststellen geschiktheid bestuurder 
om te voldoen aan de wet en risico's te vermijden
als verhuurder
wil ik de geschiktheid van een bestuurder vaststellen
 
Scenario: Vaststellen geschiktheid bestuurder 
Given Heeft een <rijbewijssoort> rijbewijs
And Heeft rijbewijs <rijbewijstype>
And Rijbewijs is geldig tot <rijbewijsvervaldatum>
And is geboren op <geboortedatum>
And Begindatum huurperiode is <startdatum>
And De huurperiode is <huurperiode> dagen
And het rijbewijs is afgegeven in <rijbewijsland>
When
Then is de bestuurder <resultaat> voor verhuur
| rijbewijssoort | rijbewijstype | rijbewijsvervaldatum | geboortedatum | startdatum | huurperiode | rijbewijsland |
|                |               |                      |               |            |             |               | 


Scenario: Vaststellen geschiktheid rijbewijs bestuurder voor verhuur
Given heeft <rijbewijssoort> rijbewijs
And bestuurder heeft rijbewijs <rijbewijstype>
And rijbewijs vervalt op <rijbewijsvervaldatum>
And de huurperiode eindigt op <eindatum>
Then is het rijbewijs van de bestuurder is <resultaat> 
| rijbewijssoort | rijbewijstype | rijbewijsvervaldatum | eindatum   | resultaat               |
| Europees       | B             | 01-01-2027           | 25-10-2025 | geschikt                |
| Internationaal | A             | 01-01-2027           | 25-10-2025 | Incorrect rijbewijstype |
| Chinees        | B             | 01-02-2017           | 31-01-2017 | Onleesbaar rijbewijs    |
| Europees       | B             | 01-01-2027           | 01-01-2027 | Rijbewijs verlopen      |
| Internationaal | B             | 01-01-2027           | 02-01-2027 | Rijbewijs verlopen      |
