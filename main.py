from spotifyhack.hackclient import HackClient
from bluetoothhack import asifblue, bluetoothscanner, testaddress
from spotifyhack.RestClient import RestClient


rest_client = RestClient()
#allMacs = rest_client.get_registered_macs()
allUsers = rest_client.get_all_users()

print(allUsers)

client = HackClient()

blackholesun = client.gettrackuriforsongbyartist("Soundgarden","black hole sun")
regularjohn = client.gettrackuriforsongbyartist("Queens of the stone age","regular john")
jeremy = client.gettrackuriforsongbyartist("pearl jam","jeremy")
jopparoad = client.gettrackuriforsongbyartist("ween","joppa road")
voodolady = client.gettrackuriforsongbyartist("ween", "Voodoo Lady")

while True:
	found = False
	devices = asifblue.searchdevices(1)
	print(devices)
	for device in devices:
		for user in allUsers:
			if user['macAddress'] == device and not found:
				song = client.gettrackuriforsongbyartist(user['artist'], user['song'])
				print("Hey "+ user['name'] + " Here's your song.")
				found = True
				client.stop()
				client.playtrack(song)
