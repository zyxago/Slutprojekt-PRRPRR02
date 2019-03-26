# Slutprojekt-PRRPRR02

För att skapa nya banor, torn, fiender behöver man uppfylla dessa krav:

Banor:
Behövs en XML fil med en filsökväg till en bild och en x, y värden som defienera vägen fiender kommer följa. 
Första "pointen" anger startpunkten, sista "pointen" anger slutpunkt på banan.
MÅSTE finnas en filsökväg och minst två points
Ex.
<?xml version="1.0"?>
<Map>
	<Texture>C:\Users\...</Texture>
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
