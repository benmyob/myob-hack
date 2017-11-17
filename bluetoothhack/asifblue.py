# file: inquiry.py
# auth: Albert Huang <albert@csail.mit.edu>
# desc: performs a simple device inquiry followed by a remote name request of
#       each discovered device
# $Id: inquiry.py 401 2006-05-05 19:07:48Z albert $
#

import bluetooth 
#from bluetooth.ble import DiscoveryService


#def lowenergysearch():
	#service = DiscoveryService()
	#devices = service.discover(2)

	#for address, name in devices.items():
    	#	print("name: {}, address: {}".format(name, address))

def searchdevices(frequency):
	print("performing inquiry...")

	#nearby_devices = bluetooth.discover_devices(duration=frequency, lookup_names=True, flush_cache=False, lookup_class=False)
	nearby_devices = bluetooth.discover_devices(flush_cache=True)
	print(nearby_devices)
	print("found %d devices" % len(nearby_devices))

	#for addr, name in nearby_devices:
    	#	try:
        #		print("  %s - %s" % (addr, name))
    	#	except UnicodeEncodeError:
        #		print("  %s - %s" % (addr, name.encode('utf-8', 'replace')))
	return nearby_devices
