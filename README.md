# EscapeRoomRedi
Escape room -peli, jossa tapahtumapaikkana on Kalasataman Redi-kauppakeskus.

## Tausta
Academyn Accelerated Learning Program C# -kurssin viikon 4 ryhmätyö. Aiheena tekstipohjainen seikkailupeli ConsoleApplication-toteutuksella.

## Rakenne
### Pelin osat
- O pelaaja
- H vastustaja
- \# seinä
- W, X, Y, Z ovi
- @ lukko
- a, b, c, d, e, f avain

### Pelin tasot
1. Pimeä huone
2. Kauppakeskuksen kerros avaimilla ja lukitulla ovella
3. Parkkihalli, jossa tekoälyllä ohjattu vastustaja
4. Kerros, jossa hissit vaihtoehtoisiin suuntiin
5. Kerros, jossa vaihtoehtoiset loppuratkaisut

## Toteutus
![luokkakaavio](https://github.com/markojuv/EscapeRoomRedi/blob/master/luokkakaavio.png)
Ohjelma lukee pohjakartat ja isot ASCII-grafiikkaelementit ulkoisista tekstitiedostoista.
