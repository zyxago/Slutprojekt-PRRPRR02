# Slutprojekt-PRRPRR02

För att skapa nya banor, torn, fiender behöver man uppfylla dessa krav:

Banor:
Behövs en XML fil med en filsökväg till en bild och en x, y värden som defienera vägen fiender kommer följa. 
Första "pointen" anger startpunkten, sista "pointen" anger slutpunkt på banan.
MÅSTE finnas en filsökväg och minst två points
Ex.
<?xml version="1.0"?>
<Map>
	<Texture>Map\Graphics\...</Texture>
	<Path>
		<point>
			<X>100</X>
			<Y>300</Y>
		</point>
		<point>
			<X>100</X>
			<Y>100</Y>
		</point>
	</Path>
</Map>

Torn:
Behövs en XML fil och en bild till tornet, olika sorters torn har lite annorlunda värden i XML filen.
Exempel på classic tower:
<?xml version="1.0"?>
<CTower>	<---Indikerar att det är ett classic Tower
	<Texture>Towers\Graphics\...</Texture>
	<Name>Classic Tower</Name>	//Namn på tornet
	<Cost>20</Cost>		//Kostnad för tornet
	<Radius>25</Radius>	//Storleken på tornet
	<Attack>
		<range>250</range>	//Anfalls räckvidden
		<speed>1</speed>	//Attack hastighet i sekunder
		<dmg>10</dmg>	//Skadan den gör mot fiender
		<type>pierce</type>	//Typ av skada
		<typeModifier>2</typeModifier>	//Om typen är av pierce så avgör denna hur många skottet går igenom innan den går sönder. Är typen av splash så avgör den hur stor radie explosionen kommer att ske på.
		<projectileTexture>Towers\Graphics\projectile\...</projectileTexture>	//Texture för skottet
	</Attack>
</CTower>

Ända skillnaden på classic och road är att road har ett hp värde
Exempel på Road tower:
<?xml version="1.0"?>
<RTower>	<---Indikerar att det är ett road Tower
	<Texture>Towers\Graphics\...</Texture>
	<Name>Road Tower</Name>
	<Cost>20</Cost>
	<Radius>25</Radius>
	<Attack>
		<range>50</range>
		<speed>2</speed>
		<dmg>40</dmg>
		<type>splash</type>
		<typeModifier>100</typeModifier>
		<projectileTexture>Towers\Graphics\projectile\...</projectileTexture>
	</Attack>
	
	<Hp>10</Hp>	//Hp för tornet
</RTower>

Spell tower kan inte attackera och har en spell istållet så den är lite annorlunda ifrån de andra.
Det ända spellet man kan välja utifrån för tillfället är 'Tslow'
Exempel på Spell tower:
<?xml version="1.0"?>
<STower>	<---Indikerar att det är ett spell Tower
	<Texture>Towers\Graphics\...</Texture>
	<Name>Spell Tower</Name>
	<Cost>35</Cost>
	<Radius>25</Radius>
	<Spell>
		<type>Tslow</type>	//Typ av spell, i detta fall ett slow spell
		<cooldown>10</cooldown>		//cooldown för spellet i sekunder
		<radius>100</radius>	//Spellets räckvidd
	</Spell>
</STower>

XML exemplen till fiender:
<?xml version="1.0"?>
<NEnemy>	<----Indikator på att det är en standar fiender
	<Texture>Enemies\Graphics\standardEnemy1.png</Texture>
	<Speed>5</Speed>
	<Hp>20</Hp>
	<Radius>20</Radius>
	<Resistance>splash</Resistance>
</NEnemy>

XML elit exempel:
<?xml version="1.0"?>
<EEnemy>
	<Texture>Enemies\Graphics\elitEnemy1.png</Texture>
	<Speed>5</Speed>
	<Hp>100</Hp>
	<Radius>20</Radius>
	<Resistance>pierce</Resistance>
	
	<Ability>
		<cooldown>10</cooldown>	      //cooldown för spellet i sekunder
		<radius>100</radius>	//Spellets räckvidd
		<type>Eslow</type>	//Typ av spell, i detta fall ett attack speed slow spell
	</Ability>
</EEnemy>
