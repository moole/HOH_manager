HOH_manager
===========

Manažer šifrovacích her pro Windows Mobile.

HOHManager vznikl jako podpora [Horské Orientační Hry](http://orientacni-hra.ic.cz/), kterou každý rok organizuje Michal Peitz, a bodla mu pomoc s počítáním stavu hry, odesíláním nápověd a pořadí ve hře. Pokud netušíte, co jsou šifrovací hry, tak se mrkněte třeba sem: [Kalendář šifrovacích her](http://herka.deka.cz/index.php/Kalendář_šifrovac%C3%ADch_her).
HOHManager umí importovat a exportovat data hry z/do CSV, aby se dala editovat v Excelu/OOo. HOHManager přijímá SMSky od týmů, počítá body, pořadí, penalizace, odesílá SMSky s nápovědami, s informacemi o pořadí atd. a obsahuje rozhraní pro korekci špatně poslaných SMSek.
HOHManager je napsaný v C#, běží na WM 5.0 a vyšších, podmínkou je .NET CF 3.0 (myslím). Bude-li zájem, můžu ho dát k dispozici zdrojový kód, jinak pokud byste ho chteli zkusit nebo potřebujete úpravy pro svou hru, napište.
Další informace, obrázky a download uvnitř článku.

Jak HOHManager používat
-----------------------

Rychlý návod, jak na to:
1.  Stáhnout a zkompilovat solution pro své zařízení v MS VS 2008 nebo stáhnout zkompilovanou binárku dole, nahrát PocketPC a spustit.
2.  V menu Import/Export vyexportovat všechny tři datové soubory (týmy, šifry, pravidla), stáhnout je do PC.
3.  Editovat/doplnit v Excelu nebo notepadu (UTF-8, text do uvozovek, buňky oddělované středníkem).
4.  Soubory nahrát zpět do PocketPC a všechny tři načíst. Povedlo-li se, hotovo!
5.  Program teď čeká na příchozí SMS, odpovídá a počítá stav. Tým je možné ručně odstartovat/vzdát/finalizovat na záložce Tým-Info.
6.  Exportovat Aktuální stav hry a přijaté SMS, zkopírovat do PC, načíst v Excelu a.. to už je na vás.

Pravidla počítání
-----------------

# HOHManager podporuje: #
*     Nápovědy - odesílání nápověd k šifrám, možnost omezení počtu nápověd na tým
*   Pořadí - odesílání pořadí ve hře, na vyžádání i po řešení šifry
*   Bonifikace šifer - koeficient násobného počtu bodů za šifru podle pořadí příchodu
*   Skupiny šifer - týmu se počítá jen první vyřešená šifra ze skupiny
*   Rozdílný čas staru - každý tým může startovat v jiný čas, který se v pořadí na šifrách a cíli bere v úvahu
*   Penalizace za pozdní příchod - bodové penalizace za pozdní příchod do cíle v bodových pásmech a diskvalifikace po timeoutu
*   Vzdání týmu po SMS
*   Možnost vyměnit pravidla/týmy/šifry v průběhu hry

# Další poznámky: #
*   Čas startu týmu se nastavuje v CSV souboru týmů, tým se automaticky odstartuje v tento čas (není potřeba mačkat Start týmu).
*   Stav programu se ukládá po každé změně, měl by být odolný proti kousnutí zařízení nebo pádu programu.
*   SMSky po zachycení programem projdou i do PocketOutlooku, takže i když program úplně něco pokazí, tak SMSky ze hry se neztratí.
*   Validní SMSky lze z programu exportovat do CSV ve tvaru: čas;číslo;text a zpracovat ručně
*   Je možné importovat Šifry i Týmy bez ztráty průběhu hry (checkbox na obrazovce Import/Export), ale všechny už obdržené SMS se jakoby obdrží znova včetně odeslání odpovědí týmům.
*   Resetovat stav programu tedy lze efektivně načtením nových Šifer a Týmu se zaškrtnutým Zahodit stav hry....
*   Dvojklikem na řádek (skoro) jakéhokoliv listboxu se zobrazí detail řádku.
*   Za správnou odpověď bez nápovědy se započítají body dle bonifikace, za správnou odpověď s nápovědou polovina bodů, za špatnou odpověď s i bez nápovědy se týmu odečte hodnota šifry, za nezodpovězenou šifru s nápovědou se odečte polovina hodnoty šifry, a šifra bez odpovědi a bez nápovědy je samozřejmě za 0.
*   Počítá se vždy pouze první odpověď ze skupiny šifer, ostatní se ignorují, včetně žádostí o nápovědu po přijetí řešení.
*   Testoval jsem převážně jen na emulátoru a pouze na 240x320

Tvary SMSek
-----------

HOHManager podporuje čtyři druhy SMSek:
Odpověd na jednu nebo více šifer: "ID_tymu ID_sifry:odpoved ID_sifry:odpoved ID_sifry:odpoved" atd. (zaregistruje odpovědi na šifry)
Žádost o nápovědu k šifře: "ID_tymu ID_sifry NAPOVEDA" (odešle nápovědu, pokud tým ještě může brát)
Žádost o aktuální pořadí ve hře: "ID_tymu PORADI" (odešle info o pořadí ve hře)
Informaci o vzdání týmu: "ID_tymu VZDAVAME" (vzdá tým)
Odpovědi posílá HOHManager na číslo, ze kterého SMS přišla. Neodpovídá-li text SMS žádnému vzoru, objeví se na záložce Problémy a můžete ho opravit nebo SMS smazat (v Exportovaných SMS zůstane).

Obrázky
-------

![1](http://moole.itpro.cz/uploads/HOH/HOH_01.png)
![2](http://moole.itpro.cz/uploads/HOH/HOH_02.png)
![3](http://moole.itpro.cz/uploads/HOH/HOH_03.png)
![4](http://moole.itpro.cz/uploads/HOH/HOH_04.png)
![5](http://moole.itpro.cz/uploads/HOH/HOH_05.png)
![6](http://moole.itpro.cz/uploads/HOH/HOH_06.png)
![7](http://moole.itpro.cz/uploads/HOH/HOH_07.png)
![8](http://moole.itpro.cz/uploads/HOH/HOH_08.png)
![9](http://moole.itpro.cz/uploads/HOH/HOH_09.png)
        
Úpravy
------

Tyto poznámky se týkají zde stahnutelné verze programu. HOHManager jde ale poměrně jednoduše upravit pro potřeby snad jakékoliv šifrovačky. Napište.

Download
--------

Zkompilovaná binárka (.NET 3.0)
Visual Studio 2008 Solution GitHub