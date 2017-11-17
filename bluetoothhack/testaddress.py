from bt_rssi import BluetoothRSSI
import time
import sys

#BT_ADDR = '6C:72:E7:2E:96:F4'  # You can put your Bluetooth address here 
BT_ADDR = '48:5A:B6:3F:E3:55'
NUM_LOOP = 1

def dotest():
    addr = BT_ADDR
    num = NUM_LOOP
    btrssi = BluetoothRSSI(addr=addr)
    for i in range(0, num):
        print(btrssi.get_rssi())
        time.sleep(1)
